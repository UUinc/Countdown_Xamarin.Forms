using System;

namespace Countdown.Control
{
    public class Event
    {
        public string EventTitle { get; set; }
        public string BgColor { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Timespan { get; set; }
        public string Days => Timespan.Days.ToString("00");
        public string Hours => Timespan.Hours.ToString("00");
        public string Minutes => Timespan.Minutes.ToString("00");
        public string Seconds => Timespan.Seconds.ToString("00");

        public Event() { }
        public Event(string EventTitle, string BgColor, DateTime Date)
        {
            this.EventTitle = EventTitle;
            this.BgColor = BgColor;
            this.Date = Date;
            Timespan = new TimeSpan(Date.Ticks - DateTime.Now.Ticks);
        }

        public Event GetEvents()
        {
            return this;
        }
    }
}
