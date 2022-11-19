namespace VisitACity.Data
{
    using Microsoft.AspNetCore.Identity;
    using VisitACity.Common;

    public static class IdentityOptionsProvider
    {
        public static void GetIdentityOptions(IdentityOptions options)
        {
            options.Password.RequireDigit = false;
            options.Password.RequireLowercase = false;
            options.Password.RequireUppercase = false;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequiredLength = ModelConstants.Account.PasswordMinSize;
            options.SignIn.RequireConfirmedAccount = false;
        }
    }
}
