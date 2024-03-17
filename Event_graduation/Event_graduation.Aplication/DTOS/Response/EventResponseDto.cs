using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Event_graduation.Aplication.DTOS.Response
{
    public class EventResponseDto
    {

        public int Id { get; set; }
        public string InstitutionName { get; set; }
        public string InstitutionAddress { get; set; }
        public int NumberOfStudents { get; set; }
        public DateTime StartTime { get; set; } 
        public decimal ServicePrice { get; set; }
        public string Email { get; set; }
        public DateTime? CreationDate { get; set; }

        public DateTime? UpdateDate { get; set; }
    }
}
