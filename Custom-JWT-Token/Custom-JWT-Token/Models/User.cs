using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Custom_JWT_Token.Models
{
    public class User
    {
        [Key]
        public string Id { get; set; }

        public string FirstName{ get; set; }

        public string LastName { get; set; }

        public string UserName { get; set; }

        public List<Role> userRole { get; set; }

        [JsonIgnore]
        public string Password { get; set; }

    }
}
