
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Threading;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Timer = System.Timers.Timer;
using Android.Webkit;

namespace PinCode
{
    public partial class MainPage : ContentPage
    {
        System.Timers.Timer timer = new System.Timers.Timer();

        public MainPage()
        {
            InitializeComponent();
            DAO d = new DAO();
            d.InitializeDatabase();
            BackgroundImage = "Assets/background1.png";
            SetDefaultSettings();
        }


        public void playSimpleSound()
        {

            var assembly = IntrospectionExtensions.GetTypeInfo(typeof(MainPage)).Assembly;
            Stream audioStream =
                assembly.GetManifestResourceStream(
                        "PinCode.Assets.Sounds.sound.mp3");
            var player = Plugin.SimpleAudioPlayer.CrossSimpleAudioPlayer.Current;
            player.Load(audioStream);

            //turn on/off the background sound
            var soundOnOff = soundPicker.SelectedIndex;
            {
                player.Play();
                player.Loop = true;
            }
            if (soundOnOff == 1)
            {
                try
                {
                    var memStream = new MemoryStream();
                    player.Stop();
                }
                catch
                {

                }
                
                
            }
        }


        private void SetDefaultSettings()
        {
            var soundMode = soundPicker.SelectedIndex;

        }

        private void AccountBtn_Clicked(object sender, EventArgs e)
        {
            //Go to Account page
             Navigation.PushAsync(new Account());
        }

        private void FeedbackBtn_Clicked(object sender, EventArgs e)
        {
            //Go to Feedback page
            Navigation.PushAsync(new Feedback());
        }

        private void StartBtn_Clicked(object sender, EventArgs e)
        {
          
            var gameBoardType = GameBoardPicker.SelectedIndex;
            if (gameBoardType == 0)
            {
                Navigation.PushAsync(new Roullete());
            }
            else
            {
                Navigation.PushAsync(new Square());
            }

                playSimpleSound(); 
        }

        //Exit Application.
        private void ExitBtn_Clicked(object sender, EventArgs e)
        {
            System.Environment.Exit(1);
        }


    }
}
