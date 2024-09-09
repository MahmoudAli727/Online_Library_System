using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace Online_Lbrary_System.ExtensionAuth
{
	public static class ExtensionAuthJWT
	{
		public static void AddCustomJwtAuth(this IServiceCollection services, ConfigurationManager configuration)
		{
			services.AddAuthentication(o =>
			{
				o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
				o.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
			}).AddJwtBearer(o =>
			{
				o.RequireHttpsMetadata = false;
				o.SaveToken = true;
				o.TokenValidationParameters = new TokenValidationParameters()
				{
					ValidateIssuer = true,
					ValidIssuer = configuration["JWT:Issuer"],
					ValidateAudience = true,
					ValidAudience = configuration["JWT:Audience"],
					ValidateIssuerSigningKey = true,
					IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:SecretKey"])),
					ClockSkew = TimeSpan.Zero
				};
			});
		}

		public static void AddSwaggerGenJwtAuth(this IServiceCollection services)
		{
			services.AddSwaggerGen(o =>
			{
				o.SwaggerDoc("v1", new OpenApiInfo()
				{
					Version = "v1",
					Title = "App_JWT_Auth",
					Description = "This For Jwt",
					Contact = new OpenApiContact()
					{
						Name = "Mahmoud Ali Mahmoud Abdelaal",
						Email = "mahmoudalio353@gmail.com",
						Url = new Uri("https://google.com")
					}
				});

				o.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
				{
					Name = "Authorization",
					Type = SecuritySchemeType.ApiKey,
					Scheme = "Bearer",
					BearerFormat = "JWT",
					In = ParameterLocation.Header,
					Description = "Enter the JWT Key"
				});

				o.AddSecurityRequirement(new OpenApiSecurityRequirement() {
					{
					   new OpenApiSecurityScheme()
					   {
						  Reference = new OpenApiReference()
						  {
							 Type = ReferenceType.SecurityScheme,
							 Id = "Bearer"
						  },
						  Name = "Bearer",
						  In = ParameterLocation.Header
					   },
					   new List<string>()
					}
				});
			});
		}
	}
}
