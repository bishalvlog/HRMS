using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Update.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Data.Comman.Helpers
{
    public static class FileUploadHelper
    {
        public static async Task<string> UploadFile(IWebHostEnvironment hostev, string folderPath, IFormFile file)
        {
            if (!Directory.Exists("" + hostev.WebRootPath + "\\" + folderPath))
                Directory.CreateDirectory("" +hostev.WebRootPath + "\\" + folderPath);
            if(file == null)
                return  "/" + folderPath;

            var fileNameWithOutExtension = Path.GetFileNameWithoutExtension(folderPath);
            var fileExtension = Path.GetExtension(file.FileName);
            folderPath += fileNameWithOutExtension + "_" + DateTime.UtcNow.ToString("yyyy_MM_dd_HH_mm_ss_fff") + fileExtension;

            var serverFolder =  Path.Combine(hostev.WebRootPath, folderPath);
            await using(var fileStream = new FileStream(serverFolder,FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
                fileStream.Flush();

            }
            return "/" + folderPath;


        }
    }
}
