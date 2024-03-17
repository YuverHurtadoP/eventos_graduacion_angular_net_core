using System;
using System.Collections.Generic;

namespace Event_graduation.Models;

public  class Event
{
    public int Id { get; set; }

    public string InstitutionName { get; set; } = null!;

    public string InstitutionAddress { get; set; } = null!;

    public int NumberOfStudents { get; set; }

    public DateTime StartTime { get; set; }

    public decimal ServicePrice { get; set; }

    public string? Email { get; set; }

    public DateTime? CreationDate { get; set; }

    public DateTime? UpdateDate { get; set; }
}
