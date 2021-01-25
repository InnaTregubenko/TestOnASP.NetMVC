using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Data.Entity;

namespace Test.Models
{
    public class UserDbInitializer : DropCreateDatabaseAlways <UserContext>
    {
        protected override void Seed(UserContext db)
        {
            db.Users.Add(new User { Name = "Ivan", Married = true, DateofBirth = new DateTime(2015, 7, 20), Phone = "+380000000", Salary = 1500.0m});
            db.SaveChanges();
            base.Seed(db);
        }
    }
}