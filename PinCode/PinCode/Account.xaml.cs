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
using Plugin.Connectivity;

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
        private bool InternetConSatete;

        public  Account()
        {
            InitializeComponent();
            InternetConSatete = DoIHaveInternet();
            BackgroundImage = "Assets/background.png";


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
   
             if (InternetConSatete == false)
             {
               await DisplayAlert("No Internet Connection!", "No Internet Connection!", "OK");
               Navigation.PushAsync(new MainPage());
             }
            else
            {
                if ((username.Text != null) && (username.Text.Length != 0) && (password.Text != null) && (password.Text.Length != 0))
                {
                    foundUserName = false;
                    FoundpWord = false;
                    //check if the username is correct
                    var results = await firebase.Child(node).OnceAsync<Data>();
                    foreach (var uName in results)
                    {
                        if (username.Text == uName.Object.username)
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
                        await DisplayAlert("Incoorect Username", "Incorrect Username!", "OK", "Cancel");
                        Navigation.PushAsync(new Account());
                    }

                    if (FoundpWord == true)
                    {
                        App.IsLogin = true;
                        App.CurrentUser = username.Text;
                        Navigation.PushAsync(new MyAccount(username.Text));
                    }
                    if (foundUserName == true && FoundpWord == false)
                    {
                        await DisplayAlert("Incorrect Password!", "Incorrect Password!", "OK");
                        Navigation.PushAsync(new Account());
                    }

                    //Clear the entry field
                    password.Text = "";

                }
                else
                {
                    await DisplayAlert("Warning!", "username and password fields must be filled!", "OK");
                    Navigation.PushAsync(new Account());
                }
            }
        }

        //insert user's details to Firebase
        private  async void SignUp_Clicked(object sender, EventArgs e)
        {
            if (InternetConSatete == false)
            {
                await DisplayAlert("No Internet Connection!", "No Internet Connection!", "OK");
                Navigation.PushAsync(new MainPage());
            }
            else
            {
                if ((username.Text != null) && (username.Text.Length != 0) && (password.Text != null) && (password.Text.Length != 0))
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
                else
                {
                    await DisplayAlert("Warning!", "username and password fields must be filled!", "OK");
                    Navigation.PushAsync(new Account());
                }
                  
            }

        }

        //Go to home page.
        private void HomeBtn_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new MainPage());
        }

        //check internet connection using a plugin
        public bool DoIHaveInternet()
        {
            return CrossConnectivity.Current.IsConnected;
        }

        //check the internet connection without plugin.
        /* public bool CheckInternetConnection()
         {
             string CheckUrl = "https://www.google.com/";

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
         }*/

    }

}