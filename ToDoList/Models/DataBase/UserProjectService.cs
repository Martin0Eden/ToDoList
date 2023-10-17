using ToDoList.Models.DataClass;

namespace ToDoList.Models.DataBase
{
    public class UserProjectService
    {
        public static List<Project> GetProject(string userid)
        {
            using (ApplicationContext Db = new ApplicationContext())
            {
                List<UserProject> users = Db.UserProject.Where(x=>x.UserId==userid).ToList();
                List<Project> projects = new List<Project>();
                foreach (var user in users)
                {
                    projects.Add(Db.Project.FirstOrDefault(x => x.ProjectId == user.ProjectId));
                }
                return projects;
            }
        }
        public static List<Users> GetUsers(string project)
        {
            List<Users> usersList = new List<Users>();
            using (ApplicationContext Db = new ApplicationContext())
            {
                List<UserProject> users = Db.UserProject.Where(x => x.ProjectId == project).ToList();
                foreach (var user in users)
                {
                    usersList.Add(Db.Users.FirstOrDefault(x => x.UserId == user.UserId));
                }
                return usersList;
            }
        }

        public static bool DeleteUser(string userid, string projectid)
        {
            try
            {
                using (ApplicationContext Db = new ApplicationContext())
                {
                    UserProject delete = Db.UserProject.FirstOrDefault(x => x.ProjectId == projectid && x.UserId == userid);
                    Db.UserProject.Remove(delete);
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
