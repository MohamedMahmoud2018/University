using AutoMapper;
using BLL;
using CORE;
using CORE.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
//using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace API
{
    public class Startup
    {
        string defaultPolicy = "default";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            TypesToRegister = Assembly.Load("BLL").GetTypes()
                .Where(x => !string.IsNullOrEmpty(x.Namespace))
                .Where(x => x.IsClass).ToList();

            ITypesToRegister = Assembly.Load("BLL").GetTypes()
                .Where(x => !string.IsNullOrEmpty(x.Namespace))
                .Where(x => x.IsInterface).ToList();
        }

        public IConfiguration Configuration { get; }
        public List<Type> TypesToRegister { get; }
        public List<Type> ITypesToRegister { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {



            #region Cors Origins
            //services.AddCors(
            //    options => options.AddDefaultPolicy(
            //        builder => builder
            //            .WithOrigins(
            //                // App:CorsOrigins in appsettings.json can contain more than one address separated by comma.
            //                Configuration["App:CorsOrigins"]
            //                    .Split(",", StringSplitOptions.RemoveEmptyEntries)
            //                    //.Select(o => o.RemovePostFix("/"))
            //                    .ToArray()
            //            )
            //            .AllowAnyHeader()
            //            .AllowAnyMethod()
            //            .AllowCredentials()
            //    )
            //);

            //services.AddCors(options =>
            //{
            //    options.AddDefaultPolicy(
            //        builder =>
            //        {
            //            builder.AllowAnyOrigin()
            //                                .AllowAnyHeader()
            //                                .AllowAnyMethod();
            //        });
            //});
            //services.AddCors(options =>
            //{
            //    options.AddPolicy("CORS", corsPolicyBuilder => corsPolicyBuilder.AllowAnyOrigin()
            //        // Apply CORS policy for any type of origin  
            //        .AllowAnyMethod()
            //        // Apply CORS policy for any type of http methods  
            //        .AllowAnyHeader());
            //        // Apply CORS policy for any headers  
            //        //.AllowCredentials());
            //    // Apply CORS policy for all users  
            //});
            #endregion

            #region Db Context
            services.AddHttpContextAccessor();
            services.AddDbContext<ApplicationDBContext>(options => options
            .UseSqlServer(Configuration.GetConnectionString("Default2")));

            #endregion

            #region identity
            services.AddIdentity<ApplicationUser, ApplicationRole>()
                .AddEntityFrameworkStores<DbContext>()
                .AddDefaultTokenProviders();
            #endregion

            #region authentication
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options =>
                {
                    options.SaveToken = true;
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidAudience = Configuration["JWT:ValidAudience"],
                        ValidIssuer = Configuration["JWT:ValidIssuer"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT:key"]))
                    };
                });
            #endregion

            #region Atuo Mapping
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new Mapping());
            });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);
            #endregion

            #region Swagger
            //Add Swagger relates setting  
            services.AddSwaggerGen(swagger =>
            {
                swagger.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "APPIS API",
                    Version = "v1.1",
                    Description = "API to unerstand request and response schema.",
                });
            });
            #endregion

            #region Dynamic Service Registration
            //services.AddServicesOfType<IScopedService>();
            //services.AddServicesWithAttributeOfType<ScopedServiceAttribute>();
            #endregion


            services.AddScoped<DbContext, ApplicationDBContext>();

            for (int i = 0; i < TypesToRegister.Count; i++)
            {
                var itype = ITypesToRegister
                    .Find(t => (t.Name == "I" + TypesToRegister[i].Name));
                if (itype != null)
                    services.AddScoped(itype, TypesToRegister[i]);
            }

            services.AddControllers();

            #region return Pascal case
            services.AddMvcCore().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.DictionaryKeyPolicy = null;
                options.JsonSerializerOptions.PropertyNamingPolicy = null;
            });
            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,
            UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
        {
            
            #region Swagger
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("v1/swagger.json", "API v1"));

            #endregion

            #region Cors
            app.UseCors(builder =>
            {
                builder
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
            });
            // app.UseCors("CORS");
            #endregion

            app.UseHttpsRedirection();

            #region Static Files
            app.UseStaticFiles();
            // In case of using custom folder to upload files uncomment the folling lines

            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot")),
                RequestPath = new PathString("/wwwroot")
            });
            #endregion
            ///


            
            app.UseCookiePolicy();

            MyIdentityDataInitializer.SeedData(userManager, roleManager);

            app.UseRouting();
            // RolesSeeder.Seed(app.ApplicationServices).Wait();
            
            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
