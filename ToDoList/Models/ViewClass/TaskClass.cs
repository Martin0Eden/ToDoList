using ToDoList.Models.DataClass;

namespace ToDoList.Models.ViewClass
{
    public class TaskClass
    {
        public Users Users { get; set; }
        public List<Users> listuser { get; set; }
        public Task Task { get; set; }
        public List<FileUploadModel> FileUpload { get; set; }
        public FileUploadModel NewFileUpload { get; set; }

    }
}
