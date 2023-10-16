using Microsoft.AspNetCore.Http;

namespace Twitter.Application.Services.Contract;

public interface IFileService
{
    string SaveImage(IFormFile imageFile);

    bool DeleteImage(string imageFileName);
}
