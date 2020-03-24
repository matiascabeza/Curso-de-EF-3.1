using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCore_Mod4
{
    public class Contact
    {
        public int Id { get; set; }
        public int Name { get; set; }
        public int Relation { get; set; }
        public int StudentId { get; set; }
        public Student Student { get; set; }
    }
}
