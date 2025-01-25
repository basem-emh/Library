using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.BLL.Common.Helpers.Attachments
{
    public interface IAttachmentSettings
    {
        string Upload(IFormFile file ,string folderName);
        bool Delete(string filePath);
    }
}
