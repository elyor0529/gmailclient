using GmailClient.WebUI.BLL.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace GmailClient.WebUI.DAL
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", false)
        {
        }


        /// <summary>
        /// create instance apply to Owin dependencies
        /// </summary>
        /// <returns></returns>
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}