using Azure;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;

namespace GerEsportes_BackEnd.Infra.Documentos
{
    public class FileControll
    {
        private readonly string _conectionString = "DefaultEndpointsProtocol=https;AccountName=gerdocumentos;AccountKey=e4LE682lHMrVKNM/kFhVR+iOi2ACqDxdPh5cNDE2QwDmwTotjsJ54wJfr/QJu6eL6jETFZsDSNZo+AStOibgJg==;EndpointSuffix=core.windows.net";
        private readonly string _container = "documentousuario";
        private readonly string _chaveAcces = "r4kAkFTWUriHqRbhDjgHWfYXoTBOohxm8e2qbalMOO1h+cmDioXx+AmOIIb9wVg0NRmRwMNbkQk7+AStw0F9lA==";

        public string UploadBase64(string base64Image, string guide)
        {
            // Gera um nome randomico para imagem
            var fileName = guide;

            // Gera um array de Bytes
            byte[] imageBytes = Convert.FromBase64String(base64Image);

            // Define o BLOB no qual a imagem será armazenada
            var blobClient =
                new BlobClient(_conectionString, _container, fileName);

            // Envia a imagem
            using (var stream = new MemoryStream(imageBytes))
            {
                blobClient.Upload(stream);
            }

            return blobClient.Uri.AbsoluteUri;
        }

        public async Task DeletarArquivo(string guidArquivo)
        {
            BlobServiceClient blobServiceClient = new BlobServiceClient(_conectionString);
            BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(_container);

            BlobClient blobClient = containerClient.GetBlobClient(guidArquivo);

            // Verifica se o blob existe
            if (await blobClient.ExistsAsync())
            {
                // Exclui o blob
                await blobClient.DeleteIfExistsAsync();
            }
            else
            {
                throw new FileNotFoundException($"O Arquivo '{guidArquivo}' não foi encontrado no armazenamento!");
            }
        }

        public async Task<byte[]> RecuperarBase64Arquivo(string guidArquivo)
        {
            BlobServiceClient blobServiceClient = new BlobServiceClient(_conectionString);
            BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(_container);

            BlobClient blobClient = containerClient.GetBlobClient(guidArquivo);

            // Verifica se o blob existe
            if (await blobClient.ExistsAsync())
            {
                // Baixa o blob como um fluxo de bytes
                Response<BlobDownloadInfo> response = await blobClient.DownloadAsync();

                // Lê os bytes do fluxo
                using (MemoryStream ms = new MemoryStream())
                {
                    await response.Value.Content.CopyToAsync(ms);
                    byte[] imageBytes = ms.ToArray();

                    // Converte os bytes da imagem para base64
                    string base64String = Convert.ToBase64String(imageBytes);
                    return imageBytes;
                }
            }
            else
            {
                throw new FileNotFoundException($"O Arquivo '{guidArquivo}' não foi encontrado no armazenamento!");
            }
        }
    }
}
