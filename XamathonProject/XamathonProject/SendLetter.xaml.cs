using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace XamathonProject
{
    public partial class SendLetter : ContentPage
    {
        public SendLetter()
        {
            InitializeComponent();
            this.BackgroundImage = "christmas_texture2632.jpg";

            wish.TextColor = Color.White;
            playMusic.TextColor = Color.White;
        }

        void PlayMusic(object sender, EventArgs e)
        {
            DependencyService.Get<IAudio>().Play("sound.mp3");
        }
    }
}
