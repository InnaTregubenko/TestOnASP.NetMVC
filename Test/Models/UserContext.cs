using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace Test.Models
{
    public class UserContext : DbContext
    {
        public DbSet<User> Users { get; set; }
    }
}