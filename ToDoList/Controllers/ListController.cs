using Microsoft.AspNetCore.Mvc;
using ToDoList.Models;
using System.Threading.Tasks;
using System.Formats.Asn1;
using Domain.Entity;
using Service;
using DAL;

namespace ToDoList.Controllers
{
    public class ListController : Controller
    {
        private readonly ILogger<ListController> _logger;

        public ListController(ILogger<ListController> logger)
        {
            _logger = logger;
        }
        Users user = new Users();

        public IActionResult List(Users s)
        {
            user = UserService.GetName(s.Name);
            if (user != null)
            {
                if (user.Password == s.Password)
                {
                    if (user.Role == "R1")
                    {
                            ListClass listClass = new ListClass(user, ProjectService.GetAll());
                            return View(listClass);
                       
                    }
                    else
                    {
                        List<Project> projects;
                        projects = UserProjectService.GetProject(user.UserId);
                        ListClass listClass = new ListClass(user, projects);
                        return View(listClass);
                    }


                }
            }
            return RedirectToAction("Login", "Authorization");
        }

        public IActionResult ProjectView(string id, string userid)
        {
                ProjectClass projectClass = new ProjectClass();

                projectClass.Project = ProjectService.GetById(id);
                projectClass.User = UserService.GetId(userid);
                projectClass.Users = UserProjectService.GetUsers(id);
                projectClass.Spints = SprintService.GetById(id);
                projectClass.Tasks = TaskService.GetAll(projectClass.Spints);

                projectClass.TrueFile = FileService.GetTryeFile(projectClass.Tasks);
                
                return View(projectClass);
            
        }

        public IActionResult Task(string idtask, string iduser)
        {
                TaskClass taskClass = new TaskClass();
                taskClass.FileUpload = FileService.GetAll(idtask);
                taskClass.Task = TaskService.Get(idtask);
                taskClass.Users = UserService.GetId(iduser);
                taskClass.listuser = UserTaskService.GetUsers(idtask);
                taskClass.NewFileUpload = new FileUploadModel();
                return View(taskClass);
        }

    }
}