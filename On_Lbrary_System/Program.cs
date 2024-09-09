using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Online_Lbrary_System.Data;
using Online_Lbrary_System.Data.Model;
using Online_Lbrary_System.ExtensionAuth;
using Online_Lbrary_System.Helper;
using Online_Lbrary_System.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(op =>
	  op.UseLazyLoadingProxies()
		.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.Configure<JWT>(builder.Configuration.GetSection("JWT"));
builder.Services.AddIdentity<AppUser, IdentityRole>().AddEntityFrameworkStores<AppDbContext>();
builder.Services.AddTransient<IAuthServices, AuthServices>();
builder.Services.AddTransient<IBookCategoryServices, BookCategoryServices>();
builder.Services.AddTransient<IOrderServices, OrderServices>();
builder.Services.AddTransient<IBookServices, BookServices>();

builder.Services.AddControllers().AddFluentValidation(validate => validate.RegisterValidatorsFromAssemblyContaining<Program>());
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCustomJwtAuth(builder.Configuration);
builder.Services.AddSwaggerGenJwtAuth();

builder.Services.AddAutoMapper(typeof(Program));

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
