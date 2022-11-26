namespace VisitACity.Common
{
    public class TempDataMessageConstants
    {

        public const string ThankForComment = "Thank you for your comment!";
        public const string CityAdded = "City {0} added successfully.";
        public const string CityDeleted = "City {0} deleted successfully.";
        public const string CountryAdded = "Country {0} added successfully.";
        public const string CountryDeleted = "Country {0} deleted successfully.";

        public class Attraction
        {
            public const string ExistingAttractionToThePlan = "Attraction already in your plan.";
            public const string AttractionAddedToPlan = "Attraction added successfully to your plan.";
            public const string AttractionAdded = "Attraction added successfully";
            public const string AttractionDeleted = "Attraction deleted successfully";
            public const string AttractionUpdated = "Attraction updated successfully.";

        }

        public class Restaurant
        {
            public const string ExistingRestaurantToThePlan = "Restaurant already in your plan.";
            public const string RestaurantAddedToPlan = "Restaurant added successfully to your plan.";
            public const string RestaurantAdded = "Restaurant added successfully";
            public const string RestaurantUpdated = "Restaurant updated successfully";
            public const string RestaurantDeleted = "Restaurant deleted successfully";
        }

        public class Plan
        {
            public const string PlanDeleted = "Plan deleted successfully";
            public const string NoPlansInTheCity = "You have no plans in this city. Please create it";
            public const string ExistingUpcomingPlanToTheCity = "You already have upcoming plan to this city!";
        }
    }
}
