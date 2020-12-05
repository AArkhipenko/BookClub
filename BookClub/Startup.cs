using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.CookiePolicy;

namespace BookClub
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //настройки аутентификации через JWT
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
            {
                options.RequireHttpsMetadata = true;
                //options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    // укзывает, будет ли валидироваться издатель при валидации токена
                    ValidateIssuer = true,
                    // строка, представляющая издателя
                    ValidIssuer = AuthOptions.ISSUER,

                    // будет ли валидироваться потребитель токена
                    ValidateAudience = true,
                    // установка потребителя токена
                    ValidAudience = AuthOptions.AUDIENCE,
                    // будет ли валидироваться время существования
                    ValidateLifetime = true,

                    // установка ключа безопасности
                    IssuerSigningKey = AuthOptions.SecretKey,
                    // валидация ключа безопасности
                    ValidateIssuerSigningKey = true,
                };
            });

            services.AddControllersWithViews();
            //настроки папки статических файлов одностраничного приложения
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseExceptionHandler("/api/error/error");
            if (!env.IsDevelopment())
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                //механизм принудительной активации использования протокло https
                app.UseHsts();
                //добавление использование статических файлов одностраничных приложений
                app.UseSpaStaticFiles();
            }



            //добавление перенаправление http запросов на https
            app.UseHttpsRedirection();
            //использование статических файлов (из папки wwwroot)
            app.UseStaticFiles();
            //токен аутентификации будет храниться в куки
            app.UseCookiePolicy(new CookiePolicyOptions
            {
                //повышает уровень безопасности файлов cookie для типов приложений, которые не полагаются на обработку cross-origin запросов
                MinimumSameSitePolicy = SameSiteMode.Strict,
                //куки меняются не только на сервере (для куки токена ставим параметр в true)
                HttpOnly = HttpOnlyPolicy.None,
                //передача куки только через https
                Secure = CookieSecurePolicy.Always
            });



            //использование возможности изменения адреса страницы
            app.UseRouting();



            //middleware 
            //вытаскиваем куки и докидываем его в заголовок запроса
            app.Use(async (context, next) =>
            {
                var token = context.Request.Cookies[AuthOptions.TOKENCOOKIE];
                if (!string.IsNullOrEmpty(token))
                    context.Request.Headers.Add("Authorization", "Bearer " + token);

                await next();
            });
            app.UseAuthentication();
            app.UseAuthorization();



            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
            });


            //использование одностраничных приложений
            app.UseSpa(spa =>
            {
                //путь к папке с исходниками angular
                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                    //в случае работы в режиме разработки, старт сервера angular происходит автоматически
                    spa.UseAngularCliServer(npmScript: "start");
            });

        }
    }
}
