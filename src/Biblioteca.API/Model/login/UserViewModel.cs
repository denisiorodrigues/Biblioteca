using System.Collections.Generic;

namespace Biblioteca.API.Model.login
{
    public class UserViewModel
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public IEnumerable<ClaimViewModel> Claims { get; set; }
    }
}