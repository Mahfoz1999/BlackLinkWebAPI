using BlackLink_SharedKernal.Enum.File;
using Microsoft.AspNetCore.Http;

namespace BlackLink_Repository.Util
{
    public static class FileManagment
    {
        public static async Task<string> SaveFile(FileType fileType, IFormFile file)
        {
            string folder = $"{fileType}/";
            folder += Guid.NewGuid().ToString() + "_" + file.FileName;
            string serverFolder = Path.Combine("wwwroot/", folder);
            await file.CopyToAsync(new FileStream(serverFolder, FileMode.Create));
            return folder;
        }
        public static void DeleteFile(string url)
        {
            File.Delete("wwwroot/" + url);
        }
    }
}
