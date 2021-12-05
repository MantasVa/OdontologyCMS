using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace Odontology.Business.Helpers
{
    internal static class ImageHelper
    {
        private static readonly List<string> AllowedContentType = new(){ "image/png", "image/jpeg" };
        public static List<byte[]> ConvertToBytes(this IEnumerable<IFormFile> files) 
            => files.Select(file => file.ConvertToBytes()).ToList();

        public static byte[] ConvertToBytes(this IFormFile file)
        {
            if (file == null || file.Length == 0)
                throw new ArgumentNullException("Image does not exist");
            
            if (!IsValidContentType(file.ContentType)) 
                throw new ArgumentException("Not supported image type");

            using var reader = new BinaryReader(file.OpenReadStream());
            return reader.ReadBytes((int)file.Length);
        }

        private static bool IsValidContentType(string contentType) => AllowedContentType.Contains(contentType);

    }
}
