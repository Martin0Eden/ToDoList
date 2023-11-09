using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using Domain.Entity;

namespace Service
{
    public class SprintService
    {
        public static List<Sprint> GetById(string id)
        {
            using (ApplicationContext Db = new ApplicationContext())
            {
                return Db.Sprint.Where(x => x.ProjectId == id).ToList();
            }
        }
        public static Sprint Add(Sprint sprint)
        {
            using (ApplicationContext Db = new ApplicationContext())
            {
                string id;
                while (true)
                {
                    Random rand = new Random();
                    id = "S" + rand.Next(100, 999);
                    if (Db.Sprint.FirstOrDefault(x => x.SprintId == id) == null)
                        break;
                }
                sprint.SprintId = id;
                Db.Sprint.Add(sprint);
                Db.SaveChanges();
                return sprint;
            }
        }
    }
}
