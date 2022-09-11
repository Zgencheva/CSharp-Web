using System;
using VisitACity.Data.Common.Models;

namespace VisitACity.Data.Models
{
    public abstract class Review : BaseModel<int>, IDeletableEntity
    {

        public double Rating { get; set; }

        public string Content { get; set; }

        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set ; }
    }
}