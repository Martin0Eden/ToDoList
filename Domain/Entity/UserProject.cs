using System.ComponentModel.DataAnnotations;

namespace Domain.Entity
{
    public class UserProject
    {
        [Key]
        public string UserProjectId { get; set; }
        public string UserId { get; set; }
        public string ProjectId { get; set; }

        public UserProject(string userId, string projectId)
        {
            UserId = userId;
            ProjectId = projectId;
        }

        public UserProject()
        {
            
        }
    }
}
