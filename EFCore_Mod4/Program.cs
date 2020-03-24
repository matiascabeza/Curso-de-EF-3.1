using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCore_Mod4
{
    class Program
    {
        static void Main(string[] args)
        {
            InsertDataRelated();
        }

        static void InsertDataRelated()
        {
            using (var context = new ApplicationDbContext())
            {
                var studentId = context.Students.Select(x => x.Id).FirstOrDefault();

                var contact = new Contact();
                contact.Name = "Yessuca Krystal";
                contact.Relation = "Hermana";
                contact.StudentId = studentId;

                context.Add(contact);
                context.SaveChanges();
            }           
        }


    }
}
