namespace ToDoList.Models.ViewClass
{
    public class ListClass
    {
        public Users user { get; set; }
        public List<Project> project { get; set; }

        public ListClass(Users user, List<Project> project)
        {
            this.user = user;
            this.project = project;
        }
    }
}
