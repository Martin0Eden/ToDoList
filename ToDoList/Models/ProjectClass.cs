using Domain.Entity;

namespace ToDoList.Models
{
    public class ProjectClass
    {
        public Project Project { get; set; }
        public Users User { get; set; }
        public List<Sprint> Spints { get; set;}
        public List<Users> Users { get; set;}
        public Dictionary<string, List<Domain.Entity.Task>>  Tasks { get; set;}
        public Dictionary<string, FileUploadModel> TrueFile { get; set;}
    }
}
