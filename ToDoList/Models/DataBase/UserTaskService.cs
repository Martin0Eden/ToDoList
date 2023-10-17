using ToDoList.Models.DataClass;

namespace ToDoList.Models.DataBase
{
    public class UserTaskService
    {
        public static List<Users> GetUsers(string task)
        {
            List<Users> usersList = new List<Users>();
            using (ApplicationContext Db = new ApplicationContext())
            {
                List<UserTask> users = Db.UserTask.Where(x => x.TaskId == task).ToList();
                foreach (var user in users)
                {
                    usersList.Add(Db.Users.FirstOrDefault(x => x.UserId == user.UserId));
                }
                return usersList;
            }

        }

        public static bool DeleteUser(string userid, string taskid)
        {
            try
            {
                using (ApplicationContext Db = new ApplicationContext())
                {
                    UserTask delete = Db.UserTask.FirstOrDefault(x => x.TaskId == taskid && x.UserId == userid);
                    Db.UserTask.Remove(delete);
                    Db.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
