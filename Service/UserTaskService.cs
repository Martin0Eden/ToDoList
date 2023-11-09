using DAL;
using Domain.Entity;

namespace Service
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

        public static List<UserTask> GetAll(string taskid)
        {
            using(ApplicationContext Db = new ApplicationContext())
            {
                return Db.UserTask.Where(x => x.TaskId == taskid).ToList();
            }
        }

        public static bool Add(List<UserTask> userTasks)
        {
            if (userTasks != null)
            {
                using (ApplicationContext Db = new ApplicationContext())
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
                }
                return true;
            }
            return false;
        }
    }
}
