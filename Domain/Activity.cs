using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Activity
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Title { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public string Description { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;   
        public bool IsCancelled { get; set; }


        //location Properties
        public string City { get; set; } = string.Empty;    
        public string Venue { get; set; } = string.Empty;
        public double Latitude { get; set; }    
        public double Longitude { get; set; }


    }
}
