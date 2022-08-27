using Suls.Data;
using Suls.ViewModels;
using Suls.ViewModels.Problems;
using Suls.ViewModels.Users.Problems;
using System.Collections.Generic;

namespace Suls.Sevices
{
    public interface IProblemService
    {
        public string CreateProblem(AddProblemModel model);
        public ICollection<HomePageProblemViewModel> GetAll();
        public ProblemViewModel GetProblemDetils(string id);
    }
}
