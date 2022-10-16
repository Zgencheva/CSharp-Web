using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;
using TaskBoardApp.Data;
using TaskBoardApp.Models;

namespace TaskBoardApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly TaskBoardAppDbContext context;

        public HomeController(TaskBoardAppDbContext context)
        {
            this.context = context;
        }

        public IActionResult Index()
        {
            var taskBoards = this.context
                .Boards
                .Select(b => b.Name)
                .Distinct();
            var taskCounts = new List<HomeBoardModel>();
            foreach (var boardName in taskBoards)
            {
                var tasksInBoardCount = this.context.Tasks.Where(t => t.Board.Name == boardName).Count();
                taskCounts.Add(new HomeBoardModel()
                {
                    BoardName = boardName,
                    TasksCount = tasksInBoardCount,
                });
            }
                var userTasksCount = -1;

                if (this.User.Identity.IsAuthenticated)
                {
                    var currentUser = User.FindFirstValue(ClaimTypes.NameIdentifier);
                    userTasksCount = this.context.Tasks.Where(t=> t.OwnerId == currentUser).Count();
                }
            var homeModel = new HomeViewModel()
            {
                AllTasksCount = this.context.Tasks.Count(),
                BoardsWithTasksCount = taskCounts,
                UserTasksCount = userTasksCount
           
            };
            return View(homeModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}