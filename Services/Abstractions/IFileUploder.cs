namespace GasHub.Services.Abstractions
{
    public interface IFileUploader
    {
        Task<string> ImgUploader(IFormFile file);
        Task<bool> DeleteFile(string fileName);
    }
}
