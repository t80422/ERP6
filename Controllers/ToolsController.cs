using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ERP6.Controllers
{
    public class ToolsController : Controller
    {
        // 上傳圖片
        public async Task<string> UploadImg(IFormFile file , string filepath)
        {
            if(file.Length > 0)
            {
                string filename = $"{DateTime.Now.ToString("yyyyMMddHHmmss")}_{file.FileName}";
                filepath += $"\\{filename}";
                
                using ( var fc = System.IO.File.Create(filepath))
                {
                    await file.CopyToAsync(fc);

                    return filename;
                }
            }

            return "";
        }
    }
}
