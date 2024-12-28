using Custom_JWT_Token.Models;
using System.Runtime.Intrinsics.X86;

namespace Custom_JWT_Token.Repository
{
    public class UserRepository : IUserRepository
    {
        private  MyDbContext _context;
        public UserRepository(MyDbContext _context)
        {
            this._context = _context;
        }

        public string AddNewUser(User user)
        {   
            User foundUser= this.AuthenticateUser(user);
            if (foundUser==null)
            {
                _context.User.Add(user);
                int rowsAffected = _context.SaveChanges();
                return "User Added Successfully";
            }  
            else
            {
                return "User Already Exists";
            }
        }

        public User AuthenticateUser(User user)
        {
            var foundUser = _context.User.FirstOrDefault(x => x.UserName.Equals(user.UserName) && x.Password.Equals(user.Password));
            return foundUser;
        }

        public User GetUserById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
