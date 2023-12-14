using Microsoft.AspNetCore.Http;
using System.IO;
using System;

namespace BookStore.PL.Helpers
{
    public class DocumentSetting
    {
        //Upload
        public static string UploadFile(IFormFile file, string foldername)
        {
            // Get Located folder path
            string folderpath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\files", foldername);

            // Get filename and make it unique
            string filename = $"{Guid.NewGuid()}{file.FileName}";

            //Get file path(folder path + file name)
            string filepath = Path.Combine(folderpath, filename);

            // Save file as streams
            using var FS = new FileStream(filepath, FileMode.Create);
            file.CopyTo(FS);

            //Return file name
            return filename;
        }
        //Delete
        public static void DeleteFile(string filename, string foldername)
        {
            //Get the File Path
            string filepath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\files", foldername, filename);

            //Check if file exists or not
            // if exists remove it
            if (File.Exists(filepath))
            {
                File.Delete(filepath);
            }

        }
    }
}

