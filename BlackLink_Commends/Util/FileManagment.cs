using BlackLink_SharedKernal.Enum.File;
using Microsoft.AspNetCore.Http;

namespace BlackLink_Commends.Util;

public class FileManagment
{
    public static async Task<List<string>> SaveFiles(IFormFileCollection files, FileType fileEntityName)
    {
        var fileUrls = new List<string>();
        foreach (var file in files)
        {
            string folder = $"{fileEntityName}/" + Guid.NewGuid().ToString() + "_" + file.FileName;
            string serverFolder = Path.Combine("wwwroot/", folder);
            var streem = new FileStream(serverFolder, FileMode.Create);
            await file.CopyToAsync(streem);
            streem.Dispose();
            fileUrls.Add(folder);
        }
        return fileUrls;
    }
    public static async Task<string> SaveFile(IFormFile file, FileType FileName)
    {
        string folder = $"{FileName}/" + Guid.NewGuid().ToString() + "_" + file.FileName;
        string serverFolder = Path.Combine("wwwroot/", folder);
        var x = new FileStream(serverFolder, FileMode.Create);
        await file.CopyToAsync(x);
        await x.DisposeAsync();
        return folder;
    }
    public static void DeleteFile(string url)
    {
        File.Delete("wwwroot/" + url);
    }

    public static void DeleteFiles(List<string> urls)
    {
        urls.ForEach(e => File.Delete("wwwroot/" + e));
    }
}