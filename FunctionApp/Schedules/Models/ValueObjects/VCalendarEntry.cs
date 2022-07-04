using System;
using System.Collections.Generic;

namespace Firepuma.EskomLoadShedding.FunctionApp.Schedules.Models.ValueObjects;

public class VCalendar
{
    public string CalendarName { get; set; }

    public IEnumerable<CalendarEvent> CalendarEvents { get; set; }

    public class CalendarEvent
    {
        public Member Organizer { get; set; }

        public IEnumerable<Member> Attendees { get; set; }

        public string Uid { get; set; }

        public string Summary { get; set; }

        public DateTime Start { get; set; }

        public DateTime End { get; set; }

        public DateTime Created { get; set; }

        public DateTime? Updated { get; set; }
        public IEnumerable<string> Categories { get; set; }

        public class Member
        {
            public string FullName { get; set; }
            public string Email { get; set; }

            public Member(string fullName, string email)
            {
                FullName = fullName;
                Email = email;
            }
        }
    }
}