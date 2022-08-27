using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Suls.ViewModels.Problems
{
    public class ProblemSubmissionsViewModel
    {
        public string Username { get; set; }

        public string SubmissionId { get; set; }

        public DateTime CreatedOn { get; set; }

        public int AchievedResult { get; set; }

        public int MaxPoints { get; set; }

        public int Percentage => (int)Math.Round(this.AchievedResult * 100.0M / this.MaxPoints);
    }
}
