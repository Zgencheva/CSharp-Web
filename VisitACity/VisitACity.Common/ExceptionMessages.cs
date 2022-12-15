namespace VisitACity.Common
{
    public static class ExceptionMessages
    {
        public const string NotExistingUser = "User does not exist";
        public const string InvalidComment = "Comment does not exist";
        public const string InvalidSubjectAndMessage = "Subject and message should be provided.";
        public const string DbException = "We were not able to apply changes.";
        public const string DbFailedUponCreatePlan = "User {0} cound not create his plan due to DB error";
        public const string DbFailedUponAddingReview = "User {0} cound not create his plan due to DB error";

        public class Attraction
        {
            public const string InvalidAttraction = "Invalid attraction";
            public const string InvalidAttractionType = "Invalid attraction type";
        }

        public class Restaurant
        {
            public const string InvalidRestaurant = "Invalid restaurant";
        }

        public class City
        {
            public const string NotExists = "Invalid city";
        }

        public class Image
        {
            public const string NotExists = "Invalid image";
        }

        public class Country
        {
            public const string NotExists = "Invalid country";
        }

        public class Plan
        {
            public const string NotExists = "Invalid plan";
            public const string NotExistingAttraction = "Not existing attraction in the plan";
            public const string NotExistingRestaurant = "Not existing restaurant in the plan";
        }
    }
}
