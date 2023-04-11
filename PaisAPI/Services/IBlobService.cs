namespace PaisAPI.Services
{
    public interface IBlobService
    {
        string CarregaBlob(string stringBase64, string tipoBlob);
        string GetFileExtension(string stringBase64);
    }
}
