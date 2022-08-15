using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDo_List.Models
{
    public class ToDoTask
    {
        public int Id { get; set; }

        [Required]
        public string Description { get; set; }
        
        public DateTime DeadLine { get; set; } = DateTime.Now;
        
        public int UserId { get; set; }
    }
}
