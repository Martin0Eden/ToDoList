using Microsoft.AspNetCore.Mvc;
using ToDoList.Models;
using ToDoList.Models.DataBase;
using ToDoList.Models.ViewClass;
using ToDoList.Models.DataClass;

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
            if(user!=null) 
            {
                if(user.Password==s.Password)
                {

                    if (user.Role == "R1")
                    {
                        using (ApplicationContext Db = new ApplicationContext())
                        {
                            ListClass listClass = new ListClass(user, Db.Project.ToList());
                            return View(listClass);
                        }
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
            using(ApplicationContext Db = new ApplicationContext())
            {
                ProjectClass projectClass = new ProjectClass();

                projectClass.Project = Db.Project.FirstOrDefault(x => x.ProjectId == id);
                projectClass.User = Db.Users.FirstOrDefault(x => x.UserId == userid);
                projectClass.Users = UserProjectService.GetUsers(id);
                List<Sprint> sprint = Db.Sprint.Where(x=>x.ProjectId==id).ToList();
                projectClass.Spints= sprint;

                return View(projectClass);
            }
        }

        public IActionResult Task(string idtask, string iduser)
        {
            using(ApplicationContext Db = new ApplicationContext())
            {
                TaskClass taskClass = new TaskClass();
                taskClass.FileUpload = Db.FileUploadModel.Where(x=>x.TaskId==idtask).ToList();
                taskClass.Task = TaskService.Get(idtask);
                taskClass.Users = UserService.GetId(iduser);
                taskClass.listuser = UserTaskService.GetUsers(idtask);
                taskClass.NewFileUpload = new FileUploadModel();
                return View(taskClass);
            }
            
        }

        

    }
}