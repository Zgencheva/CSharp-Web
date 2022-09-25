namespace VisitACity.Web.Infrastructure.ValidationAttributes
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class DateMinValueAttribute : ValidationAttribute
    {
        public DateMinValueAttribute()
        {
            this.ErrorMessage = $"Travelling date cannot be in the past";
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
