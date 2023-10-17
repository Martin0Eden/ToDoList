using Microsoft.AspNetCore.Mvc;
using ToDoList.Models.ViewClass;
using ToDoList.Models;
using ToDoList.Models.DataClass;
using ToDoList.Models.DataBase;
using System.IO;
using System.Net;

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
            if (model.NewFileUpload.FileToUpload != null)
            {

                model.NewFileUpload.FilePath = "wwwroot/Files";

                model.NewFileUpload.FileName = Path.GetFileName(model.NewFileUpload.FileToUpload.FileName);
                model.NewFileUpload.FileExtension = Path.GetExtension(model.NewFileUpload.FileName);

                var filePath = Path.Combine(model.NewFileUpload.FilePath, model.NewFileUpload.FileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.NewFileUpload.FileToUpload.CopyTo(fileStream);
                    using (ApplicationContext Db = new ApplicationContext())
                    {
                        model.NewFileUpload.TaskId = idtask;
                        Random random = new Random();
                        string id;
                        while(true)
                        {
                            id = "F"+random.Next(100,999);
                            if (Db.FileUploadModel.FirstOrDefault(x => x.FileId == id) == null)
                                break;
                        }
                        model.NewFileUpload.FileId = id;
                        Db.FileUploadModel.Add(model.NewFileUpload);
                        Db.SaveChanges();
                        return Json(model);
                    }
                }

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
            using (ApplicationContext Db = new ApplicationContext())
            {
                List<UserTask> userTask = Db.UserTask.Where(x => x.TaskId == taskid).ToList();
                List<UserProject> userProjects = Db.UserProject.ToList();
                List<Users> allusers = Db.Users.ToList();
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
        }

        public IActionResult UserAddTask([FromBody] List<UserTask> userTasks)
        {
            if(userTasks!=null)
            {
                using(ApplicationContext Db = new ApplicationContext())
                {
                    Random rand = new Random();
                    foreach (UserTask userTask in userTasks)
                    {
                        string id;
                        while (true)
                        {
                            id = "UT" + rand.Next(100, 999);
                            if (Db.UserTask.FirstOrDefault(x => x.UserTaskId == id) == null)
                                break;
                        }
                        userTask.UserTaskId = id;
                        Db.UserTask.Add(userTask);

                    }
                    Db.SaveChanges();
                    return Json(true);
                }
            }
            return Json(false);
            
        }
        public IActionResult UserAddProject([FromBody] List<UserProject> userProject)
        {
            if (userProject != null)
            {
                using (ApplicationContext Db = new ApplicationContext())
                {
                    Random rand = new Random();
                    foreach (UserProject userTask in userProject)
                    {
                        string id;
                        while (true)
                        {
                            id = "UP" + rand.Next(100, 999);
                            if (Db.UserProject.FirstOrDefault(x => x.UserProjectId == id) == null)
                                break;
                        }
                        userTask.UserProjectId = id;
                        Db.UserProject.Add(userTask);

                    }
                    Db.SaveChanges();
                    return Json(true);
                }
            }
            return Json(false);

        }

        public IActionResult UserAddTaskView([FromBody] List<UserTask> userTasks)
        {
            if (userTasks != null)
            {
                using (ApplicationContext Db = new ApplicationContext())
                {
                    List<Users> usertask = new List<Users>();
                    foreach (var item in userTasks)
                    {
                        usertask.Add(Db.Users.FirstOrDefault(x=>x.UserId==item.UserId));
                    }
                    return Json(usertask);
                }
            }
            return Json(null);

        }
        public IActionResult UserAddProjectView([FromBody] List<UserProject> userProject)
        {
            if (userProject != null)
            {
                using (ApplicationContext Db = new ApplicationContext())
                {
                    List<Users> usertask = new List<Users>();
                    foreach (var item in userProject)
                    {
                        usertask.Add(Db.Users.FirstOrDefault(x => x.UserId == item.UserId));
                    }
                    return Json(usertask);
                }
            }
            return Json(null);

        }
        public IActionResult TaskStatus([FromBody] string tasks)
        {
            using(ApplicationContext Db = new ApplicationContext())
            {
                Models.Task taskup = Db.Task.FirstOrDefault(x=>x.TaskId==tasks);
                if(taskup != null)
                {
                    if (taskup.Status == "false")
                        taskup.Status = "checed";
                    else
                        taskup.Status = "false";
                    Db.Task.Update(taskup);
                    Db.SaveChanges();
                    return Json(true);
                }
                return Json(false);
            }
            
        }

        public IActionResult DeleteFile([FromBody] string file)
        {
            try
            {
                using (ApplicationContext Db = new ApplicationContext())
                {
                    FileUploadModel fileUploadModel = Db.FileUploadModel.FirstOrDefault(x => x.FileId == file);

                    if (fileUploadModel != null)
                    {
                        string path = Path.Combine(fileUploadModel.FilePath, fileUploadModel.FileName);
                        System.IO.File.Delete(path);


                        Db.FileUploadModel.Remove(fileUploadModel);
                        Db.SaveChanges();

                        return Json(true);
                    }
                    else
                    {
                        return Json(false); 
                    }
                }
            }
            catch (Exception ex)
            {
                return Json(false);
            }
            
            
        }

        public IActionResult AllUser([FromBody] string Id)
        {
            if(Id!=null)
            {
                using(ApplicationContext Db = new ApplicationContext())
                {
                    List<Users> all = Db.Users.ToList();
                    List<UserProject> usersInProject = Db.UserProject.Where(x => x.ProjectId == Id).ToList();

                    List<Users> noproject = all.Where(u => !usersInProject.Any(up => up.UserId == u.UserId)).ToList();

                    return Json(noproject);
                }
            }
            return Json(null);
        }

        public IActionResult Users([FromBody] bool up)
        {
            if(up)
            {
                using(ApplicationContext Db = new ApplicationContext())
                {
                    return Json(Db.Users.ToList());
                }
            }
            return Json(null);
        }

        [HttpPost]
        public IActionResult AddComment([FromBody] Comments comment)
        {
            if (comment!=null)
            {
                using(ApplicationContext Db = new ApplicationContext())
                {
                    string id;
                    while(true)
                    {
                        Random rand = new Random();
                        id ="C"+ rand.Next(100, 999);
                        if (Db.Comments.FirstOrDefault(x => x.CommentId == id) == null)
                            break;
                    }
                    comment.CommentId = id;
                    Db.Comments.Add(comment);
                    Db.SaveChanges();
                    var data = new
                    {
                        comment = comment,
                        Users = Db.Users.FirstOrDefault(x=>x.UserId==comment.UserId)
                    };
                    return Json(data);
                }
            }
            return Json(null);
        }

        public IActionResult Allcoment([FromBody] string id)
        {
            if (id!=null)
            {
                using (ApplicationContext Db = new ApplicationContext())
                {
                    List<Comments> comments = Db.Comments.Where(x => x.IdIn == id).ToList();
                    List<Users> users = new List<Users>();
                    foreach (Comments comment in comments)
                    {
                        users.Add(Db.Users.FirstOrDefault(s=>s.UserId==comment.UserId));
                    }
                    var result = new
                    {
                        Comments = comments,
                        User = users
                    };

                    return Json(result);

                }
            }
            return Json(null);
            
        }

        public IActionResult AddSprint([FromBody] Sprint sprint)
        {
            if (sprint!=null)
            {
                using (ApplicationContext Db = new ApplicationContext())
                {
                    string id;
                    while(true)
                    {
                        Random rand = new Random();
                        id="S"+rand.Next(100,999);
                        if (Db.Sprint.FirstOrDefault(x => x.SprintId == id) == null)
                            break;
                    }
                    sprint.SprintId = id;
                    Db.Sprint.Add(sprint);
                    Db.SaveChanges();
                    return Json(sprint);
                }
            }
            return Json(null);
        }
        
            public IActionResult AddTask([FromBody] Models.Task task)
        {
            if (task != null)
            {
                using (ApplicationContext Db = new ApplicationContext())
                {
                    string id;
                    while (true)
                    {
                        Random rand = new Random();
                        id = "T" + rand.Next(100, 999);
                        if (Db.Sprint.FirstOrDefault(x => x.SprintId == id) == null)
                            break;
                    }
                    task.TaskId = id;
                    task.Status = "false";
                    Db.Task.Add(task);
                    Db.SaveChanges();
                    return Json(task);
                }
            }
            return Json(null);
        }

        public IActionResult AddProject([FromBody] Project project)
        {
            if(project != null)
            {
                using (ApplicationContext Db = new ApplicationContext())
                {
                    Users user = Db.Users.FirstOrDefault(x=>x.UserId==project.ProjectId); 
                    string id;
                    while (true)
                    {
                        Random random = new Random();
                        id = "P" + random.Next(100, 999);
                        if (Db.Project.FirstOrDefault(x => x.ProjectId == id) == null)
                            break;
                    }
                    project.ProjectId = id;
                    Db.Project.Add(project);
                    Db.SaveChanges();

                    var data = new
                    {
                        users= user,
                        projects=project
                    };

                    return Json(data);
                }
            }
            return Json(null);
            
        }

        public IActionResult UpRole([FromBody] string id)
        {
            if(id != null)
            {
                using(ApplicationContext Db = new ApplicationContext())
                {
                    Users user = Db.Users.FirstOrDefault(y=>y.UserId==id);
                    switch(user.Role)
                    {
                        case "R3":
                            {
                                user.Role = "R2";
                                break;
                            }
                        case "R2":
                            {
                                user.Role = "R1";
                                break;
                            }
                    }
                    Db.Users.Update(user);
                    Db.SaveChanges();
                    return Json(true);
                }
            }
            return Json(null);
        }
        public IActionResult DownRole([FromBody] string id)
        {
            if (id != null)
            {
                using (ApplicationContext Db = new ApplicationContext())
                {
                    Users user = Db.Users.FirstOrDefault(y => y.UserId == id);
                    switch (user.Role)
                    {
                        case "R1":
                            {
                                user.Role = "R2";
                                break;
                            }
                        case "R2":
                            {
                                user.Role = "R3";
                                break;
                            }
                    }
                    Db.Users.Update(user);
                    Db.SaveChanges();
                    return Json(true);
                }
            }
            return Json(null);
        }

    }
}
