using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dayspent.Web.API.Binders
{
    public class FileUploadDTO
    {
        public string Title { get; set; }
        public string FileName { get; set; }
        public string FileExtension { get; set; }
        public byte[] Contents { get; set; }
        public string ContentType { get; set; }
    }
}