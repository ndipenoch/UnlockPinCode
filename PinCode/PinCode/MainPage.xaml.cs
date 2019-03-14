
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
            //d.AddUserDetails("mark", "Marco", "Ndip", "jimmy@gmail.com", "000000", "29 Forser Place", "LA", "USA", 200,50);
           // d.AddUserDetails("james", "James", "Ndip", "jimmy@gmail.com", "000000", "29 Forser Place", "NY", "USA", 400, 260);
            //d.UpdateUserDetails("mark","James","Ndip","jimmy@gmail.com","000000","29 Forser Place","LA", "USA", 200);
            //d.AddUserDetails("markavella", "mark", "Ndip", "jimmy@gmail.com", "000000", "29 Forser Place", "NY", "USA", 1000);
            //d.DeleteUserAccount("marko");
            // d.DeleteUserAccount("markavella");
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

            //turn on the background sound
            var soundOnOff = soundPicker.SelectedIndex;
            {
                player.Play();
                player.Loop = true;
            }
            if (soundOnOff == 1)
            {
                var memStream = new MemoryStream();
                player.Stop(); 
                
            }
        }


        private void SetDefaultSettings()
        {
            var soundMode = soundPicker.SelectedIndex;

        }

        private void AccountBtn_Clicked(object sender, EventArgs e)
        {
            //open the new page
            Navigation.PushAsync(new Account());
        }

        private void FeedbackBtn_Clicked(object sender, EventArgs e)
        {
            //open the new page
            Navigation.PushAsync(new Feedback());
        }

        private void StartBtn_Clicked(object sender, EventArgs e)
        {
            //open the new page
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


        private void ExitBtn_Clicked(object sender, EventArgs e)
        {
            System.Environment.Exit(1);
        }


    }
}
