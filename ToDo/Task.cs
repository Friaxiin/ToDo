using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDo
{
    public class Task
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Deadline { get; set; }
        public string Priority { get; set; }
        public string IsCompleted { get; set; }
    }
}
