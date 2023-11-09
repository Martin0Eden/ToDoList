using System.ComponentModel.DataAnnotations;

namespace Domain.Entity
{
    public class Comments
    {
        [Key]
        public string CommentId { get; set; }
        public string IdIn { get; set; }
        public string UserId { get; set; }
        public string Text { get; set; }

        public Comments(string commentId, string taskId, string userId, string text)
        {
            CommentId = commentId;
            IdIn = taskId;
            UserId = userId;
            Text = text;
        }

        public Comments()
        {
            
        }
    }
}
