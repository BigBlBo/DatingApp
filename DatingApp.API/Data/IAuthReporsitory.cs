using System.Threading.Tasks;
using DatingApp.API.Models;

namespace DatingApp.API.Data
{
    public interface IAuthReporsitory
    {
         Task<User> Register(User user, string password);
         Task<User> LogIn(string username, string password);
         Task<bool> UserExists(string username);
    }
}