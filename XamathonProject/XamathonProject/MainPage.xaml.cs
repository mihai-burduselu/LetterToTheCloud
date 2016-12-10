using System;
using Xamarin.Forms;

namespace XamathonProject
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            this.BackgroundImage = "background.jpg";

        }

        void SendLetter(object sender, EventArgs e)
        {
            String letter = writeLetter.Text;
            String name = nameEntry.Text;

            ApiCommunication.MakeRequests(letter,name);

            NavigationPage page = new NavigationPage(new SendLetter());
            Navigation.PushAsync(page); 
        }
    }
}
