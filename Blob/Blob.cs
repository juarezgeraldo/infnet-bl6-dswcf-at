using Azure.Storage.Blobs;

namespace Blob
{
    public abstract class Blob
    {
        private string CadeiaConexao = "DefaultEndpointsProtocol=https;AccountName=blobsjuarez;AccountKey=Iy29lAW91W3qE2YaMtekkfVf4R/RnHGObRMO9vJQF7h339CiR3Gvl3kmg9gwHY53GiNI+QkdxkSr+AStFeAvog==;EndpointSuffix=core.windows.net";

        public BlobServiceClient blobServiceClient { get; set; }
        public BlobContainerClient blobContainerClient { get; set; }

        public Blob(string container)
        {
            var blobServiceClient = new BlobServiceClient(CadeiaConexao);

            blobContainerClient = blobServiceClient.GetBlobContainerClient(container);
        }

        public string AdicionarBlob(string base64, string tipoContainer)
        {
            var id = $"{Guid.NewGuid()}.{ExtensaoArquivo(base64)}";
            var blob = blobContainerClient.GetBlobClient(id); 

            //var sourceData = Convert.FromBase64String(base64);
            //var uploadData = new BinaryData(Convert.FromBase64String(base64));
            blob.UploadAsync(new BinaryData(Convert.FromBase64String(base64)), true);

            return blob.Uri.AbsoluteUri;
        }
        private static string ExtensaoArquivo(string base64String)
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
