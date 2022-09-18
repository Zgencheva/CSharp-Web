namespace VisitACity.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using VisitACity.Data.Common.Models;

    public class Country : BaseModel<int>
    {
        public Country()
        {
            this.Cities = new HashSet<City>();
            this.Plans = new HashSet<Plan>();
        }

        public string Name { get; set; }

        public virtual ICollection<City> Cities { get; set; }

        public virtual ICollection<Plan> Plans { get; set; }
    }
}
