using System.ComponentModel.DataAnnotations;

namespace ToDoList.Models.DataClass
{
    public class UserTask
    {
        [Key]
        public string UserTaskId { get; set; }
        public string UserId { get; set; }
        public string TaskId { get; set; }
    }
}
