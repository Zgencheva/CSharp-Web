// ReSharper disable VirtualMemberCallInConstructor
namespace VisitACity.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Identity;
    using VisitACity.Data.Common.Models;

    public class ApplicationUser : IdentityUser, IAuditInfo, IDeletableEntity
    {
        public ApplicationUser()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Roles = new HashSet<IdentityUserRole<string>>();
            this.Claims = new HashSet<IdentityUserClaim<string>>();
            this.Logins = new HashSet<IdentityUserLogin<string>>();

            this.Reviews = new HashSet<Review>();
            this.Plans = new HashSet<Plan>();
            this.AttractionsReviewed = new HashSet<Attraction>();
        }

        [StringLength(20)]
        public string? FirstName { get; set; }

        [StringLength(20)]
        public string? LastName { get; set; }

        // Audit info
        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        // Deletable entity
        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public virtual ICollection<IdentityUserRole<string>> Roles { get; set; }

        public virtual ICollection<IdentityUserClaim<string>> Claims { get; set; }

        public virtual ICollection<IdentityUserLogin<string>> Logins { get; set; }

        public virtual ICollection<Review> Reviews { get; set; }

        public virtual ICollection<Attraction> AttractionsReviewed { get; set; }

        public virtual ICollection<Plan> Plans { get; set; }
    }
}
