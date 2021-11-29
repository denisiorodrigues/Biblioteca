namespace Biblioteca.API.Model.login
{
    public class LoginResponseViewModel
    {
        public string AccessToken { get; set; }
        public double ExpiresIn { get; set; }
        public UserViewModel User { get; set; }
    }
}