﻿namespace VisitACity.Web.ViewModels.Accounts
{
    using System.ComponentModel.DataAnnotations;

    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;

        [Required]
        [Compare(nameof(ConfirmedPassword))]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;

        [Required]
        [DataType(DataType.Password)]
        public string ConfirmedPassword { get; set; } = null!;

        [Required]
        [StringLength(20, MinimumLength = 2)]
        public string FirstName { get; set; } = null!;

        [Required]
        [StringLength(20, MinimumLength = 2)]
        public string LastName { get; set; } = null!;
    }
}
