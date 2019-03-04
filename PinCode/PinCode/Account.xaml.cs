using Plugin.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Firebase.Database;
using Firebase.Database.Query;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Android.Widget;

namespace PinCode
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Account : ContentPage
	{

        private const String databaseUrl = "https://unlockpincode-d448d.firebaseio.com/";
        private const String databaseSecret = "gOjxFlBGP1v8vufauNkO9VqnH5PiEwltVATNdUey";
        private const String node = "details/";
        private FirebaseClient firebase;
        private bool foundUserName = false;
        private bool FoundpWord = false;
        private bool FoundUname = false;

        public Account()
        {
            InitializeComponent();

            this.firebase = new FirebaseClient(

                databaseUrl,
                new FirebaseOptions
                {
                    AuthTokenAsyncFactory = () => Task.FromResult(databaseSecret)
                });
        }

        //Retrieve userdetails from Firebase
        private  async void SignIn_Clicked(object sender, EventArgs e)
        {

            foundUserName = false;
            FoundpWord =false;
            //check if the username is correct
            var results = await firebase.Child(node).OnceAsync<Data>();
            foreach (var uName in results)
            {
                if(username.Text== uName.Object.username)
                {
                    foundUserName = true;
                    break;
                }
            }

               //if username is correct check if the password is correct
                if (foundUserName == true)
               {
                foreach (var pword in results)
                {
                    if (password.Text == pword.Object.password)
                    {
                        FoundpWord = true;
                        break;
                    }
                }
            }
            else
            {
                await DisplayAlert("Incoorect Username", "Incoorect Username!", "OK");
            }

            if (FoundpWord == true)
            {
                System.Diagnostics.Debug.WriteLine("Your are login");
            }
            if (foundUserName==true && FoundpWord == false)
            {
                await DisplayAlert("Incorrect Password!", "Incorrect Password!", "OK");
            }

            //Clear the entry field
            password.Text = "";

        }

        //insert user's details to Firebase
        private  async void SignUp_Clicked(object sender, EventArgs e)
        {
            FoundUname = false;
            //check if the username is already used
            var results = await firebase.Child(node).OnceAsync<Data>();
            foreach (var uName in results)
            {
                if (username.Text == uName.Object.username)
                {
                    FoundUname = true;
                    break;
                }
            }

            //If username is not used create an account
            if (FoundUname == true)
            {
                await DisplayAlert("Username is already used!", "Username is already used!", "OK");
            }
            else
            {
                Data data = new Data
                {
                    username = username.Text,
                    password = password.Text
                };

                await firebase.Child(node).PostAsync<Data>(data);
            }

            username.Text = "";
            password.Text = "";

        }

                private async void Delete_Clicked(object sender, EventArgs e)
               {
                 //FirebaseResponse response = await client.DeleteTaskAsync("information/");
                // await firebase.Child(node).DeleteAsync();

               }
    }

 
}