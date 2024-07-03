using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using static System.Net.Mime.MediaTypeNames;
using static System.Net.WebRequestMethods;

namespace CompanyMVC.Helper
{
    public static class DocumentSettings
    {//Upload File
        //1)the Function will return the path of the file(Image - File - Vedio - pdf)
        //2)the function take name of file & Name of folder that place the file
        public static string UploadFile(IFormFile file, string FolderName)
        {
            //1)Get Located Folder Path(which will store the file)
            //[C:\Users\Super Magic\OneDrive\سطح المكتب\C# Course\Asp Web\Aliaa-MVC\CompanyMVC\CompanyMVC]=> CurrentDirectory
            //[\wwwroot\Files\Images\]=>path
            string FolderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Files\\Images");

            //2)Get File Name and Make it Unique
            string FileName = $"{Guid.NewGuid()}_{file.FileName}";

            //3)Get File Path[Folder Path+ FileName]
            string FilePath = Path.Combine(FolderPath, FileName);

            //4)Save File As Stream [Like open Data Base]:FileStream is a database For Files
            using var Fs = new FileStream(FilePath, FileMode.Create);
            file.CopyTo(Fs);//this will move the file into Stream(Database)
            // FileMode.Create=> check if image exist or not and if exist with the same name it will replace old with new 
            //FileMode.CreateNew => if image exist with the same name it will throw exception unitl remove old 
            //So in CreateNew it must be new image[not found any one]

            //5)Return File Name => which Store in database
            return FileName;
        }

        public static void DeleteFile(string FileName, string FolderName)
        {
            //1)Get Fie Path 
            string FilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Files", FolderName, FileName);
            //EX: Directory.GetCurrentDirectory()=>[C:\Users\Super Magic\OneDrive\سطح المكتب\C# Course\Asp Web\Aliaa-MVC\CompanyMVC\CompanyMVC\]
            //[wwwroot\Files]
            //FolderName=>[\Images\]
            //File Name => [782f35e9-a336-464b-8a0a-af58040e0226_IMG_20230207_141403_598.jpg]

            //2)Check If File Exist Or Not
            if (System.IO.File.Exists(FilePath))
            {
                System.IO.File.Delete(FilePath);
            }

        }
    }
}
