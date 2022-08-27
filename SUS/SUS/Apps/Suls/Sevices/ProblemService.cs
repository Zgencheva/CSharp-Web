using Suls.Data;
using Suls.ViewModels;
using Suls.ViewModels.Problems;
using Suls.ViewModels.Users.Problems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Suls.Sevices
{
    public class ProblemService : IProblemService
    {
        private readonly ApplicationDbContext db;

        public ProblemService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public string CreateProblem(AddProblemModel model)
        {
            var problem = new Problem
            {
                Name = model.Name,
                Points = model.Points,
            };

            db.Problems.Add(problem);
            db.SaveChanges();
            return problem.Id;
        }

        public ICollection<HomePageProblemViewModel> GetAll()
        {
            var problems = db.Problems.Select(x => new HomePageProblemViewModel
            {
                Id = x.Id,
                Name = x.Name,
                Count = x.Submissions.Count(),
            })
                .ToList();

            return problems;
        }
        public ProblemViewModel GetProblemDetils(string id)
        {
            var problem = db.Problems.Where(x => x.Id == id)
                .Select(x => new ProblemViewModel
                {
                    Name = x.Name,
                    Submissions = x.Submissions.Select(s => new ProblemSubmissionsViewModel
                    {
                        Username = s.User.Username,
                        SubmissionId = s.Id,
                        CreatedOn = s.CreatedOn,
                        AchievedResult = s.AchievedResult,
                        MaxPoints = x.Points,

                    })

                }).FirstOrDefault();

            return problem;
        }

    }
}
