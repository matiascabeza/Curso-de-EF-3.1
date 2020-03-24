﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCore_Mod4
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DateBirth { get; set; }
        public List<Contact> Contacts { get; set; }
        public StudentDetail Detail { get; set; }
        public List<StudentCourse> StudentCourses { get; set; }
    }

    public class StudentScholar : Student
    {
        public string InstitutionThatAwardsTheScholarship { get; set; }
    }

    public class StudentNotScholar : Student
    {
    }
}