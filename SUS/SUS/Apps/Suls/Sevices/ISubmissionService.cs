using Suls.ViewModels.Submissions;

namespace Suls.Sevices
{
    public interface ISubmissionService
    {
        public SubmissionViewModel CreateSubmissionView(string Id);

        public void AddSubmissionToProblem(CreateSubmissionInputModel model, string userId);
        void DeleteSubmission(string id);
    }
}
