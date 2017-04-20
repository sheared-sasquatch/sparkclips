using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sparkclips.Models
{
    public class LogEntry
    {
        public int ID { get; set; }
        public string BarberShop { get; set; }
        public string Location { get; set; }
        public DateTime DateOfCut { get; set; }

        public static List<LogEntry> GetFakeData()
        {
            List<LogEntry> list = new List<LogEntry>();

            list.Add(new LogEntry
            {
                ID = 1,
                BarberShop = "Hair Master's",
                Location = "Northgate",
                DateOfCut = new DateTime(2017, 1, 13)
            });

            list.Add(new LogEntry
            {
                ID = 2,
                BarberShop = "SuperCuts",
                Location = "Northgate",
                DateOfCut = new DateTime(2017, 2, 22)
            });

            list.Add(new LogEntry
            {
                ID = 3,
                BarberShop = "Rudy's",
                Location = "The Ave",
                DateOfCut = new DateTime(2017, 4, 19)
            });


            return list;
        }
    }

}
