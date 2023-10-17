namespace ToDoList.Models.DataBase
{
    public class UserService
    {
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
    }
}
