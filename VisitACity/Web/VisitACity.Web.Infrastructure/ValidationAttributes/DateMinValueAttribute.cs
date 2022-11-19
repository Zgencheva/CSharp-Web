namespace VisitACity.Web.Infrastructure.ValidationAttributes
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using VisitACity.Common;

    public class DateMinValueAttribute : ValidationAttribute
    {
        public DateMinValueAttribute()
        {
            this.ErrorMessage = ModelConstants.TravellingDateInThePast;
        }

        public override bool IsValid(object value)
        {
            if (value is DateTime datetime)
            {
                if (datetime >= DateTime.UtcNow)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
