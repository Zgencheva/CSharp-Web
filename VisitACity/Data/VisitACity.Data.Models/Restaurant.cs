﻿using System;
using System.Collections.Generic;
using VisitACity.Data.Common.Models;

namespace VisitACity.Data.Models
{
    public class Restaurant : BaseModel<int>, IDeletableEntity
    {
        public Restaurant()
        {
            this.Reviews = new HashSet<RestaurantReview>();
            this.Plans = new HashSet<Plan>();
        }

        public string Name { get; set; }

        public string ImageUrl { get; set; }

        public string Address { get; set; }

        public double Raiting { get; set; }

        public int CityId { get; set; }

        public virtual City City { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public virtual ICollection<RestaurantReview> Reviews { get; set; }

        public virtual ICollection<Plan> Plans { get; set; }
    }

}