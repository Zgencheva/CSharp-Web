using System;
using System.Collections;
using System.Collections.Generic;
using VisitACity.Data.Common.Models;
using VisitACity.Data.Models.Enums;

namespace VisitACity.Data.Models
{
    public class Attraction : BaseModel<int>, IDeletableEntity
    {
        public Attraction()
        {
            this.Reviews = new HashSet<AttractionReview>();
            this.Plans = new HashSet<Plan>();
        }

        public string Name { get; set; }

        public AttractionType Type { get; set; }

        public decimal Price { get; set; }

        public string ImageUrl { get; set; }

        public string Address { get; set; }

        public string Description { get; set; }

        public int CityId { get; set; }

        public virtual City City { get; set; }

        public bool IsDeleted { get ; set; }

        public DateTime? DeletedOn { get; set; }

        public virtual ICollection<AttractionReview> Reviews { get; set; }

        public virtual ICollection<Plan> Plans { get; set; }

    }
}