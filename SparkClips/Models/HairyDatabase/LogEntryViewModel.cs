using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SparkClips.Models.HairyDatabase
{
    public class LogEntryViewModel
    {
        public string Description { get; set; }
        [DataType(DataType.Currency)]
        public decimal? Cost { get; set; } // make decimal a nullable type so that ef core doesn't make it required by convention
        [Display(Name = "Haircut Date")]
        [DataType(DataType.Date)]
        public DateTime DateTimeCreated { get; set; } // Gets auto set on first save
        public string Location { get; set; }
        public string Barbers { get; set; }

        public ApplicationUser ApplicationUser { get; set; }

    }
}
