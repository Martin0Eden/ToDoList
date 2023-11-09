using Domain.Entity;

namespace ToDoList.Models
{
    public class TaskClass
    {
        public Users Users { get; set; }
        public List<Users> listuser { get; set; }
        public Domain.Entity.Task Task { get; set; }
        public List<FileUploadModel> FileUpload { get; set; }
        public FileUploadModel NewFileUpload { get; set; }

    }
}
