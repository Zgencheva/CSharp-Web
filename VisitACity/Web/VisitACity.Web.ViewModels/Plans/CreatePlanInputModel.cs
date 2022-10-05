namespace VisitACity.Web.ViewModels.Plans
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using VisitACity.Web.Infrastructure.ValidationAttributes;

    public class CreatePlanInputModel : IValidatableObject
    {
        [Required]
        [MaxLength(100)]
        [Display(Name = "Country")]
        public string CountryId { get; set; }

        [Required]
        [MaxLength(100)]
        [Display(Name = "City")]
        public string CityId { get; set; }

        [Display(Name="From date")]
        [DataType(DataType.Date)]
        [DateMinValueAttribute]
        public DateTime FromDate { get; set; }

        [Display(Name ="To date")]
        [DataType(DataType.Date)]
        [DateMinValueAttribute]
        public DateTime ToDate { get; set; }

        public IEnumerable<KeyValuePair<string, string>> CitiesItems { get; set; }

        public IEnumerable<KeyValuePair<string, string>> CountriesItems { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var res = DateTime.Compare(this.FromDate, this.ToDate);
            if (res > 0)
            {
                yield return new ValidationResult("Final date cannot be before starting date");
            }
        }
    }
}
