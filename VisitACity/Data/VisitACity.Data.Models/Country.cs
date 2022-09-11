using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisitACity.Data.Common.Models;

namespace VisitACity.Data.Models
{
    public class Country : BaseModel<int>
    {
        public Country()
        {
            this.Cities = new HashSet<City>();
        }

        public string Name { get; set; }

        public virtual ICollection<City> Cities { get; set; }
    }
}
