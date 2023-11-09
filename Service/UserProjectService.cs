using DAL;
using Domain.Entity;

namespace Service
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
        public static List<UserProject> GetAll()
        {
            using (ApplicationContext Db = new ApplicationContext())
            {
                return Db.UserProject.ToList();
            }
        }
        public static bool Add(List<UserProject> userProject)
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
                    return true;
                }
            }
            return false;
        }
    }
}
