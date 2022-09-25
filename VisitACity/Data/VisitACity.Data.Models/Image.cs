namespace VisitACity.Data.Models
{
    using System;

    using VisitACity.Data.Common.Models;

    public class Image : BaseModel<string>
    {
        public Image()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string Extension { get; set; }

        public int AttractionId { get; set; }

        public virtual Attraction Attraction { get; set; }

    }
}
