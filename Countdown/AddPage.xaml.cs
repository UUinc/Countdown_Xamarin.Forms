using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Countdown.Control;
using Plugin.LocalNotifications;

namespace Countdown
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddPage : ContentPage
    {
        public AddPage()
        {
            InitializeComponent();
        }

        Event GetEvent()
        {
            return new Event(EventName.Text, EventColor.Text, new DateTime(EventDate.Date.Ticks + EventTime.Time.Ticks));
        }

        async private void Button_Clicked(object sender, EventArgs e)
        {
            MainPage.AddEvent(GetEvent());

            CrossLocalNotifications.Current.Show("Countdown", "Check out \"" + EventName.Text + "\" event is happening right now!", 0, new DateTime(EventDate.Date.Ticks + EventTime.Time.Ticks));
            CrossLocalNotifications.Current.Show("Countdown", "Your \"" + EventName.Text + "\" event is Today, Be ready.", 1, EventDate.Date);

            await Navigation.PopAsync();
        }

        private void ColorBTN(object sender, EventArgs e)
        {
            var btn = sender as Button;
            var color = btn.BackgroundColor;

            EventColor.Text = color.ToHex();
        }
    }
}