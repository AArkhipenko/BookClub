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
            //��������� �������������� ����� JWT
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
            {
                options.RequireHttpsMetadata = true;
                //options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    // ��������, ����� �� �������������� �������� ��� ��������� ������
                    ValidateIssuer = true,
                    // ������, �������������� ��������
                    ValidIssuer = AuthOptions.ISSUER,

                    // ����� �� �������������� ����������� ������
                    ValidateAudience = true,
                    // ��������� ����������� ������
                    ValidAudience = AuthOptions.AUDIENCE,
                    // ����� �� �������������� ����� �������������
                    ValidateLifetime = true,

                    // ��������� ����� ������������
                    IssuerSigningKey = AuthOptions.SecretKey,
                    // ��������� ����� ������������
                    ValidateIssuerSigningKey = true,
                };
            });

            services.AddControllersWithViews();
            //�������� ����� ����������� ������ ��������������� ����������
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
                //�������� �������������� ��������� ������������� �������� https
                app.UseHsts();
                //���������� ������������� ����������� ������ �������������� ����������
                app.UseSpaStaticFiles();
            }



            //���������� ��������������� http �������� �� https
            app.UseHttpsRedirection();
            //������������� ����������� ������ (�� ����� wwwroot)
            app.UseStaticFiles();
            //����� �������������� ����� ��������� � ����
            app.UseCookiePolicy(new CookiePolicyOptions
            {
                //�������� ������� ������������ ������ cookie ��� ����� ����������, ������� �� ���������� �� ��������� cross-origin ��������
                MinimumSameSitePolicy = SameSiteMode.Strict,
                //���� �������� �� ������ �� ������� (��� ���� ������ ������ �������� � true)
                HttpOnly = HttpOnlyPolicy.None,
                //�������� ���� ������ ����� https
                Secure = CookieSecurePolicy.Always
            });



            //������������� ����������� ��������� ������ ��������
            app.UseRouting();



            //middleware 
            //����������� ���� � ���������� ��� � ��������� �������
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


            //������������� �������������� ����������
            app.UseSpa(spa =>
            {
                //���� � ����� � ����������� angular
                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                    //� ������ ������ � ������ ����������, ����� ������� angular ���������� �������������
                    spa.UseAngularCliServer(npmScript: "start");
            });

        }
    }
}
