using System;
using System.Linq;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using WebApi.Application.UserOperations.Commands.CreateToken;
using WebApi.DBOperations;
using WebApi.TokenOperations.Models;

namespace WebApi.Application.UserOperations.Commands.RefreshToken
{
    public class RefreshTokenCommand
    {
        public  string RefreshToken { get; set; }
        private readonly IBookStoreDbContext _dbContext;
        private readonly IConfiguration _configuration;

        public RefreshTokenCommand(IBookStoreDbContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _configuration = configuration;
        }

        public Token Handle()
        {
            var user = _dbContext.Users.FirstOrDefault(c => c.RefreshToken== RefreshToken && c.RefreshTokenExpireDate > DateTime.Now);
            if (user is not null)
            {
                //Token yarat
                TokenHandler handler = new TokenHandler(_configuration);
                Token token = handler.CreateAccessToken(user);

                user.RefreshToken = token.RefreshToken;
                user.RefreshTokenExpireDate = token.Expiration.AddMinutes(5);
                _dbContext.SaveChanges();
                return token;
            }
            else
                throw new InvalidOperationException("Kullanıcı Adı - Şifre hatalı");
        }

    }
}

