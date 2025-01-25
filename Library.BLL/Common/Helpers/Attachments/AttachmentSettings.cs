using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;

namespace Library.BLL.Common.Helpers.Attachments
{
    public class AttachmentSettings : IAttachmentSettings
    {
        private readonly List<string> _allowedExtensions = new() { ".jpg", ".jpeg", ".png" };
        private const int MaxFileSize = 2 * 1024 * 1024; // 2 MB

        public string Upload(IFormFile file, string folderName)
        {
            if (file == null || file.Length == 0)
                throw new InvalidOperationException("The file is empty or not provided.");

            var extension = Path.GetExtension(file.FileName).ToLower();

            if (!_allowedExtensions.Contains(extension))
                throw new InvalidOperationException("Unsupported file type.");

            if (file.Length > MaxFileSize)
                throw new InvalidOperationException($"File size exceeds the maximum allowed size of {MaxFileSize / 1_048_576} MB.");

            var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "files", folderName);

            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            var fileName = $"{Guid.NewGuid()}{extension}";
            var filePath = Path.Combine(folderPath, fileName);

            using var fileStream = new FileStream(filePath, FileMode.Create);
            file.CopyTo(fileStream);

            return fileName;
        }

        public bool Delete(string filePath)
        {
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
                return true;
            }
            return false;
        }

    }
}
