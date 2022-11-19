namespace VisitACity.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using VisitACity.Data.Common.Models;

    public class Image : BaseModel<string>
    {
        public Image()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        [Required]
        [MaxLength(10)]
        public string Extension { get; set; }

        [Required]
        public int AttractionId { get; set; }

        public virtual Attraction Attraction { get; set; }
    }
}
