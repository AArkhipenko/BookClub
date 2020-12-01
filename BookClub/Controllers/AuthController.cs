
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;

namespace BookClub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ILogger<BookController> _logger;

        public AuthController(ILogger<BookController> logger)
        {
            _logger = logger;
        }

        [HttpPost, Route("login")]
        public IActionResult LogIn(Models.LoginModel user)
        {
            if (user == null)
                throw new Exception("Модель аутентификации отсутствует");

            using (BookDb.BookDbContext db = new BookDb.BookDbContext())
            {
                BookDb.User dbUser = db.User.FirstOrDefault(x => x.Login == user.UserName && x.Password == user.Password);
                if (dbUser == null)
                    throw new Exception("Указаны неверный логин или пароль");
                else
                {
                    //аутентификация прошла успешно
                    //составляем, некоторый список параметров аутентифицированного пользователя
                    var claims = new List<Claim>{
                        new Claim(ClaimsIdentity.DefaultNameClaimType, dbUser.Login),
                        new Claim("id", dbUser.Id.ToString())
                        };

                    //генерим токен
                    var secretKey = AuthOptions.SecretKey;
                    var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
                    var tokeOptions = new JwtSecurityToken(
                        issuer: AuthOptions.ISSUER,
                        audience: AuthOptions.AUDIENCE,
                        claims: claims,
                        notBefore: DateTime.Now,
                        expires: DateTime.Now.AddMinutes(AuthOptions.LIFETIME),
                        signingCredentials: signinCredentials
                    );
                    var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);

                    //добавляем токен в куки, он не видим клиентом
                    HttpContext.Response.Cookies.Append(AuthOptions.TOKENCOOKIE, tokenString,
                        new CookieOptions
                        {
                            HttpOnly = true,
                            MaxAge = TimeSpan.FromMinutes(AuthOptions.LIFETIME)
                        });
                    //добавляем флаг прохождения аутентификации, с которым клиент сверяется
                    HttpContext.Response.Cookies.Append(AuthOptions.TESTCOOKIE, "true",
                        new CookieOptions
                        {
                            HttpOnly = false,
                            MaxAge = TimeSpan.FromMinutes(AuthOptions.LIFETIME)
                        });
                    return Ok();
                }
            }
        }

        [HttpPost, Route("registry")]
        public IActionResult Registry(Models.RegistryModel model)
        {
            using (BookDb.BookDbContext db = new BookDb.BookDbContext())
            {
                if (db.User.Any(x => x.Login == model.UserName))
                    throw new Exception("Пользователь с указанным логином уже имеется");

                using (IDbContextTransaction trans = db.Database.BeginTransaction())
                {
                    var user = new BookDb.User();
                    user.Login = model.UserName;
                    user.Password = model.Password;

                    db.User.Add(user);
                    db.SaveChanges();

                    trans.Commit();
                }
            }
            return Ok();
        }

        [Authorize]
        [HttpPost, Route("logout")]
        public IActionResult LogOut()
        {
            var cookies = new List<string>
            {
                AuthOptions.TOKENCOOKIE,
                AuthOptions.TESTCOOKIE
            };
            foreach (var cookieName in cookies)
            {
                if (HttpContext.Request.Cookies.ContainsKey(cookieName))
                    //такие манипуляции позволяют удалить куки на строне клиента
                    HttpContext.Response.Cookies.Append(cookieName, "",
                            new CookieOptions
                            {
                                Expires = DateTime.Now.AddSeconds(-1)
                            });
            }

            return Ok();
        }


    }
}
