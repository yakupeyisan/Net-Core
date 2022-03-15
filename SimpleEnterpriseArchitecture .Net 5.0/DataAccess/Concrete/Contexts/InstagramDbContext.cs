using Core.Utilities.IoC;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Core.Entities.Concrete;
using Entities.Concrete;

namespace DataAccess.Concrete.Contexts
{
    public class InstagramDbContext:DbContext
    {
        private IConfiguration Configuration;
        public InstagramDbContext()
        {
            Configuration = ServiceTool.ServiceProvider.GetService<IConfiguration>();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
        }
        DbSet<User> Users { get; set; }
        DbSet<Employee> Employees { get; set; }
        DbSet<Account> Accounts { get; set; }
        DbSet<AccountAvatar> AccountAvatars { get; set; }
        DbSet<PostInformation> PostInformations { get; set; }
        DbSet<PostTag> PostTags { get; set; }
        DbSet<PostComment> PostComments { get; set; }
        DbSet<PostLike> PostLikes { get; set; }
        DbSet<TextMessage> TextMessages { get; set; }
    }
}
