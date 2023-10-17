using System.Collections.Generic;
using ToDoList.Models.DataClass;

namespace ToDoList.Models.DataBase
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



    }
}
