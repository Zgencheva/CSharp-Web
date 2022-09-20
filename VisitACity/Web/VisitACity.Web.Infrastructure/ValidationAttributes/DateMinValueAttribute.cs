using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisitACity.Web.Infrastructure.ValidationAttributes
{
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
