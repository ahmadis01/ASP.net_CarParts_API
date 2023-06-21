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
        private readonly IHostEnvironment _environment;
        public BaseRepository(IHostEnvironment environment)
        {
            _environment = environment;
        }
        public async Task<string> UploadFile(IFormFile file , string path)
        {
            var extension = Path.GetExtension(file.FileName);
            var fileName = Guid.NewGuid();
            var fullPath = Path.Combine(_environment.ContentRootPath + "wwwroot" , path, fileName + extension);
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
