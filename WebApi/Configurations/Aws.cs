using Amazon;
using Amazon.S3;

namespace WebApi.Configurations;

public static class Aws
{
    public static void AddAwsConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        var awsAccessKey = configuration.GetSection("AWS")["AccessKey"];
        var awsSecretKey = configuration.GetSection("AWS")["SecretKey"];
        var awsRegion = configuration.GetSection("AWS")["Region"];

        services.AddSingleton<IAmazonS3>(s => new AmazonS3Client(awsAccessKey, awsSecretKey, RegionEndpoint.GetBySystemName(awsRegion)));
    }
}