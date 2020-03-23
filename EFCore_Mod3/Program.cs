using DemoEFCoreWinforms.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCore_Mod3
{
    class Program
    {
        static void Main(string[] args)
        {
            UpdateModeDesConnectRegistry();
        }

        static void InsertRegistros() 
        {
            using (var context = new ApplicationDbContext())
            {
                var student1 = new Student();
                // paso 1: Creamos el Objecto
                student1.Name = "Felipe Gavilán";
                student1.DateBirth = new DateTime(1934, 7, 20);

                // paso 2: Notificamos que queremos agregar un estudiante
                context.Students.Add(student1);

                // paso 3: Guardamos los mitos
                context.SaveChanges();
            }
        }
        static void InsertMultipleRegistros()
        {
            using (var context = new ApplicationDbContext())
            {
                // paso 1: Creamos el Objecto
                var student1 = new Student();
                student1.Name = "Felipe Gavilán";
                student1.DateBirth = new DateTime(1934, 7, 20); 
                var student2 = new Student();
                student1.Name = "Matias Cabeza";
                student1.DateBirth = new DateTime(1988, 5, 21);

                IList<Student> list1 = new List<Student>() { student1, student2 };
                IList<Student> list2 = new List<Student>();
                for(int i = 0; i < 10; i++)
                {
                    list2.Add(new Student()
                    {
                        Name = "Estudiante " + i.ToString(),
                        DateBirth = new DateTime(1900 + i, 1, 2)
                    });
                }
                // paso 2: Notificamos que queremos agregar un estudiante
                context.Students.AddRange(list1);
                context.Students.AddRange(list2);

                // paso 3: Guardamos los mitos
                context.SaveChanges();
            }


        }
        static void MapeoFlexible()
        {
            using (var context = new ApplicationDbContext())
            {
                var student1 = new Student();
                student1.Name = "jeLipe GaVilán";
                student1.DateBirth = new DateTime(1934, 7, 20); 
                context.Students.Add(student1);
                context.SaveChanges();
            }
        }
        static void ReadAllDataBase()
        {
            using (var context = new ApplicationDbContext())
            {
                //var studens = context.Students.ToList();
                var studens = context.Students.OrderByDescending(x => x.DateBirth).ThenBy(x => x.Name).ToList();
            }
        }
        static void FirstAndFirstOrDefaultDataBase()
        {
            using (var context = new ApplicationDbContext())
            {
                
                //Da error si no hay registro  en la tabla estudiantes
                var studens1 = context.Students.First(x => x.Name.StartsWith("Felipe"));

                //Trae nulo si no existen registros en la tabla estudiantes
                var studens2 = context.Students.FirstOrDefault(x => x.Name.StartsWith("Felipe"));

            }
        }
        static void SelectColumnsName()
        {
            using (var context = new ApplicationDbContext())
            {

                //Trae solo el nombre
                var studens1 = context.Students.Select(x => x.Name).FirstOrDefault();
                //Proyeccion a un tipo anonimo
                var studens2 = context.Students.Select(x => new { x.Id, x.Name}).ToList();
                //Proyeccion a una clase
                var studens3 = context.Students.Select(x => new Student {Id = x.Id, Name = x.Name }).ToList();

            }
        }
        static void FilterRegistry()
        {
            using (var context = new ApplicationDbContext())
            {
                var studens1 = context.Students.Where(x => x.Id == 5).FirstOrDefault();
                var studens2 = context.Students.Where(x => x.DateBirth <= DateTime.Today.AddYears(-30)).ToList();
            }
        }

        static void UpdateModeConnectRegistry()
        {
            //Actulizacion de registro en modelo conectado
            using (var context = new ApplicationDbContext())
            {
                var studens1 = context.Students.First(x => x.Name.StartsWith("Felipe"));
                studens1.Name = "Matias";
                context.SaveChanges();
            }

        }

        static void UpdateModeDesConnectRegistry()
        {
            //Actulizacion de registro en modelo desconectado
            Student matias;
            using (var context = new ApplicationDbContext())
            {
                matias = context.Students.First(x => x.Name.StartsWith("Matias"));
            }

            matias.Name += " Purcell";

            //Se fija cual es la propiedad que se modifico y la actualiza
            using (var context = new ApplicationDbContext())
            {
                context.Entry(matias).State = EntityState.Modified;
                context.SaveChanges();
            }

            //Solo actualiza el nombre de la propiedad
            using (var context = new ApplicationDbContext())
            {
                var entrada = context.Attach(matias);
                entrada.Property(x => x.Name).IsModified = true;
                context.SaveChanges();
            }
        }


    }
}
