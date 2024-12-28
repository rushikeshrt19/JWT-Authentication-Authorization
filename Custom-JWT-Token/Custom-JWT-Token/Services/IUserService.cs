using Custom_JWT_Token.Models;

namespace Custom_JWT_Token.Services
{
    public interface IUserService
    {
        User GetById(string id);

        IEnumerable<User> GetAll();

        string AddNewUser(User user);  
    }
}
