using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VodusTask.Data
{
    public class ColumnConfigurator
    {
        [Key]
        public string Name { get; set; }
        public bool Isvisible { get; set; }
    }
}
