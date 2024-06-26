using Amazon.S3;
using Amazon.S3.Model;
using Microsoft.AspNetCore.Http;

namespace Data.Infrastructure.S3;

public class S3Handler : IS3Handler
    {
        private readonly IAmazonS3 _s3Client;
        private const string BucketName = "chatly-app-bucket";

        public S3Handler(IAmazonS3 s3Client)
        {
            _s3Client = s3Client;
        }

        public string UploadFile(IFormFile file)
        {
            using (var newMemoryStream = new MemoryStream())
            {
                file.CopyTo(newMemoryStream);

                var uploadRequest = new PutObjectRequest
                {
                    InputStream = newMemoryStream,
                    Key = Guid.NewGuid().ToString(),
                    BucketName = BucketName,
                    Metadata =
                    {
                        ["x-amz-meta-originalfilename"] = file.FileName,
                        ["x-amz-meta-extension"]= Path.GetExtension(file.FileName)
                    }
                };

                _s3Client.PutObjectAsync(uploadRequest).Wait();

                return $"https://{BucketName}.s3.amazonaws.com/{uploadRequest.Key}";
            }
        }
    }
