namespace FitApp.Models
{
    public class Register
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string ConfirmPassword { get; set; }

        public UserType UserType { get; set; }
    }
}