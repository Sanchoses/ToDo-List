using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDo_List.Context;
using ToDo_List.Models;

namespace ToDo_List.Controllers
{
    public class ToDoController : Controller
    {
        private readonly ToDoContext context;

        public ToDoController(ToDoContext context)
        {
            this.context = context;
        }

        //GET /
        [Authorize]
        public async Task<IActionResult> Index()
        {
            IQueryable<ToDoTask> items = from i in context.Tasks
                                         where i.UserId == AccountController.user.Id
                                         orderby i.DeadLine 
                                         select i; 
            List<ToDoTask> tasks = await items.ToListAsync();

            return View(tasks);
        }

        //GET /todo/create
        public IActionResult Create() => View();


        //POST /todo/create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ToDoTask task)
        {
            if (ModelState.IsValid)
            {
                task.UserId = AccountController.user.Id;
                context.Add(task);
                await context.SaveChangesAsync();

                TempData["Success"] = "The new task was created!";
                return RedirectToAction("Index");
            }
            return View(task);
        }



        //GET /todo/edit/id
        public async Task<IActionResult> Edit(int id)
        {
            ToDoTask task = await context.Tasks.FindAsync(id);

            if (task == null)
                return NotFound();
            return View(task);
        }


        //POST /todo/edit/id
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ToDoTask task)
        {
            if (ModelState.IsValid)
            {
                task.UserId = AccountController.user.Id;
                context.Update(task);
                await context.SaveChangesAsync();

                TempData["Success"] = "The task was updated!";
                return RedirectToAction("Index");
            }
            return View(task);
        }

        //GET   /todo/delete/id
		public async Task<IActionResult> Delete(int id)
		{
			ToDoTask item = await context.Tasks.FindAsync(id);
           
            if (item == null)
                TempData["Error"] = "The item does not exist";
            else
            {
                context.Tasks.Remove(item);
                await context.SaveChangesAsync();

                TempData["Success"] = "The task was deleted !";
            }
            return RedirectToAction("Index");
        }
	}
}
