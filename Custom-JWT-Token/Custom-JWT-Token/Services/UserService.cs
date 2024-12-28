using Custom_JWT_Token.Models;
using Custom_JWT_Token.Repository;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Custom_JWT_Token.Services
{
    public class UserService : IUserService
    {
        private readonly  UserRepository userRepository;

        private List<User> _users = new List<User> {
            new User {
                Id = "1",
                FirstName = "Rushikesh",
                userRole= new List<Role>{Role.Customer},
                LastName = "Telsinge",
                UserName = "rushikeshrt",
                Password = "test123"
            },
            new User {
                Id = "2",
                FirstName = "Durvesh",
                LastName = "Kulkarni",
                UserName = "durveshdk",
                Password = "test123"
            }
        };

        public IEnumerable<User> GetAll()
        {
            return _users;
        }

        public User GetById(string id)
        {
            var foundUser = _users.FirstOrDefault(x => x.Id == id);
            return foundUser;
        }

        public string AddNewUser(User user)
        {
            return userRepository.AddNewUser(user);
        }
    }
}
