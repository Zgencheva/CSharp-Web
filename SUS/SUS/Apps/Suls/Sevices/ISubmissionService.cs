using Suls.ViewModels.Submissions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Suls.Sevices
{
    public interface ISubmissionService
    {
        public SubmissionViewModel CreateSubmission(string Id);

        public void AddSubmissionToProblem(CreateSubmissionInputModel model, string userId);
        void DeleteSubmission(string id);
    }
}
