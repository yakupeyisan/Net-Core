using Core.Abstract;
using Core.Utilities.Cloudinary;
using Core.Utilities.IoC;
using Integrations.CloudInary;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Concrete
{
    public class CloudinaryAdapter : ICloudinaryService
    {
        private IConfiguration Configuration;

        public CloudinaryAdapter(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public string UploadImage(string filePath)
        {
            var options = Configuration.GetSection("Cloudinary").Get<CloudinaryAccount>();
            CloudinaryIntegration cloudinary = new CloudinaryIntegration(
                options.Cloud,
                options.ApiKey,
                options.ApiSecret
                );
            return cloudinary.UploadImage(filePath);
        }

        public string UploadVideo(string filePath)
        {
            var options = Configuration.GetSection("Cloudinary").Get<CloudinaryAccount>();
            CloudinaryIntegration cloudinary = new CloudinaryIntegration(
                options.Cloud,
                options.ApiKey,
                options.ApiSecret
                );
            return cloudinary.UploadVideo(filePath);
        }
    }
}
