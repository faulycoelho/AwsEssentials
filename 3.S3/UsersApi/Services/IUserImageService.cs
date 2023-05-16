using Amazon.S3.Model;

namespace Users.Api.Services
{
    public interface IUserImageService
    {
        Task<PutObjectResponse> UploadImageAsync(int id, IFormFile file);

        Task<GetObjectResponse> GetImageAsync(int id);

        Task<DeleteObjectResponse> DeleteImageAsync(int id);
    }
}
