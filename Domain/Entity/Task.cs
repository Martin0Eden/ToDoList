using System.ComponentModel.DataAnnotations;

namespace Domain.Entity
{
    public class Task
    {
        [Key]
        public string TaskId { get; set; }
        public string SprintId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }

        public Task(string taskId, string sprintId, string name, string description, string status)
        {
            TaskId = taskId;
            SprintId = sprintId;
            Name = name;
            Description = description;
            Status = status;
        }

        public Task()
        {
            
        }
    }
}
