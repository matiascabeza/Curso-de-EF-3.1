using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication
{
    public class ApplicationDbContext : DbContext
    {
        // Se agrega para indicar que no puede ser nulo
        public ApplicationDbContext([NotNullAttribute] DbContextOptions options) : base(options)
        {

        }
    }
}
