using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisitACity.Data.Common.Models;

namespace VisitACity.Data.Models
{
    public class AttractionsPlans : BaseDeletableModel<int>
    {
        public int AttractionId { get; set; }

        public virtual Attraction Attraction { get; set; }

        public int PlanId { get; set; }

        public virtual Plan Plan { get; set; }

    }
}
