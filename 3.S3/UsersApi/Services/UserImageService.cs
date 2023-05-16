using Amazon.S3;
using Amazon.S3.Model;

namespace Users.Api.Services
{
    public class UserImageService : IUserImageService
    {
        private readonly IAmazonS3 _s3;
        private readonly string _bucketName = "faulycoelho.bucket";

        public UserImageService(IAmazonS3 s3)
        {
            _s3 = s3;
        }

        public async Task<PutObjectResponse> UploadImageAsync(int id, IFormFile file)
        {
            var putObjectRequest = new PutObjectRequest
            {
                BucketName = _bucketName,
                Key = $"images/{id}",
                ContentType = file.ContentType,
                InputStream = file.OpenReadStream(),
                Metadata =
            {
                ["x-amz-meta-originalname"] = file.FileName,
                ["x-amz-meta-extension"] = Path.GetExtension(file.FileName),
            }
            };

            return await _s3.PutObjectAsync(putObjectRequest);
        }

        public async Task<GetObjectResponse> GetImageAsync(int id)
        {
            var getObjectRequest = new GetObjectRequest
            {
                BucketName = _bucketName,
                Key = $"images/{id}"
            };

            return await _s3.GetObjectAsync(getObjectRequest);
        }

        public async Task<DeleteObjectResponse> DeleteImageAsync(int id)
        {
            var deleteObjectRequest = new DeleteObjectRequest
            {
                BucketName = _bucketName,
                Key = $"images/{id}"
            };

            return await _s3.DeleteObjectAsync(deleteObjectRequest);
        }
    }
}
