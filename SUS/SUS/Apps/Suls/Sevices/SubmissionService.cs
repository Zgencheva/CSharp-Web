using Suls.Data;
using Suls.ViewModels.Submissions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Suls.Sevices
{
    public class SubmissionService : ISubmissionService
    {
        private readonly ApplicationDbContext db;

        public SubmissionService(ApplicationDbContext db)
        {
            this.db = db;
        }
        public SubmissionViewModel CreateSubmission(string Id)
        {
        
            Console.WriteLine($"in create submission {Id}");
            var problem = db.Problems.FirstOrDefault(x => x.Id == Id);
               var model =  new SubmissionViewModel 
                {
                    ProblemId = problem.Id,
                    Name = problem.Name,
                };
            return model;
        }

        public void AddSubmissionToProblem(CreateSubmissionInputModel model, string userId)
        {
            var problem = db.Problems.FirstOrDefault(x=> x.Id == model.ProblemId);
            var user = db.Users.FirstOrDefault(x=> x.Id == userId);
            var rand = new Random();
            problem.Submissions.Add(new Submission
            {
                AchievedResult = rand.Next(0, problem.Points),
                CreatedOn = DateTime.UtcNow,
                User = user,
                Code = model.Code,
            });

            db.SaveChanges();
        }

        public void DeleteSubmission(string id)
        {
            var submission = db.Submissions.FirstOrDefault(x => x.Id == id);
            if (submission != null)
            {
                db.Submissions.Remove(submission);
                db.SaveChanges();
            }
            
            
        }
    }
}
