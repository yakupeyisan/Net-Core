using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integrations.CloudInary
{
    public class CloudinaryIntegration
    {
        private Cloudinary _cloudinary;

        public CloudinaryIntegration(string cloud, string apiKey, string apiSecret)
        {
            Account account = new Account(cloud,apiKey,apiSecret);
            _cloudinary = new Cloudinary(account);
        }
        public string UploadImage(string filePath)
        {
            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(filePath)
            };
            var uploadResult = _cloudinary.Upload(uploadParams);
            return uploadResult.Url.ToString();
        }
        public string UploadVideo(string filePath)
        {
            var uploadParams = new VideoUploadParams()
            {
                File = new FileDescription(filePath)
            };
            var uploadResult = _cloudinary.Upload(uploadParams);
            return uploadResult.Url.ToString();
        }
    }
}
