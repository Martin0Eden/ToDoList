using Microsoft.AspNetCore.Mvc;
using ToDoList.Models;
using System.IO;
using System.Net;
using System.Formats.Asn1;
using System.Xml.Linq;
using Domain.Entity;
using DAL;
using Service;

namespace ToDoList.Controllers
{
    public class UpController : Controller
    {
        private readonly ILogger<UpController> _logger;

        public UpController(ILogger<UpController> logger)
        {
            _logger = logger;
        }

        public IActionResult Upload([FromForm] TaskClass model, [FromForm] string idtask)
        {
            if (model.NewFileUpload != null)
            {

               FileService.Add(model.NewFileUpload, idtask); 
               return Json(model);
            }

            return Json(null);
        }

        public IActionResult DeleteUserTask([FromBody] UserTask userTask)
        {
            return Json(UserTaskService.DeleteUser(userTask.UserId, userTask.TaskId));
        }


        public IActionResult DeleteUserProject([FromBody] UserProject userTask)
        {
            return Json(UserProjectService.DeleteUser(userTask.UserId, userTask.ProjectId));
        }

        public IActionResult DeleteProject([FromBody] Project project)
        {
            return Json(ProjectService.Delete(project.ProjectId));
        }

        public IActionResult UserNotTask([FromBody] string taskid)
        {
                List<UserTask> userTask = UserTaskService.GetAll(taskid);
                List<UserProject> userProjects = UserProjectService.GetAll();
                List<Users> allusers = UserService.GetAll();
                List<Users> nousers = new List<Users>();

                if (userTask.Count > 0)
                {
                    var usersInTask = userTask.Select(ut => ut.UserId).ToList();

                    nousers = allusers
                        .Where(u => userProjects.Any(up => up.UserId == u.UserId) && !usersInTask.Contains(u.UserId))
                        .ToList();
                }
                else
                {
                    nousers = allusers
                        .Where(u => userProjects.Any(up => up.UserId == u.UserId))
                        .ToList();
                }
                return Json(nousers);

        }

        public IActionResult UserAddTask([FromBody] List<UserTask> userTasks)
        {
           return Json(UserTaskService.Add(userTasks));

        }
        public IActionResult UserAddProject([FromBody] List<UserProject> userProject)
        {
            return Json(UserProjectService.Add(userProject));

        }



        public IActionResult UserAddTaskView([FromBody] List<UserTask> userTasks)
        {
            if (userTasks != null)
            {
                return Json(UserService.GetByTask(userTasks));
            }
            return Json(null);

        }
        public IActionResult UserAddProjectView([FromBody] List<UserProject> userProject)
        {
            if (userProject != null)
            {
                return Json(UserService.GetByProject(userProject));
            }
            return Json(null);

        }
        public IActionResult TaskStatus([FromBody] string tasks)
        {
            return Json(TaskService.Status(tasks));

        }

        public IActionResult DeleteFile([FromBody] string file)
        {
            return Json(FileService.Delete(file));

        }

        public IActionResult AllUser([FromBody] string Id)
        {
            if (Id != null)
            {
                return Json(UserService.NoProject(Id));
            }
            return Json(null);
        }

        public IActionResult Users([FromBody] bool up)
        {
            if (up)
            {
               return Json(UserService.GetAll());
            }
            return Json(null);
        }

        [HttpPost]
        public IActionResult AddComment([FromBody] Comments comment)
        {
            if (comment != null)
            {
                return Json(CommentService.Add(comment));
            }
            return Json(null);
        }

        public IActionResult Allcoment([FromBody] string id)
        {
            if (id != null)
            {
                return Json(CommentService.AllComment(id));
            }
            return Json(null);

        }

        public IActionResult AddSprint([FromBody] Sprint sprint)
        {
            if (sprint != null)
            {
                return Json(SprintService.Add(sprint));
            }
            return Json(null);
        }

        public IActionResult AddTask([FromBody] Domain.Entity.Task task)
        {
            if (task != null)
            {
                return Json(TaskService.Add(task));
            }
            return Json(null);
        }

        public IActionResult AddProject([FromBody] Project project)
        {
            if (project != null)
            {
                return Json(ProjectService.Add(project));
            }
            return Json(null);

        }

        public IActionResult UpRole([FromBody] string id)
        {
            if (id != null)
            {
                return Json(UserService.UpRole(id));
            }
            return Json(null);
        }
        public IActionResult DownRole([FromBody] string id)
        {
            if (id != null)
            {
                return Json(UserService.DownRole(id));
            }
            return Json(null);
        }

    }
}
