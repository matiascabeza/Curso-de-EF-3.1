﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCore_Mod4
{
    public class StudentCourse
    {
        public int StudentId { get; set; }
        public int CourseId { get; set; }
        public bool IsActive { get; set; }
        public Course Course{ get; set; }
        public Student Student { get; set; }
    }
}
