using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoEFCoreWinforms.Models
{
    class Student
    {
        public int Id { get; set; }
        private string _name;
        public string Name 
        {
            get 
            {
                return _name;
            }
            set 
            {
                _name = string.Join("", value.Split(' ').Select(x => x[0].ToString().ToUpper() + x.Substring(1).ToLower()).ToArray());
            }

        }
        public DateTime DateBirth { get; set; }
        public bool ItsErased { get; set; }

    }
}
