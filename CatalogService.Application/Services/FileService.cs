using CatalogService.Application.Interfaces;
using Microsoft.AspNetCore.Http;

namespace CatalogService.Application.Services
{
    public class FileService : IFileService
    {
        private readonly string _baseUploadPath;

        public FileService()
        {
            _baseUploadPath = Path.Combine(Directory.GetCurrentDirectory(), "uploads");
        }

        public string GetBaseUploadPath()
        {
            return _baseUploadPath;
        }

        public async Task<string> UploadFileAsync(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                throw new ArgumentException("Invalid file.");
            }

            // Verifica se o diretório de upload existe, caso contrário, cria-o.
            if (!Directory.Exists(_baseUploadPath))
            {
                Directory.CreateDirectory(_baseUploadPath);
            }

            // Define o caminho completo para salvar o arquivo
            var filePath = Path.Combine(_baseUploadPath, file.FileName);

            // Copia o arquivo para o local de destino
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            // Retorna o caminho relativo que será salvo no banco de dados
            return Path.Combine("uploads", file.FileName);
        }
    }
}