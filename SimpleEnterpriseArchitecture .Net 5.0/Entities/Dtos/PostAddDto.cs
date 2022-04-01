using Core.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos
{
    public class PostAddDto:IDto
    {
        public List<string> Files { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
        public int UserId { get; set; }
    }
}
