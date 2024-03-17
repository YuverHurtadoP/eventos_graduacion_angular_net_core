using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Event_graduation.Aplication.DTOS.Request
{
    public class EventRequestDto
    {
        public string InstitutionName { get; set; }
        public string InstitutionAddress { get; set; }
        public int NumberOfStudents { get; set; }
        public DateTime StartTime { get; set; }
        public decimal ServicePrice { get; set; }
        public string? Email { get; set; }
    }
}
