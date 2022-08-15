using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo_List.Models;

namespace ToDo_List.Context
{
    public class ToDoContext:DbContext
    {
        public ToDoContext(DbContextOptions<ToDoContext> options):base(options)
        {}

        public DbSet<ToDoTask> Tasks { get; set; }
        public  DbSet<User> Users { get; set; }
    }
}
