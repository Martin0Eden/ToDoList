using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entity;
using DAL;

namespace Service
{
    public class FileService
    {
        public static Dictionary<string, FileUploadModel> GetTryeFile(Dictionary<string, List<Domain.Entity.Task>> tasks)
        {
            using (ApplicationContext Db = new ApplicationContext())
            {
                Dictionary<string, FileUploadModel>  TrueFile = new Dictionary<string, FileUploadModel>();
                foreach (var task in tasks.Values)
                {
                    foreach (var file in task)
                    {
                        TrueFile[file.TaskId] = Db.FileUploadModel.FirstOrDefault(x => x.TaskId == file.TaskId);
                    }
                }
                return TrueFile;
            }
        }

        public static List<FileUploadModel> GetAll(string idtask)
        {
            using(ApplicationContext Db = new ApplicationContext())
            {
                 return Db.FileUploadModel.Where(x => x.TaskId == idtask).ToList();
            }
        }

        public static bool Add(FileUploadModel newfile, string idtask)
        {
            try
            {
                if (newfile.FileToUpload != null)
                {

                    newfile.FilePath = "wwwroot/Files";

                    newfile.FileName = Path.GetFileName(newfile.FileToUpload.FileName);
                    newfile.FileExtension = Path.GetExtension(newfile.FileName);

                    var filePath = Path.Combine(newfile.FilePath, newfile.FileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        newfile.FileToUpload.CopyTo(fileStream);
                        using (ApplicationContext Db = new ApplicationContext())
                        {
                            newfile.TaskId = idtask;
                            Random random = new Random();
                            string id;
                            while (true)
                            {
                                id = "F" + random.Next(100, 999);
                                if (Db.FileUploadModel.FirstOrDefault(x => x.FileId == id) == null)
                                    break;
                            }
                            newfile.FileId = id;
                            Db.FileUploadModel.Add(newfile);
                            Db.SaveChanges();
                            return true;
                        }
                    }

                }
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
            
        }

        public static bool Delete (string file)
        {
            try
            {
                using (ApplicationContext Db = new ApplicationContext())
                {
                    FileUploadModel fileUploadModel = Db.FileUploadModel.FirstOrDefault(x => x.FileId == file);

                    if (fileUploadModel != null)
                    {
                        string path = Path.Combine(fileUploadModel.FilePath, fileUploadModel.FileName);
                        System.IO.File.Delete(path);


                        Db.FileUploadModel.Remove(fileUploadModel);
                        Db.SaveChanges();

                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
