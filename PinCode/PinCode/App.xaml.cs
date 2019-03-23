using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;


[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace PinCode
{
    public partial class App : Application
    {
        public static float SquareBestScores;
        public static float RoulleteBestScores;
        public static bool IsLogin = false;
        public static string CurrentUser;
        public static bool EdittedAcount=false;

        public static String MainUsername;
        public static String MainFirstname;
        public static String MainSurname;
        public static String MainEmail;
        public static String MainTelephone;
        public static string MainStreet;
        public static String MainTown;
        public static String MainCountry;
    

        public App()
        {
            InitializeComponent();

            //MainPage = new MainPage();
            MainPage = new NavigationPage(new MainPage());
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }


}
