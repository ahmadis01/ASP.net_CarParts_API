using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarParts.Base.BaseRepository
{
    public class BaseRepository
    {
        private readonly IHostEnvironment Environment;
        public BaseRepository(IHostEnvironment environment)
        {
            this.Environment = environment;
        }
        public async Task<string> UploadFile(IFormFile file , string path)
        {
            var extension = Path.GetExtension(file.FileName);
            var fileName = Guid.NewGuid();
            var fullPath = Path.Combine(Environment.ContentRootPath + "wwwroot" , path, fileName + extension);
            string returnedPath = Path.Combine(path, fileName + extension);
            using (FileStream stream = new FileStream(fullPath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
                stream.Close();
            }
            return returnedPath;
        }
    }
}
