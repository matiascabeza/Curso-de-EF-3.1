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
            //EagerLoading();
            //LazyLoading();
            //RelatedUnotoUno();
            //RelatedOneToMany();
            //InsertCourses();
            //RelatedManyToMany();
            ReadRelatedManyToMany();
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

        static void LazyLoading()
        {
            using (var context = new ApplicationDbContext())
            {
                var student = context.Students.FirstOrDefault();
                var contacts = student.Contacts.ToList();
                //Ya cargado los contactos ef no cargara de nuevo los contactos
                foreach(var contact in student.Contacts)
                {


                }
            }
        }

        static void RelatedUnotoUno()
        {
            int studentId;
            using (var context = new ApplicationDbContext())
            {
                studentId = context.Students.Select(x => x.Id).FirstOrDefault();
            }
            using (var context = new ApplicationDbContext())
            {
                var studentDetails = new StudentDetail();

                studentDetails.Identification = "123-456789-1";
                studentDetails.StudentId = studentId;
                context.StudentDetails.Add(studentDetails);
                context.SaveChanges();
            }
        }

        static void RelatedOneToMany()
        {
            using (var context = new ApplicationDbContext())
            {
                var students = context.Students.Include(x => x.Detail).ToList();
            }
        }

        static void InsertCourses()
        {
            using (var context = new ApplicationDbContext())
            {
                var course1 = new Course();
                course1.Name = "Programacion";
                var course2 = new Course();
                course2.Name = "Calculo";
                var course3 = new Course();
                course3.Name = "Estadistica";

                context.AddRange(new Course[] { course1, course2, course3 });
                context.SaveChanges();
            }
        }

        static void RelatedManyToMany()
        {
            using (var context = new ApplicationDbContext())
            {
                var course1 = context.Courses.First();
                var student1 = context.Students.First();

                var studentCourse = new StudentCourse();
                studentCourse.CourseId = course1.Id;
                studentCourse.StudentId = student1.Id;

                context.Add(studentCourse);
                context.SaveChanges();
            }
        }

        static void ReadRelatedManyToMany()
        {
            using (var context = new ApplicationDbContext())
            {
                //Traemos toda la data de estudiantecursos
                var studentCoursesAll = context.StudentCourses.ToList();

                //Traemos toda la data de estudiamtecursos incluyendo la informacion de los estudiantes y cursos
                var studentCoursesRelated = context.StudentCourses.Include(x => x.Student).Include(x => x.Course).ToList();
                var studentId = context.Students.Select(x=> x.Id).First();

                //Traemos todos los cursos de un estudiante especifico
                var studentCourses = context.StudentCourses.Where(x => x.StudentId == studentId).Include(x => x.Course).ToList();

            }
        }

    }
}
