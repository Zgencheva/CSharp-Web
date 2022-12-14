namespace VisitACity.Web.ViewModels.Plans
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Mvc.Rendering;
    using VisitACity.Common;
    using VisitACity.Web.Infrastructure.ValidationAttributes;

    public class CreatePlanInputModel : IValidatableObject
    {
        [Display(Name = ModelConstants.FromDateDisplay)]
        [DataType(DataType.Date)]
        [DateMinValueAttribute]
        public DateTime FromDate { get; set; }

        [Display(Name = ModelConstants.ToDateDisplay)]
        [DataType(DataType.Date)]
        [DateMinValueAttribute]
        public DateTime ToDate { get; set; }

        [Required]
        [Display(Name = ModelConstants.City.CityDisplay)]
        public int CityId { get; set; }

        public List<SelectListItem> Cities { get; set; }

        [Required]
        [Display(Name = ModelConstants.Country.CountryDisplay)]
        public int CountryId { get; set; }

        public List<SelectListItem> Countries { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var res = DateTime.Compare(this.FromDate, this.ToDate);
            if (res > 0)
            {
                yield return new ValidationResult(ModelConstants.FinalDateBeforeStartDateError);
            }
        }
    }
}
