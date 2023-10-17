using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToDoList.Models.DataClass
{
    public class FileUploadModel
    {
        [Key]
        public string FileId { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public string FileExtension { get; set; }
        public string TaskId { get; set; }

        [NotMapped]
        public IFormFile FileToUpload { get; set; }
    }

}
