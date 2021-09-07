using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Essentials;
using Countdown.Control;
using Newtonsoft.Json;

namespace Countdown
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            Setup();
        }

        private static string eventKey = "Events";
        public static List<Event> AllEvents { get; set; } = new List<Event>();

        public static void AddEvent(Event evt)
        {
            AllEvents.Add(evt);
            SaveEvent();
        }
        public static void AddEvent(string EventTitle, string BgColor, DateTime Date)
        {
            AllEvents.Add(new Event(EventTitle, BgColor, Date));
            SaveEvent();
        }
        public static void DeleteEvent(Event evt)
        {
            AllEvents.Remove(evt);
            SaveEvent();
        }
        private static void SaveEvent()
        {
            Preferences.Set(eventKey, JsonConvert.SerializeObject(AllEvents));
        }
        private List<Event> GetEvents()
        {
            AllEvents = JsonConvert.DeserializeObject<List<Event>>(Preferences.Get(eventKey, "default_value"));
            return AllEvents;
        }

        void CheckEvent()
        {
            if(Preferences.ContainsKey(eventKey))
            {
                AllEvents = GetEvents();
            }
            else
            {
                SaveEvent();
            }
        }
        private void Setup()
        {
            CheckEvent();

            evenList.ItemsSource = AllEvents;

            Device.StartTimer(new TimeSpan(0, 0, 1), () =>
            {
                foreach (var evt in AllEvents)
                {
                    var timespan = evt.Date - DateTime.Now;
                    evt.Timespan = timespan;
                }

                evenList.ItemsSource = null;
                evenList.ItemsSource = AllEvents;

                return true;
            });
        }

        async private void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            if(GetEvents().Count > 4)
            {
                await DisplayAlert("Alert", "You can't add more than 5 events, Try to delete an event by clicking on it", "OK");
                return;
            }
            await Navigation.PushAsync(new AddPage());
        }

        async private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            var frame = sender as Frame;
            var _event = frame.BindingContext as Event;

            if (await DisplayAlert("Delete", "Are you sure want to delete this event?", "delete", "cancel"))
            {
                DeleteEvent(_event);
            }
        }
    }
}
