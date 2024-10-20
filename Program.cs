using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using WebAPIProject.Data;
using WebAPIProject.Interfaces;
using WebAPIProject.Models;
using WebAPIProject.Repository;
using WebAPIProject.Service;

var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddSwaggerGen(option =>
            {
                option.SwaggerDoc("v1", new OpenApiInfo { Title = "Demo API", Version = "v1" });
                option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter a valid token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                });
                option.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type=ReferenceType.SecurityScheme,
                                Id="Bearer"
                            }
                        },
                        new string[]{}
                    }
                });
            });

            builder.Services.AddControllers().AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling =Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            });

            /* ici je vais creer l"tebalissement avec la base donnée */
            builder.Services.AddDbContext<ApplicationDBContext>(options =>{
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            //ici je vais mettre les options de securtié pour avoir un mdp robuste
            builder.Services.AddIdentity<AppUser,IdentityRole>(options =>{
                options.Password.RequireDigit= true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase =true;
                options.Password.RequireNonAlphanumeric= true;
                options.Password.RequiredLength = 10;
            })
            .AddEntityFrameworkStores<ApplicationDBContext>();

            builder.Services.AddAuthentication(options=> {
                options.DefaultAuthenticateScheme=
                options.DefaultChallengeScheme = 
                options.DefaultForbidScheme =
                options.DefaultScheme = 
                options.DefaultSignInScheme =
                options.DefaultSignOutScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer( options => 
                options.TokenValidationParameters = new TokenValidationParameters 
            {
                ValidateIssuer = true,
                ValidIssuer = builder.Configuration["JWT : Issuer"],
                ValidateAudience = true,
                ValidAudience = builder.Configuration["JWT:Audience"],
                ValidateIssuerSigningKey = true,
                IssuerSigningKey =new SymmetricSecurityKey(
                System.Text.Encoding.UTF8.GetBytes(builder.Configuration["JWT:SigningKey"])
                    
            )}
             
            );

            builder.Services.AddScoped<IStockageRepository,StockageRepository>();
            builder.Services.AddScoped<ICommentsRepository,CommentsRepository>();
            builder.Services.AddScoped<ITokenService,TokenService>();
            builder.Services.AddScoped<IPortfolioRepository, PortfolioRepository>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

                app.UseHttpsRedirection();

                app.UseAuthentication();
                app.UseAuthorization();

                app.MapControllers();

                app.Run();

