using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blob
{
    public class BlobService
    {
        public string AdicionarBlob(string base64, string tipoContainer)
        {
            if (string.IsNullOrEmpty(base64))
                return string.Empty;

            Blob blob = null;

            return blob.AdicionarBlob(base64, tipoContainer);
        }
    }
}
