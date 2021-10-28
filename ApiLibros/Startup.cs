using ApiLibros.Data;
using ApiLibros.LibroMapper;
using ApiLibros.Repository;
using ApiLibros.Repository.Irepsitory;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace ApiLibros
{
#pragma warning disable CS1591 // Falta el comentario XML para el tipo o miembro visible públicamente
    public class Startup
#pragma warning restore CS1591 // Falta el comentario XML para el tipo o miembro visible públicamente
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {


            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("conections")));
            //Aqui hacemos la inyection de la Dto Mapper
            services.AddAutoMapper(typeof(LibroMappers));

            services.AddScoped<ICategoriaRepository, CategoriaRepository>();
            services.AddScoped<IAutorRepository, AutorRepository>();
            services.AddScoped<ILibroRepository, LibroRepository>();
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(
                Options =>
                {
                    Options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration.GetSection("AppSettings:Token").Value)),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                } );


            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("LibrosUsuario", new OpenApiInfo
                { Title = "Api Usuario Libros",
                 Version = " 1",
                 Description="Backend Libro",
                 Contact=new OpenApiContact
                 { 
                     Email = "Guitarra2428@gmail.com",
                     Name = "Luis",
                     Url = new Uri("http://quitckcode-001-site1.etempurl.com/")
                 },
                 License=new OpenApiLicense
                 {
                     Name="Ing. Luis Mesili",
                     Url = new Uri("http://quitckcode-001-site1.etempurl.com/")

                 }

                });
                c.SwaggerDoc("Libros", new OpenApiInfo
                {
                    Title = "Api Libros",
                    Version = " 1",
                    Description = "Backend Libro",
                    Contact = new OpenApiContact
                    {
                        Email = "Guitarra2428@gmail.com",
                        Name = "Luis",
                        Url = new Uri("http://quitckcode-001-site1.etempurl.com/")
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Ing. Luis Mesili",
                        Url = new Uri("http://quitckcode-001-site1.etempurl.com/")

                    }

                });

                c.SwaggerDoc("LibrosCategoria", new OpenApiInfo
                {
                    Title = "Api Categoria Libros",
                    Version = " 1",
                    Description = "Backend Libro",
                    Contact = new OpenApiContact
                    {
                        Email = "Guitarra2428@gmail.com",
                        Name = "Luis",
                        Url = new Uri("http://quitckcode-001-site1.etempurl.com/")
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Ing. Luis Mesili",
                        Url = new Uri("http://quitckcode-001-site1.etempurl.com/")

                    }

                });

                c.SwaggerDoc("LibrosAutor", new OpenApiInfo
                {
                    Title = "Api Autor Libros",
                    Version = " 1",
                    Description = "Backend Libro",
                    Contact = new OpenApiContact
                    {
                        Email = "Guitarra2428@gmail.com",
                        Name = "Luis",
                        Url = new Uri("http://quitckcode-001-site1.etempurl.com/")
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Ing. Luis Mesili",
                        Url = new Uri("http://quitckcode-001-site1.etempurl.com/")

                    }

                });
                //inyections De documentacio de la Api
                var documentacion = $"{ Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var rutadocumentacio = Path.Combine(AppContext.BaseDirectory, documentacion);
                c.IncludeXmlComments(documentacion);
                //Autenticacion DE Token
                c.AddSecurityDefinition("Bearer",
                    new OpenApiSecurityScheme
                    {
                        Description = "Autenticacion JTW (Bearer)",
                        Type = SecuritySchemeType.Http,
                        Scheme = "Bearer"
                    }

                    );
                c.AddSecurityRequirement(new OpenApiSecurityRequirement{

                    {
                         new OpenApiSecurityScheme
                          {
                                Reference= new OpenApiReference
                                 {
                                      Id="Bearer",
                                      Type= ReferenceType.SecurityScheme
                                 }
                          }, new List<string>()
                    }             
                });
           
            });

            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(
                      c =>
                      {
                          c.SwaggerEndpoint("/swagger/LibrosUsuario/swagger.json", "Api Usuario Libros ");
                          c.SwaggerEndpoint("/swagger/Libros/swagger.json", "Api Libros ");
                          c.SwaggerEndpoint("/swagger/LibrosCategoria/swagger.json", "Api Categoria Libros ");
                          c.SwaggerEndpoint("/swagger/LibrosAutor/swagger.json", "Api Autor Libros ");

                       }
                 );               
               
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
#pragma warning restore CS1591 // Falta el comentario XML para el tipo o miembro visible públicamente

}
