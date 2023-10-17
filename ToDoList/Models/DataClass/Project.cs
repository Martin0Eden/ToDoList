namespace ToDoList.Models
{
    public class Project
    {
        public string ProjectId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public Project(string projectId, string name, string description)
        {
            ProjectId = projectId;
            Name = name;
            Description = description;
        }

        public Project()
        {
            
        }

    }
}
