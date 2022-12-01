namespace VisitACity.Common
{
    public static class ModelConstants
    {
        public const int DefaultPageNumber = 1;
        public const int DefaultPageSize = 6;
        public const int UrlMaxLength = 150;

        public const string RestaurantsRadioOption = "Restaurants";
        public const string AttractionsRadioOption = "Attractions";

        public const string PhotoExtensionNotAllowed = "This photo extension is not allowed!";
        public const string TravellingDateInThePast = "Travelling date cannot be in the past";
        public const string FinalDateBeforeStartDateError = "Final date cannot be before starting date";
        public const string PricePositiveNumber = "Price must be grater than 0";

        public const string NameLengthError = "Name must be between {2} and {1} symbols";
        public const string AddressLengthError = "Address must be between {2} and {1} symbols";
        public const string PhoneLengthError = "Phone number must be between {2} and {1} symbols";
        public const string DescriptionLengthError = "Description must be between {2} and {1} symbols";

        public const string FromDateDisplay = "From date";
        public const string ToDateDisplay = "To date";

        public class Attraction
        {
            public const int NameMaxSize = 100;
            public const int NameMinSize = 3;
            public const int AddressMaxSize = 150;
            public const int AddressMinSize = 10;
            public const int DescriptionMaxLength = 500;
            public const int DescriptionMinLength = 10;
            public const int TypeMaxLength = 500;
        }

        public class Country
        {
            public const int NameMaxSize = 100;
            public const int NameMinSize = 2;

            public const string CountryExists = "Country {0} already exists";
            public const string CountryDoesNotExists = "Country {0} does not exists";
            public const string CountryDisplay = "Country";
        }

        public class City
        {
            public const int NameMaxSize = 100;
            public const int NameMinSize = 2;

            public const string CityExists = "City {0} already exists";
            public const string CityDoesNotExist = "City {0} does not exists";
            public const string CityDisplay = "City";
        }

        public class Restaurant
        {
            public const int NameMaxSize = 150;
            public const int NameMinSize = 2;
            public const int AddressMaxSize = 150;
            public const int AddressMinSize = 10;
            public const int PhoneMaxLength = 15;
            public const int PhoneMinLength = 5;
        }

        public class Review
        {
            public const int RateMin = 1;
            public const int RateMax = 5;
            public const int CommentMinLength = 5;
            public const int CommentMaxLength = 400;

            public const string CommentLengthError = "Comment must be between {2} and {1} symbols";
        }

        public class Account
        {
            public const int NameMaxSize = 20;
            public const int NameMinSize = 3;
            public const int PasswordMaxSize = 100;
            public const int PasswordMinSize = 6;

            public const string FirstNameDisplay = "First name";
            public const string LastNameDisplay = "Last name";
            public const string PasswordDisplay = "Password";
            public const string ConfirmPasswordDisplay = "Password";
            public const string EmailDisplay = "Email";
            public const string PasswordError = "The password and confirmation password do not match.";
            public const string PasswordLengthError = "The Password must be at least {2} and at max {1} characters long.";

            public const string UserCreatedAccountLogger = "User created a new account with password.";
        }
    }
}
