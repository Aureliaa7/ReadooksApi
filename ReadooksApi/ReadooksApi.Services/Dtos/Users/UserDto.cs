using System;

namespace Readooks.BusinessLogicLayer.Dtos.Users
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int NumberOfCoins { get; set; }
        public int AvailableSpotsOnBookshelf { get; set; }
    }
}
