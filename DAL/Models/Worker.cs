using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace DAL.Models
{
    public class Worker
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTimeOffset StartWorkingDate { get; set; }
        public string Special { get; set; }
        public bool Gender { get; set; }

        public WorkerAnket WorkerAnket { get; set; }
    }

    public class WorkerAnket
    {
        [Key]
        [ForeignKey("Worker")]
        public int Id { get; set; }
        public string Quotation { get; set; }

        public string Image { get; set; }

        public Worker Worker { get; set; }
    }
}
