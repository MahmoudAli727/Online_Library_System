using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Online_Lbrary_System.Data.Dtos;
using Online_Lbrary_System.Data.Model;
using Online_Lbrary_System.Helper;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Online_Lbrary_System.Services
{
	public class AuthServices : IAuthServices
	{
		private readonly UserManager<AppUser> _userManager;
		private readonly JWT _jwt;
		private readonly IMapper _mapper;
		//
		public AuthServices(UserManager<AppUser> userManager,
			IOptions<JWT> jwt,
			///
			IMapper mapper)
		{
			_userManager = userManager;
			_jwt = jwt.Value;
			//
			_mapper = mapper;
		}
		public async Task<Auth> RegisterAsync(RegisterDto model)
		{
			if (await _userManager.FindByEmailAsync(model.Email) is not null)
				return new Auth { Message = "Email is already registered!" };

			if (await _userManager.FindByNameAsync(model.UserName) is not null)
				return new Auth { Message = "Username is already registered!" };

			//if (await _cardServices.GetCardById(model.CardId) is null)
			//	return new Auth { Message = "CardId is not exist" };

			using var stream = new MemoryStream();
			await model.image.CopyToAsync(stream);
			var user = new AppUser
			{
				Email = model.Email,
				firstName = model.FName,
				lastName = model.LName,
				Age = model.Age,
				UserName = model.UserName,
				phone = model.phone,
		//
			};
			user.image = stream.ToArray();

			var result = await _userManager.CreateAsync(user, model.Password);

			if (!result.Succeeded)
			{
				var errors = string.Empty;

				foreach (var error in result.Errors)
					errors += $"{error.Description},";

				return new Auth { Message = errors };
			}

			//await _userManager.AddToRoleAsync(user, Role.LibrarianRole);
			await _userManager.AddToRoleAsync(user, Role.UserRole);

			var jwtSecurityToken = await CreateJwtToken(user);

			return new Auth
			{
				Email = user.Email,
				ExpiresOn = jwtSecurityToken.ValidTo,
				IsAuthenticated = true,
				Roles = (await _userManager.GetRolesAsync(user)).ToList(),
				Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
				Username = user.UserName,
			};
		}
		public async Task<Auth> LoginAsync(LoginDto model)
		{
			var authModel = new Auth();

			var user = await _userManager.FindByEmailAsync(model.Email);

			if (user is null || !await _userManager.CheckPasswordAsync(user, model.Password))
			{
				authModel.Message = "Email or Password is incorrect!";
				return authModel;
			}

			var jwtSecurityToken = await CreateJwtToken(user);
			var rolesList = await _userManager.GetRolesAsync(user);

			authModel.IsAuthenticated = true;
			authModel.Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
			authModel.Email = user.Email;
			authModel.Username = user.UserName;
			authModel.ExpiresOn = jwtSecurityToken.ValidTo;
			authModel.Roles = rolesList.ToList();

			return authModel;
		}
		private async Task<JwtSecurityToken> CreateJwtToken(AppUser user)
		{
			var userClaims = await _userManager.GetClaimsAsync(user);
			var roles = await _userManager.GetRolesAsync(user);
			var roleClaims = new List<Claim>();

			foreach (var role in roles)
				roleClaims.Add(new Claim("roles", role));

			var claims = new[]
			{
				new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
				new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
				new Claim(JwtRegisteredClaimNames.Email, user.Email),
				new Claim("uid", user.Id)
			}
			.Union(userClaims)
			.Union(roleClaims);

			var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.SecretKey));
			var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

			var jwtSecurityToken = new JwtSecurityToken(
				claims: claims,
				issuer: _jwt.Issuer,
				audience: _jwt.Audience,
				expires: DateTime.UtcNow.AddMinutes(_jwt.DurationInMinutes),
				signingCredentials: signingCredentials
				);

			return jwtSecurityToken;
		}

		public async Task<List<AppUser>> GetUsers()
		{
			var users = await _userManager.Users.ToListAsync();
			if (users == null)
				return null;

			return users;
		}

		//public async Task<bool> UpdateUserAsync(string email, RegisterDto registerDto)
		//{
		//	var user = await _userManager.FindByEmailAsync(email);

		//	if (user is null)
		//		return false;
		//	user.Age = registerDto.Age;
		//	user.phone = registerDto.phone;
		//	user.Email = registerDto.Email;
		//	user.firstName = registerDto.FName;
		//	user.lastName = registerDto.LName;
		//	user.UserName = registerDto.UserName;

		//	if (registerDto.image != null)
		//	{
		//		using var stream = new MemoryStream();
		//		await registerDto.image.CopyToAsync(stream);
		//		user.image = stream.ToArray();
		//	}
		//	user.image = user.image;
		//	await _userManager.UpdateAsync(user);
		//	return true;
		//}

		public async Task<bool> DeleteUserAsync(string email)
		{

			var user = await _userManager.FindByEmailAsync(email);

			if (user is null)
				return false;

			await _userManager.DeleteAsync(user);
			return true;
		}
	}
}
