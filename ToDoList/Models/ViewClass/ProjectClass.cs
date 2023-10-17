namespace ToDoList.Models.ViewClass
{
    public class ProjectClass
    {
        public Project Project { get; set; }
        public Users User { get; set; }
        public List<Sprint> Spints { get; set;}
        public List<Users> Users { get; set;}
        public List<Task> Tasks { get; set;}
    }
}
