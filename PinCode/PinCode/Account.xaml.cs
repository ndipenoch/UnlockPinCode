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
using System.Diagnostics;
using System.Net;

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

        public  Account()
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
            if (CheckInternetConnection() == false)
            {
                await DisplayAlert("No Internet Connection!", "No Internet Connection!", "OK");
                Navigation.PushAsync(new MainPage());
            }
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
                await DisplayAlert("Incoorect Username", "Incorrect Username!", "OK","Cancel");
            }

            if (FoundpWord == true)
            {
                App.IsLogin = true;
                App.CurrentUser = username.Text;
                Navigation.PushAsync(new MyAccount(username.Text));
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
            if (CheckInternetConnection() == false)
            {
                await DisplayAlert("No Internet Connection!", "No Internet Connection!", "OK");
                Navigation.PushAsync(new MainPage());
            
            }

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
                FirebaseDAO fb = new FirebaseDAO();
                //Create User account with default values
                fb.AddUserDetailsFB(username.Text, "FirstName", "Surname", "Email", "Tellphone", "Street", "Town", "Country", 0, 0);
                App.CurrentUser = username.Text;
                App.IsLogin = true;
                //Go to user's page
                Navigation.PushAsync(new MyAccount(username.Text));
            }

            username.Text = "";
            password.Text = "";

        }

        //check the internet connection.
        public bool CheckInternetConnection()
        {
            string CheckUrl = "https://unlockpincode-d448d.firebaseio.com/";

            try
            {
                HttpWebRequest iNetRequest = (HttpWebRequest)WebRequest.Create(CheckUrl);
                iNetRequest.Timeout = 5000;
                WebResponse iNetResponse = iNetRequest.GetResponse();
                iNetResponse.Close();
                return true;
            }
            catch (WebException ex)
            {
                return false;
            }
        }

    }

 
}