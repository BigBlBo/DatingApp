using System;
using System.Linq;
using System.Threading.Tasks;
using DatingApp.API.Models;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.API.Data
{
    public class AuthReporsitory : IAuthReporsitory
    {
        private readonly DataContext _context;

        public AuthReporsitory(DataContext context)
        {
            this._context = context;
        }

        public async Task<User> LogIn(string username, string password)
        {
            var user = await this._context.Users.FirstOrDefaultAsync(x => x.UserName == username);
            if(user == null)
            {
                return null;
            }
            
            if(!VerifyPasswordHash(user, password))
            {
                return null;
            }

            return user;
        }


        public async Task<User> Register(User user, string password)
        {
            byte[] passwordHash, passwordSalt;
            CreatePaswordHash(password, out passwordHash, out passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            
            await this._context.Users.AddAsync(user);
            await this._context.SaveChangesAsync();

            return user;
        }

        public async Task<bool> UserExists(string username)
        {
            if(await this._context.Users.AnyAsync(x => x.UserName == username))
            {
                return true;
            }

            return false;
        }

        private bool VerifyPasswordHash(User user, string password)
        {
            using(var hmac = new System.Security.Cryptography.HMACSHA512(user.PasswordSalt))
            {
                if(user.PasswordHash.SequenceEqual(hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password))))
                {
                    return true;
                }
            }

            return false;
        }

        private void CreatePaswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using(var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
    }
}