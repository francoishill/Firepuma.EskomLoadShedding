using System;
using System.Text;
using System.Threading.Tasks;
using Firepuma.EskomLoadShedding.FunctionApp.Schedules.Models.ValueObjects;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Formatters;

namespace Firepuma.EskomLoadShedding.FunctionApp.Infrastructure.Formatters;

public class VCalendarOutputFormatter : IOutputFormatter
{
    public bool CanWriteResult(OutputFormatterCanWriteContext context)
    {
        return typeof(VCalendar).IsAssignableFrom(context.ObjectType);
    }

    public async Task WriteAsync(OutputFormatterWriteContext context)
    {
        if (context.Object is not VCalendar calendar)
        {
            throw new ArgumentNullException(nameof(calendar));
        }

        var response = context.HttpContext.Response;

        var buffer = new StringBuilder();
        buffer.AppendLine("BEGIN:VCALENDAR");
        buffer.AppendLine("VERSION:2.0");
        buffer.AppendLine($"X-WR-CALNAME:{calendar.CalendarName}");
        buffer.AppendLine("X-WR-TIMEZONE:Africa/Johannesburg");

        buffer.AppendLine("BEGIN:VTIMEZONE");
        buffer.AppendLine("TZID:Africa/Johannesburg");
        buffer.AppendLine("X-LIC-LOCATION:Africa/Johannesburg");

        buffer.AppendLine("BEGIN:STANDARD");
        buffer.AppendLine("TZOFFSETFROM:+0200");
        buffer.AppendLine("TZOFFSETTO:+0200");
        buffer.AppendLine("TZNAME:SAST");
        buffer.AppendLine("DTSTART:19700101T000000");
        buffer.AppendLine("END:STANDARD");

        buffer.AppendLine("END:VTIMEZONE");

        foreach (var calendarEvent in calendar.CalendarEvents)
        {
            FormatVcard(buffer, calendarEvent);
        }

        buffer.AppendLine("END:VCALENDAR");

        await response.WriteAsync(buffer.ToString());
    }

    private static void FormatVcard(StringBuilder buffer, VCalendar.CalendarEvent calendarEvent)
    {
        var reminderTimeBefore = TimeSpan.FromDays(1);

        buffer.AppendLine("BEGIN:VEVENT");

        buffer.AppendLine($"UID:{calendarEvent.Uid}-{(calendarEvent.Updated ?? calendarEvent.Created):yyyyMMddTHHmmssZ}");
        buffer.AppendLine("DTSTART:" + $"{calendarEvent.Start:yyyyMMddTHHmmssZ}");
        buffer.AppendLine("DTEND:" + $"{calendarEvent.End:yyyyMMddTHHmmssZ}");
        buffer.AppendLine("CREATED:" + $"{calendarEvent.Created:yyyyMMddTHHmmssZ}");
        buffer.AppendLine("LAST-MODIFIED:" + $"{calendarEvent.Updated ?? calendarEvent.Created:yyyyMMddTHHmmssZ}");
        buffer.AppendLine("DTSTAMP:" + $"{DateTime.UtcNow:yyyyMMddTHHmmssZ}"); // This is not about the event but about the iCalendar Object
        buffer.AppendLine("SUMMARY:" + calendarEvent.Summary);
        buffer.AppendLine("CATEGORIES:" + string.Join(",", calendarEvent.Categories));
        buffer.AppendLine($"ORGANIZER:MAILTO:{calendarEvent.Organizer.Email}");

        foreach (var attendee in calendarEvent.Attendees)
        {
            buffer.AppendLine($"ATTENDEE;ROLE=REQ-PARTICIPANT;PARTSTAT=TENTATIVE;CN={attendee.FullName}\n  :MAILTO:{attendee.Email}");
        }

        buffer.AppendLine("BEGIN:VALARM");
        buffer.AppendLine($"TRIGGER:-PT{(int)reminderTimeBefore.TotalMinutes}M");
        buffer.AppendLine("ACTION:DISPLAY");
        buffer.AppendLine("DESCRIPTION:Reminder");
        buffer.AppendLine("END:VALARM");

        buffer.AppendLine("END:VEVENT");
    }
}