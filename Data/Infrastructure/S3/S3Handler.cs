using Amazon.S3;
using Amazon.S3.Model;

namespace Data.Infrastructure.S3;

public class S3Handler : IS3Handler
{
    private readonly IAmazonS3 _s3Client;
    private const string BucketName = "chatly-app-bucket";

    public S3Handler(IAmazonS3 s3Client)
    {
        _s3Client = s3Client;
    }

    public string UploadFile(ImageDto file)
    {
        using (var newMemoryStream = new MemoryStream(file.Image))
        {
            var uploadRequest = new PutObjectRequest
            {
                InputStream = newMemoryStream,
                Key = Guid.NewGuid().ToString(),
                BucketName = BucketName,
                ContentType = file.ContentType,
                Metadata =
                {
                    ["x-amz-meta-originalfilename"] = file.Name,
                    ["x-amz-meta-extension"] = Path.GetExtension(file.Name)
                }
            };

            _s3Client.PutObjectAsync(uploadRequest).Wait();

            return $"https://{BucketName}.s3.amazonaws.com/{uploadRequest.Key}";
        }
    }
}