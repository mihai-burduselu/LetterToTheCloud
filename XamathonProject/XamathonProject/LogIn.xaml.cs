using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace XamathonProject
{
    public partial class LogIn : ContentPage
    {
        public LogIn()
        {
            InitializeComponent();

            this.BackgroundImage = "login.jpg";
        }

        void SendLetter(object sender, EventArgs e)
        {
            String name = Name.Text;
            String email = Email.Text;
            String association = Association.Text;

            ApiCommunication.SendLogInData(name, email, association);

            NavigationPage page = new NavigationPage(new MainPage());
            Navigation.PushAsync(page);
        }
    }
}
