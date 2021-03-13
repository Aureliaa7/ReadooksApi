﻿using System;
using System.ComponentModel.DataAnnotations;

namespace Readooks.BusinessLogicLayer.ViewModels
{
    public class UserVm
    {
        public Guid Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Password { get; set; }
        [Required]
        public string Email { get; set; }

        public int NumberOfCoins { get; set; }
        public int AvailableSpotsOnBookshelf { get; set; }
    }
}
