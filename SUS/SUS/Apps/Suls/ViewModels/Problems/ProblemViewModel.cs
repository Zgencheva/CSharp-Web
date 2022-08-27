using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Suls.ViewModels.Problems
{
    public class ProblemViewModel
    {
        public string Name { get; set; }

        public IEnumerable<ProblemSubmissionsViewModel> Submissions { get; set; }
    }
}
