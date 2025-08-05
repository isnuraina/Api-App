namespace Api_App.Services.Interfaces
{
    public interface IFileService
    {
        Task<string> UploadFileAsync(IFormFile file, string folder);
        void Delete(string fileName, string folder);
    }
}
