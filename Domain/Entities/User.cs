using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductDomain.Entities
{
    public class User : IdentityUser<long>
    {
        //public string UserName { get; set; } = null!;
        //public string Password { get; set; } = null!;
        //public string Email { get; set; } = null!;
        public string LastLoginTime { get; set; } = null!;
    }
}
