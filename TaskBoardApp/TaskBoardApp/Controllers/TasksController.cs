using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TaskBoardApp.Data;
using TaskBoardApp.Models;

namespace TaskBoardApp.Controllers
{
    [Authorize]
    public class TasksController : Controller
    {
        private readonly TaskBoardAppDbContext context;

        public TasksController(TaskBoardAppDbContext context)
        {
            this.context = context;
        }

        public IActionResult Create()
        {
            TaskFormModel taskModel = new TaskFormModel()
            {
                Boards = GetBoards()
            };

            return View(taskModel);        
        }

        [HttpPost]
        public IActionResult Create(TaskFormModel taskModel)
        {
            if (!GetBoards().Any(b=> b.Id == taskModel.BoardId))
            {
                this.ModelState.AddModelError(nameof(taskModel.BoardId), "Board does not exist");
            }
            string currentUserId = GetUserId();

            var task = new Data.Entities.Task()
            {
                Title = taskModel.Title,
                Description = taskModel.Description,
                CreatedOn = DateTime.UtcNow,
                BoardId = taskModel.BoardId,
                OwnerId = currentUserId,
            };

            this.context.Tasks.Add(task);
            this.context.SaveChanges();

            var boards = this.context.Boards;
            return RedirectToAction("All", "Boards");
        }

        public IActionResult Details(int id)
        {
            var model = this.context
                .Tasks
                .Where(t => t.Id == id)
                .Select(t => new TaskDetailsViewModel()
                {
                    Id = t.Id,
                    Title = t.Title,
                    Description = t.Description,
                    CreatedOn = t.CreatedOn.ToString("dd/MM/yyyy HH:mm"),
                    Board = t.Board.Name,
                    Owner = t.Owner.UserName
                })
                 .FirstOrDefault();
            if (model == null)
            {
                return BadRequest();
            }
               
            return this.View(model);
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var task = context.Tasks.Find(id);
            if (task == null)
            {
                return BadRequest();
            }
            string currentUserId = GetUserId();
            if (currentUserId != task.OwnerId)
            {
                return Unauthorized();
            }
            TaskFormModel taskModel = new TaskFormModel()
            {
                Title = task.Title,
                Description = task.Description,
                BoardId = task.BoardId,
                Boards = GetBoards()
            };

            return this.View(taskModel);
        }

        [HttpPost]
        public IActionResult Edit(int id, TaskFormModel taskModel)
        {
            var task = context.Tasks.Find(id);
            if (task == null)
            {
                return BadRequest();
            }
            string currentUserId = GetUserId();
            if (currentUserId != task.OwnerId)
            {
                return Unauthorized();
            }
            if (!GetBoards().Any(b=> b.Id == taskModel.BoardId))
            {
                this.ModelState.AddModelError(nameof(taskModel.BoardId), "Board does not exist");
            }
           
            task.Title = taskModel.Title;
            task.Description = taskModel.Description;
            task.BoardId = taskModel.BoardId;

            this.context.SaveChanges();
            return this.RedirectToAction("All", "Boards");
        }

        public IActionResult Delete(int id)
        {
            var task = context.Tasks.Find(id);
            if (task == null)
            {
                return BadRequest();
            }
            string currentUserId = GetUserId();
            if (currentUserId != task.OwnerId)
            {
                return Unauthorized();
            }

            TaskViewModel taskModel = new TaskViewModel()
            {
                Id= task.Id,
                Title = task.Title,
                Description = task.Description,
            };

            return View(taskModel);
        }
        [HttpPost]
        public IActionResult Delete(TaskViewModel taskModel)
        {
            var task = context.Tasks.Find(taskModel.Id);
            if (task == null)
            {
                return BadRequest();
            }
            string currentUserId = GetUserId();
            if (currentUserId != task.OwnerId)
            {
                return Unauthorized();
            }

            this.context.Tasks.Remove(task);
            this.context.SaveChanges();
            return RedirectToAction("All", "Boards");
        }
        private string GetUserId()
        {
            return this.User.FindFirstValue(ClaimTypes.NameIdentifier);
        }

        private IEnumerable<TaskBoardModel> GetBoards()
        {
            return this.context.Boards.Select(b => new TaskBoardModel()
            {
                Id = b.Id,
                Name = b.Name,
            });
        }
    }
}
