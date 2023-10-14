﻿namespace AprajitaRetails.Mobile.Services
{
    using System.Reflection;
    using System.Threading.Tasks;

    public sealed class FileService : IFilesService
    {
        public string InstallationPath => Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

        public async Task<Stream> OpenForReadAsync(string path)
        {
            return await GetEmbeddedFileStreamAsync(GetType(), path);
        }


        private static async Task<Stream> GetEmbeddedFileStreamAsync(Type assemblyType, string fileName)
        {
            await Task.Yield();

            var manifestName = assemblyType.GetTypeInfo().Assembly
                .GetManifestResourceNames()
                .FirstOrDefault(n => n.EndsWith(fileName.Replace(" ", "_").Replace("\\", ".").Replace("/", "."), StringComparison.OrdinalIgnoreCase));

            if (manifestName == null)
            {
                throw new InvalidOperationException($"Failed to find resource [{fileName}]");
            }

            return assemblyType.GetTypeInfo().Assembly.GetManifestResourceStream(manifestName);
        }
    }

}