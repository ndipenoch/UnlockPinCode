using Firebase.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using Firebase.Database.Query;
using Microsoft.Data.Sqlite;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PinCode
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Feedback : ContentPage
	{
        /// <summary>
        /// Firebase authetication parameters
        /// </summary>
        private const String databaseUrl = "https://unlockpincode-d448d.firebaseio.com/";
        private const String databaseSecret = "gOjxFlBGP1v8vufauNkO9VqnH5PiEwltVATNdUey";
        private FirebaseClient firebase;


        /// <summary>
        /// Establish connection with Firebase
        /// </summary>
        public void ConnectToFirebase()
        {
            this.firebase = new FirebaseClient(

                 databaseUrl,
                 new FirebaseOptions
                 {
                     AuthTokenAsyncFactory = () => Task.FromResult(databaseSecret)
                 });
        }


        public Feedback ()
		{
			InitializeComponent ();
            BackgroundImage = "Assets/background.png";
        }

        private void HomeBtn_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new MainPage());
        }

        private async void CommentAlert()
        {
                await DisplayAlert("Warning!", "Name and Comment must be Filed!", "OK");
        }

        private async void SucessFullySentAllert()
        {
            await DisplayAlert("Send!", "Your Message was Succesfully Sent!", "OK");
        }

        private async void FirebaseConAllert()
        {
            await DisplayAlert("Warning!", "You have no internet Connection!", "OK");
            Navigation.PushAsync(new MainPage());
        }

        private void BtnSend_Clicked(object sender, EventArgs e)
        {

            if ((Ecomments.Text != null) && (Ecomments.Text.Length != 0) && (EditName.Text != null) && (EditName.Text.Length != 0))
            {
                try
                {
                    ConnectToFirebase();
                    SendComment();
                    RefreshView();
                    SucessFullySentAllert();
                }
                catch
                {
                    FirebaseConAllert();
                }


            }
            else
            {
                CommentAlert();
            }
             
        }

        /// <summary>
        /// Refresh view
        /// </summary>
        public void RefreshView()
        {
            EditName.Text = "";
            EditEmail.Text = "";
            EditTel.Text = "";
            Ecomments.Text = "";
        }
        /// <summary>
        /// Write comments to firebase.
        /// </summary>
        public async void SendComment()
        {
            ConnectToFirebase();

            String node = "Comments" + "/";

            Comments comments = new Comments
            {
                name = EditName.Text,
                email = EditEmail.Text,
                telephone = EditTel.Text,
                comments = Ecomments.Text,

            };

            await firebase.Child(node).PostAsync<Comments>(comments);
        }



    }
}