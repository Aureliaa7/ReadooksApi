using System.ComponentModel.DataAnnotations;

namespace Readooks.BusinessLogicLayer.ViewModels
{
    public class UserRegistrationVm
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Password { get; set; }
        [Required]
        public string Email { get; set; }
    }
}
