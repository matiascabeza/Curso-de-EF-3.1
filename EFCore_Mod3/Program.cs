﻿using DemoEFCoreWinforms.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Transactions;

namespace EFCore_Mod3
{
    class Program
    {
        static void Main(string[] args) { TransationEnvironment(); }

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
                for (int i = 0; i < 10; i++)
                {
                    list2.Add(new Student()
                    { Name = "Estudiante " + i.ToString(), DateBirth = new DateTime(1900 + i, 1, 2) });
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
                var studens2 = context.Students.Select(x => new { x.Id, x.Name }).ToList();
                //Proyeccion a una clase
                var studens3 = context.Students.Select(x => new Student { Id = x.Id, Name = x.Name }).ToList();
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

        static void DeleteConnectRegistry()
        {
            //Elimina el registro en modelo desconectado
            using (var context = new ApplicationDbContext())
            {
                var student = context.Students.FirstOrDefault();
                if (student != null)
                {
                    Console.WriteLine($"Estudiante a ser removido: { student.Name}");
                    context.Remove(student);
                    context.SaveChanges();
                }
            }
        }

        static void DeleteDesConnectRegistry()
        {
            //Eliminar de registro en modelo desconectado
            Student student;
            using (var context = new ApplicationDbContext())
            {
                student = context.Students.FirstOrDefault();
            }

            using (var context = new ApplicationDbContext())
            {
                var student1 = new Student();
                student1.Id = student.Id;
                context.Entry(student).State = EntityState.Deleted;
                context.SaveChanges();
            }
        }
        static void FilterLevelModel()
        {
            //Elimina el registro en modelo desconectado
            using (var context = new ApplicationDbContext())
            {
                var studens1 = context.Students.ToList();
                var studens2 = context.Students.IgnoreQueryFilters().ToList();
            }
        }

        static void PaginationSkipAndTake()
        {
            //Paginacion de registros 
            using (var context = new ApplicationDbContext())
            {
                var studens1 = context.Students.Skip(3).Take(5).ToList();
                var pagination = 1;
                var show = 1;

                var studens2 = context.Students.Skip((pagination - 1) * show).Take(show).ToList();
            }
        }

        static void GroupByRegistry()
        {
            //Reporte de registros con GroupBy
            using (var context = new ApplicationDbContext())
            {
                var report1 = context.Students.IgnoreQueryFilters()
                    .GroupBy(x => new { x.ItsErased })
                    .Select(x => new { x.Key, Count = x.Count()}).ToList();

                var report2 = context.Students.IgnoreQueryFilters()
                    .GroupBy(x => new { x.DateBirth.Year })
                    .Where( x=> x.Count() >= 2)
                    .Select(x => new { x.Key, Count = x.Count() }).ToList();
            }
        }

        static void FromSqRawlRegistry()
        {
            var Id = 4;
            //Reporte de registros con GroupBy
            using (var context = new ApplicationDbContext())
            {
                var report1 = context.Students
                    .FromSqlRaw(@"SELECT * FROM Students" )
                    .FirstOrDefault();
            }
        }

        static void FromSqlInterpolated()
        {
            var Id = 4;
            //Reporte de registros con GroupBy
            using (var context = new ApplicationDbContext())
            {
                var report1 = context.Students
                    .FromSqlInterpolated($"SELECT * FROM Students WHERE Id = {Id}")
                    .FirstOrDefault();
            }
        }

        static void TransationCommon()
        {
            using (var context = new ApplicationDbContext())
            {
                using (var trasaction = context.Database.BeginTransaction())
                {
                    var student1 = new Student();
                    student1.Name = "Felipe1 Fato";
                    student1.DateBirth = new DateTime(1934, 7, 20);
                    context.Students.Add(student1);
                    context.SaveChanges();
                    // El Id tendra un valor valido
                    Console.WriteLine("Id del estudiante" + student1.Id);
                    //vamos a revertir la operacion realizada
                    trasaction.Commit();
                }
                    
            }
        }

        static void TransationEnvironment()
        {
            //Verificar difencia entre los using de TransactionScope
            //Transacion de ambiente que ocurren por hilo
            using (var scope = new TransactionScope())
            {
                using (var context = new ApplicationDbContext())
                {

                    var student1 = new Student();
                    student1.Name = "Transaction Scope 1";
                    context.Students.Add(student1);
                    context.SaveChanges();
                }

                using (var context = new ApplicationDbContext())
                {

                    var student2 = new Student();
                    student2.Name = "Transaction Scope 2";
                    context.Students.Add(student2);
                    context.SaveChanges();
                }
                //Descomenta la siguiente linea si quiere que se persista en la base de datos
                //scope.Complete();
            }
        }
        //Ejecucion Diferida
        static void DeferredExecution()
        {
            //Reporte de registros con GroupBy
            using (var context = new ApplicationDbContext())
            {
                //Forma 1: Funciones en una linea 
                var report1 = context.Students
                    .Where(x => x.DateBirth.Year < 1990)
                    .OrderByDescending(x => x.DateBirth).ToList();

                //Forma 2: Funciones en una linea
                var query = context.Students.AsQueryable();
                query = query.Where(x => x.DateBirth.Year < 1990);
                query = query.OrderByDescending(x => x.DateBirth);
                var report2 = query.ToList();
            }
        }
    }
}
