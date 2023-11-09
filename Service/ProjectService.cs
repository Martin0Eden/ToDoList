using System.Collections.Generic;
using DAL;
using Domain.Entity;

namespace Service
{
    public class ProjectService
    {
        public static bool Delete(string projectId)
        {
            try
            {
                using (ApplicationContext Db = new ApplicationContext())
                {
                    Project project = Db.Project.FirstOrDefault(x => x.ProjectId == projectId);
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static List<Project> GetAll()
        {
            using (ApplicationContext Db = new ApplicationContext())
            {
                return Db.Project.ToList();
            }
        }

        public static Project GetById(string projectId)
        {
            using(ApplicationContext Db = new ApplicationContext())
            {
                return Db.Project.FirstOrDefault(x => x.ProjectId == projectId);
            }
        }

        public static object Add(Project project)
        {
            using (ApplicationContext Db = new ApplicationContext())
            {
                Users user = Db.Users.FirstOrDefault(x => x.UserId == project.ProjectId);
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
                    users = user,
                    projects = project
                };

                return data;
            }
        }

    }
}
