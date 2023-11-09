using DAL;
using Domain.Entity;

namespace Service
{
    public class UserService
    {
        public static List<Users> GetAll()
        {
            using(ApplicationContext Db =  new ApplicationContext())
            {
                return Db.Users.ToList();
            } 
        }   

        public static List<Users> NoProject(string Id)
        {
            using (ApplicationContext Db = new ApplicationContext())
            {
                List<Users> all = Db.Users.ToList();
                List<UserProject> usersInProject = Db.UserProject.Where(x => x.ProjectId == Id).ToList();

                List<Users> noproject = all.Where(u => !usersInProject.Any(up => up.UserId == u.UserId)).ToList();

                return noproject;
            }
        }

        public static Users GetName(string username)
        {
            using(ApplicationContext Db = new ApplicationContext())
            {
                Users user = Db.Users.FirstOrDefault(x=>x.Name == username);
                return user;
            }
        }
        public static Users GetEmail(string userEmail)
        {
            using (ApplicationContext Db = new ApplicationContext())
            {
                Users user = Db.Users.FirstOrDefault(x => x.Email == userEmail);
                return user;
            }
        }
        public static Users GetId(string userid)
        {
            using (ApplicationContext Db = new ApplicationContext())
            {
                Users user = Db.Users.FirstOrDefault(x => x.UserId == userid);
                return user;
            }
        }

        public static List<Users> GetByTask(List<UserTask> userTasks)
        {
            using (ApplicationContext Db = new ApplicationContext())
            {
                List<Users> usertask = new List<Users>();
                foreach (var item in userTasks)
                {
                    usertask.Add(Db.Users.FirstOrDefault(x => x.UserId == item.UserId));
                }
                return usertask;
            }
        }

        public static List<Users> GetByProject(List<UserProject> userProject)
        {
            using (ApplicationContext Db = new ApplicationContext())
            {
                List<Users> usertask = new List<Users>();
                foreach (var item in userProject)
                {
                    usertask.Add(Db.Users.FirstOrDefault(x => x.UserId == item.UserId));
                }
                return usertask;
            }
        }

        public static void Add(Users user) 
        {
            using (ApplicationContext Db = new ApplicationContext())
            {
                Random r = new Random();
            string s;
            while (true)
            {
                s = "U" + r.Next(100, 999);
                if (Db.Users.FirstOrDefault(x=>x.UserId==s)==null)
                        break;
            }
            user.Role = "R3";
            
                Db.Users.Add(user);
                Db.SaveChanges();
            }
        }

        public static bool UpRole(string id)
        {
            using (ApplicationContext Db = new ApplicationContext())
            {
                Users user = Db.Users.FirstOrDefault(y => y.UserId == id);
                switch (user.Role)
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
                return true;
            }
        }
        public static bool DownRole(string id)
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
                return true;
            }
        }
    }
}
