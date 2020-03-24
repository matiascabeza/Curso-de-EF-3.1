using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Text;
using System.Threading.Tasks;

namespace EFCore_Mod4
{
    class Program
    {
        static void Main(string[] args)
        {
            //InsertDataRelated();
            EagerLoading();
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

        static void EagerLoading()
        {
            using (var context = new ApplicationDbContext())
            {
                // Opcion 1: Include con expresion lambda
                var student1 = context.Students.Include(x => x.Contacts).ToList();
                // Opcion 2: Include con expresion lambda
                var student2 = context.Students.Include("Contacts").ToList();
            }
        }

    }
}
