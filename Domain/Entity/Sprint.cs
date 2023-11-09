using System.ComponentModel.DataAnnotations;

namespace Domain.Entity
{
    public class Sprint
    {
        [Key]
        public string SprintId { get; set; }
        public string ProjectId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public Sprint(string sprintId, string projectId, string name, string description, DateTime startDate, DateTime endDate)
        {
            SprintId = sprintId;
            ProjectId = projectId;
            Name = name;
            Description = description;
            StartDate = startDate;
            EndDate = endDate;
        }

        public Sprint()
        {

        }
    }

}
