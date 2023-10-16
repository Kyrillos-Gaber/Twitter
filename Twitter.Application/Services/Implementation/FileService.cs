using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Twitter.Application.Services.Contract;

namespace Twitter.Application.Services.Implementation;

public class FileService : IFileService
{
    private readonly IWebHostEnvironment _hostEnvironment;

    public FileService(IWebHostEnvironment hostEnvironment)
    {
        _hostEnvironment = hostEnvironment;
    }

    public bool DeleteImage(string imageFileName)
    {
        try
        {
            var wwwPath = _hostEnvironment.ContentRootPath;
            var path = Path.Combine(wwwPath, "Uploads\\", imageFileName);
            if (File.Exists(path))
            {
                File.Delete(path);
                return true;
            }
            return false;
        }
        catch { return false; }
    }

    public string SaveImage(IFormFile imageFile)
    {
        try
        {
            var contentPath = _hostEnvironment.ContentRootPath;
            var path = Path.Combine(contentPath, "Uploads");
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            var ext = Path.GetExtension(imageFile.FileName);
            string[] allowedExtensions = { ".jpg", ".png", ".jpeg" };

            if (!allowedExtensions.Contains(ext))
                throw new Exception(string.Format("Only {0} extensions are allowed.", string.Join(",", allowedExtensions)));
            
            string newFileName = $"{Guid.NewGuid()}{ext}";
            string filePath = Path.Combine(path, newFileName);
            FileStream stream = new (filePath, FileMode.Create);
            imageFile.CopyTo(stream);
            stream.Close();
            
            return newFileName;
        }
        catch(Exception ex) 
        { 
            throw new Exception($"Error can not save file => {ex}"); 
        }
    }
}
