using System;
using System.Collections.Generic;
using System.Text;

namespace Readooks.BusinessLogicLayer.Dtos.Users
{
    public class UserInfoDto
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int NumberOfCoins { get; set; }
        public int AvailableSpotsOnBookshelf { get; set; }
        public int NumberOfOpenBooks { get; set; }
        public int NumberOfFinishedBooks { get; set; }
    }
}
