namespace ToDoList.Models.DataBase
{
    public class TaskService
    {
        public static Task Get(string taskId)
        {
            using(ApplicationContext Db = new ApplicationContext())
            {
                return Db.Task.FirstOrDefault(x=>x.TaskId==taskId);
            }
        }
    }
}
