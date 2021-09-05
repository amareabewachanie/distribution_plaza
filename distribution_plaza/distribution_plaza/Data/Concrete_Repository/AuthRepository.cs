using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using distribution_plaza.Data.Repository;
using distribution_plaza.Models;
using Microsoft.EntityFrameworkCore;

namespace distribution_plaza.Data.Concrete_Repository
{
    public class AuthRepository:IAuthRepository
    {
        private readonly DataContext _dataContext;

        public AuthRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public async Task<User> Register(User user, string password)
        {
            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(password,out passwordHash,out passwordSalt);
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            await _dataContext.Users.AddAsync(user);
            await _dataContext.SaveChangesAsync();
            return user;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac=new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }

        public async Task<User> Login(string userName, string password)
        {
            var user = await _dataContext.Users.FirstOrDefaultAsync(x => x.UserName == userName);
            if (user == null)
                return null;
            if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                return null;
            return user;
        }

        private bool VerifyPasswordHash(string password, byte[] userPasswordHash, byte[] userPasswordSalt)
        {
            using (var hmac=new HMACSHA512(userPasswordSalt))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != userPasswordHash[i])
                        return false;
                }
            }

            return true;
        }

        public async Task<bool> UserExists(string userName)
        {
            if (await _dataContext.Users.AnyAsync(a => a.UserName == userName))
                return true;
            return false;
        }
    }
}
