
using System.Linq;

namespace HospitalManagementSystem.Database
{
    public class AuthService
    {
        private readonly YourDbContext _dbContext;

        public AuthService(YourDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public User AuthenticateUser(string username, string password)
        {
            return _dbContext.Users.FirstOrDefault(u => u.Username == username && u.Password == password);
        }
    }
}
