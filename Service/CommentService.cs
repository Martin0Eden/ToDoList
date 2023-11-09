using DAL;
using Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class CommentService
    {
        public static object Add(Comments comment)
        {
            using (ApplicationContext Db = new ApplicationContext())
            {
                string id;
                while (true)
                {
                    Random rand = new Random();
                    id = "C" + rand.Next(100, 999);
                    if (Db.Comments.FirstOrDefault(x => x.CommentId == id) == null)
                        break;
                }
                comment.CommentId = id;
                Db.Comments.Add(comment);
                Db.SaveChanges();
                var data = new
                {
                    comment = comment,
                    Users = Db.Users.FirstOrDefault(x => x.UserId == comment.UserId)
                };
                return data;
            }
        }
        public static object AllComment(string id)
        {
            using (ApplicationContext Db = new ApplicationContext())
            {
                List<Comments> comments = Db.Comments.Where(x => x.IdIn == id).ToList();
                List<Users> users = new List<Users>();
                foreach (Comments comment in comments)
                {
                    users.Add(Db.Users.FirstOrDefault(s => s.UserId == comment.UserId));
                }
                var result = new
                {
                    Comments = comments,
                    User = users
                };

                return result;

            }
        }
    }
}
