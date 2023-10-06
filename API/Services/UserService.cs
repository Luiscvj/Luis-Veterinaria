using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using API.Dtos;
using API.Helpers;
using Dominio.Entities;
using Dominio.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace API.Services;

    public class UserService : IUserService
    {
        private readonly JWT _jwt;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPasswordHasher<Usuario> _passwordHasher;
        public UserService(IUnitOfWork unitOfWork, IOptions<JWT> jwt, IPasswordHasher<Usuario> passwordHasher)
        {
            _jwt = jwt.Value;
            _unitOfWork = unitOfWork;
            _passwordHasher = passwordHasher;
        }
        public async Task<string> RegisterAsync(RegisterDto registerDto)
        {
            var usuario = new Usuario
            {
                Email = registerDto.Email,
                Username = registerDto.Username,

            };

            usuario.Password = _passwordHasher.HashPassword(usuario, registerDto.Password);

            var usuarioExiste = _unitOfWork.Usuarios
                                                .Find(u => u.Username.ToLower() == registerDto.Username.ToLower())
                                                .FirstOrDefault();

            if (usuarioExiste == null)
            {
                 var rolPredeterminado = _unitOfWork.Roles
                                                     .Find(u => u.Nombre == Autorizacion.rol_predeterminado.ToString())
                                                     .First();
                try
                {
                    usuario.Roles.Add(rolPredeterminado);
                    _unitOfWork.Usuarios.Add(usuario);
                    await _unitOfWork.SaveAsync();

                    return $"El Usuario {registerDto.Username} ha sido registrado exitosamente";
                }

                catch (Exception ex)
                {
                    var message = ex.Message;
                    return $"Error: {message}";
                }
            }
            else
            {

                return $"El usuario con {registerDto.Username} ya se encuentra resgistrado.";
            }

        }

        public async Task<string> AddRoleAsync(AddRoleDto model)
        {
            var usuario = await _unitOfWork.Usuarios
                                .GetByUsernameAsync(model.Username);

            if (usuario == null)
            {
                return $"No existe algun usuario registrado con la cuenta olvido algun caracter?{model.Username}.";
            }

            var resultado = _passwordHasher.VerifyHashedPassword(usuario, usuario.Password, model.Password);

            if (resultado == PasswordVerificationResult.Success)
            {
                var rolExiste = _unitOfWork.Roles
                                                .Find(u => u.Nombre.ToLower() == model.Role.ToLower())
                                                .FirstOrDefault();

                if (rolExiste != null)
                {
                    var usuarioTieneRol = usuario.Roles
                                                    .Any(u => u.Id == rolExiste.Id);

                    if (usuarioTieneRol == false)
                    {
                        usuario.Roles.Add(rolExiste);
                        _unitOfWork.Usuarios.Update(usuario);
                        await _unitOfWork.SaveAsync();
                    }

                    return $"Rol {model.Role} agregado a la cuenta {model.Username} de forma exitosa.";
                }

                return $"Rol {model.Role} no encontrado.";
            }

            return $"Credenciales incorrectas para el ususario {usuario.Username}.";
        }
        public async Task<DataUserDto> GetTokenAsync(LoginDto model)
        {
            DataUserDto datosUsuarioDto = new DataUserDto();
            var usuario = await _unitOfWork.Usuarios
                            .GetByUsernameAsync(model.Username);

            if (usuario == null)
            {
                datosUsuarioDto.EstaAutenticado = false;
                datosUsuarioDto.Mensaje = $"No existe ningun usuario con el username {model.Username}.";
                return datosUsuarioDto;
            }

            var result = _passwordHasher.VerifyHashedPassword(usuario, usuario.Password, model.Password);
            if (result == PasswordVerificationResult.Success)
            {
                
               
                    datosUsuarioDto.EstaAutenticado = true;
                    JwtSecurityToken jwtSecurityToken = CreateJwtToken(usuario);
                    datosUsuarioDto.Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
                    datosUsuarioDto.Email = usuario.Email;
                    datosUsuarioDto.UserName = usuario.Username;
                    datosUsuarioDto.Roles = usuario.Roles
                                            .Select(u => u.Nombre)
                                            .ToList();


              
            if (usuario.RefreshTokens.Any(a => a.IsActive))
            {
                var activeRefreshToken = usuario.RefreshTokens.Where(a => a.IsActive == true).FirstOrDefault();
                datosUsuarioDto.RefreshToken = activeRefreshToken.Token;
                datosUsuarioDto.RefreshTokenExpiration = activeRefreshToken.Expires;
            }
            else
            {
                var refreshToken = CreateRefreshToken();
                datosUsuarioDto.RefreshToken = refreshToken.Token;
                datosUsuarioDto.RefreshTokenExpiration = refreshToken.Expires;
                usuario.RefreshTokens.Add(refreshToken);
                _unitOfWork.Usuarios.Update(usuario);
                await _unitOfWork.SaveAsync();
            }

            return datosUsuarioDto;
        }
        datosUsuarioDto.EstaAutenticado = false;
        datosUsuarioDto.Mensaje = $"Credenciales incorrectas para el usuario {usuario.Username}.";
        return datosUsuarioDto;

        }

        private JwtSecurityToken CreateJwtToken(Usuario usuario)
    {
        var roles = usuario.Roles;
        var roleClaims = new List<Claim>();
        foreach (var role in roles)
        {
            roleClaims.Add(new Claim("roles", role.Nombre));
        }
        var claims = new[]
        {
                                new Claim(JwtRegisteredClaimNames.Sub, usuario.Username),
                                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                                new Claim(JwtRegisteredClaimNames.Email, usuario.Email),
                                new Claim("uid", usuario.Id.ToString())
                        }
        .Union(roleClaims);
        var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
        var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);
        var jwtSecurityToken = new JwtSecurityToken(
            issuer: _jwt.Issuer,
            audience: _jwt.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(_jwt.DurationInMinutes),
            signingCredentials: signingCredentials);
        return jwtSecurityToken;
    }



    public async Task<LoginDto> UserLogin(LoginDto model)
    {
        var usuario = await _unitOfWork.Usuarios.GetByUsernameAsync(model.Username);
        var resultado = _passwordHasher.VerifyHashedPassword(usuario, usuario.Password, model.Password);

        if (resultado == PasswordVerificationResult.Success)
        {
            return model;
        }
        return null;
    }
     public async Task<DataUserDto> RefreshTokenAsync(string refreshToken)
    {
        var datosUsuarioDto = new DataUserDto();

        var usuario = await _unitOfWork.Usuarios
                        .GetByRefreshTokenAsync(refreshToken);

        if (usuario == null)
        {
            datosUsuarioDto.EstaAutenticado = false;
            datosUsuarioDto.Mensaje = $"Token is not assigned to any user.";
            return datosUsuarioDto;
        }

        var refreshTokenBd = usuario.RefreshTokens.Single(x => x.Token == refreshToken);

        if (!refreshTokenBd.IsActive)
        {
            datosUsuarioDto.EstaAutenticado = false;
            datosUsuarioDto.Mensaje = $"Token is not active.";
            return datosUsuarioDto;
        }
        //Revoque the current refresh token and
        refreshTokenBd.Revoked = DateTime.UtcNow;
        //generate a new refresh token and save it in the database
        var newRefreshToken = CreateRefreshToken();
        usuario.RefreshTokens.Add(newRefreshToken);
        _unitOfWork.Usuarios.Update(usuario);
        await _unitOfWork.SaveAsync();
        //Generate a new Json Web Token
        datosUsuarioDto.EstaAutenticado = true;
        JwtSecurityToken jwtSecurityToken = CreateJwtToken(usuario);
        datosUsuarioDto.Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
        datosUsuarioDto.Email = usuario.Email;
        datosUsuarioDto.UserName = usuario.Username;
        datosUsuarioDto.Roles = usuario.Roles
                                        .Select(u => u.Nombre)
                                        .ToList();
        datosUsuarioDto.RefreshToken = newRefreshToken.Token;
        datosUsuarioDto.RefreshTokenExpiration = newRefreshToken.Expires;
        return datosUsuarioDto;
    }




   private RefreshToken CreateRefreshToken()
    {
        var randomNumber = new byte[32];
        using (var generator = RandomNumberGenerator.Create())
        {
            generator.GetBytes(randomNumber);
            return new RefreshToken
            {
                Token = Convert.ToBase64String(randomNumber),
                Expires = DateTime.UtcNow.AddDays(10),
                Created = DateTime.UtcNow
            };
        }
    }


  }



  
