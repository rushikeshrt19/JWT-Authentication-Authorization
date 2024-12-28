using Custom_JWT_Token.Models;

namespace Custom_JWT_Token.Repository
{
    public interface IUserRepository
    {
        User GetUserById(int id);
        User AuthenticateUser(User user);
        
        string AddNewUser(User user);

    }
}
