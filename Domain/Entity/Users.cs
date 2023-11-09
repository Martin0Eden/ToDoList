using System.ComponentModel.DataAnnotations;

namespace Domain.Entity
{
    public class Users
    {
        [Key]
        public string UserId { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }

        public Users(string id, string name, string password, string email, string role)
        {
            UserId = id;
            Name = name;
            Password = password;
            Email = email;
            Role = role;
        }

        public Users() { }
    }
}
