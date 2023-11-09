using DAL;
using Domain.Entity;
using System.Threading.Tasks;

namespace Service
{
    public class TaskService
    {
        public static Domain.Entity.Task Get(string taskId)
        {
            using(ApplicationContext Db = new ApplicationContext())
            {
                return Db.Task.FirstOrDefault(x=>x.TaskId==taskId);
            }
        }

        public static Dictionary<string,List<Domain.Entity.Task>> GetAll(List<Sprint> sprints)
        {
            using (ApplicationContext Db = new ApplicationContext())
            {
                Dictionary<string, List< Domain.Entity.Task>> Tasks = new Dictionary<string, List<Domain.Entity.Task>>();
                foreach (var sprint in sprints)
                {
                    Tasks[sprint.SprintId] = Db.Task.Where(x => x.SprintId == sprint.SprintId).ToList();
                }
                return Tasks;
            }
        }

        public static bool Status(string taskId)
        {
            using (ApplicationContext Db = new ApplicationContext())
            {
                Domain.Entity.Task taskup = Db.Task.FirstOrDefault(x => x.TaskId == taskId);
                if (taskup != null)
                {
                    if (taskup.Status == "false")
                        taskup.Status = "checed";
                    else
                        taskup.Status = "false";
                    Db.Task.Update(taskup);
                    Db.SaveChanges();
                    return true;
                }
                return false;
            }
        }
        public static Domain.Entity.Task Add(Domain.Entity.Task task)
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
                return task;
            }
        }
        
    }
}
