using Azure.Storage.Blobs;

namespace PaisAPI.Services
{
    public class BlobService : IBlobService
    {
        private readonly string CadeiaConexao = "DefaultEndpointsProtocol=https;AccountName=blobsjuarez;AccountKey=Iy29lAW91W3qE2YaMtekkfVf4R/RnHGObRMO9vJQF7h339CiR3Gvl3kmg9gwHY53GiNI+QkdxkSr+AStFeAvog==;EndpointSuffix=core.windows.net";
        public string CarregaBlob(string bandeiraPais, string tipoBlob)
        {

            string containerName = null;
            switch (tipoBlob)
            {
                case "BandeiraPais":
                    containerName = "bandeiras-paises";
                    break;
                case "BandeiraEstado":
                    containerName = "bandeiras-estados";
                    break;
                case "FotografiaAmigo":
                    containerName = "fotografias-amigos";
                    break;
                default:
                    containerName = "outras";
                    break;
            }

            var blobServiceClient = new BlobServiceClient(CadeiaConexao);
            BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(containerName);

            var id = $"{Guid.NewGuid()}.{GetFileExtension(bandeiraPais)}";
            var blob = containerClient.GetBlobClient(id);

            var sourceData = Convert.FromBase64String(bandeiraPais);
            var uploadData = new BinaryData(sourceData);
            containerClient.UploadBlob(id, uploadData);
            return blob.Uri.AbsoluteUri;
        }
        public string GetFileExtension(string base64String)
        {
            var data = base64String.Substring(0, 5);

            return data.ToUpper() switch
            {
                "IVBOR" => "png",
                "/9J/4" => "jpg",
                "AAAAF" => "mp4",
                "JVBER" => "pdf",
                "AAABA" => "ico",
                "UMFYI" => "rar",
                "E1XYD" => "rtf",
                "U1PKC" => "txt",
                "MQOWM" => "srt",
                "77U/M" => "srt",
                _ => "jpeg"
            };
        }
    }
}
