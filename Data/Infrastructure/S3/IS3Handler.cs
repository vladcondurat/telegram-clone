using Microsoft.AspNetCore.Http;

namespace Data.Infrastructure.S3;

public interface IS3Handler
{
    // string UploadFile(ImageDto file);
    string UploadFile(IFormFile file);
}