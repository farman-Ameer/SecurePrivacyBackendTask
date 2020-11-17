using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MedicineShopBackEnd.Services.Helpers
{
   public static class Utility
    {
        public static string FileUpload(IFormFile file, string path)
        {
            //string filePath = "";
            string FileName = string.Empty;
            try
            {
                FileName = Path.GetFileNameWithoutExtension(file.FileName);
                string FileExtension = Path.GetExtension(file.FileName);//To Get File Extension  
                FileName = DateTime.Now.ToString("hh.mm.ss.ffffff") + "-" + FileName.Trim() + FileExtension; //Add Current Date To Attached File Name  
                string folderName = "Upload";
                string newPath = Path.Combine(path, folderName);
                // path += "/ImageStorage";

                if (!System.IO.Directory.Exists(newPath)) //Check if directory exist           
                    System.IO.Directory.CreateDirectory(newPath); //Create directory if it doesn't exist

                //filePath = path + "/" + FileName;
                string fullPath = Path.Combine(newPath, FileName);
                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    //file.CopyToAsync(stream);
                    file.CopyTo(stream);

                }
            }
            catch (Exception exc)
            {
                throw new ApplicationException(exc.Message);
            }
            return FileName;
        }
    }
}
