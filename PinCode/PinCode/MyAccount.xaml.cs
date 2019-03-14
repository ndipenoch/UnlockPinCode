using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PinCode
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MyAccount : ContentPage
	{
		public  MyAccount (string uName)
		{
			InitializeComponent ();


        usernamer.Text =  uName;

            if (fName.Text == null)
            {
                fName.Text = "First name";
            }
            if (sName.Text == null)
            {
                sName.Text = "Surname";
            }
            if (lEmail.Text == null)
            {
                lEmail.Text = "Email";
            }
            if (lTel.Text == null)
            {
                lTel.Text = "Telephone Number";
            }
            if (lStreet.Text == null)
            {
                lStreet.Text = "Street";
            }
            if (lTown.Text == null)
            {
                lTown.Text = "Town";
            }
            if (lCountry.Text == null)
            {
                lCountry.Text = "Country";
            }

            switch (Device.RuntimePlatform)
            {
                case Device.Android:
                    ReadFbAdroid();
                    break;
                case Device.UWP:
                    DAO d = new DAO();
                    try
                    {
                        UserDetails ud = d.GetUserByUserName(uName);
                        fName.Text = ud.firstname;
                        sName.Text = ud.surname;
                        lEmail.Text = ud.email;
                        lTel.Text = ud.telephone;
                        lStreet.Text = ud.street;
                        lTown.Text = ud.town;
                        lCountry.Text = ud.country;
                        Rscores.Text = ud.bestRollutteScore.ToString();
                        Sscores.Text = ud.bestSquareScore.ToString();
                    }
                    catch
                    {
                        System.Diagnostics.Debug.WriteLine("User not found");
                    }

                    break;
                default:
                    break;
            }

            MyAccountSettings();

            
            async void ReadFbAdroid()
            {
                FirebaseDAO fb = new FirebaseDAO();
                string result = await fb.ReadDataFbAndroid(uName);
                if (result != "")
                {
                    string[] splitString = result.Split('*');
                    fName.Text = splitString[0];
                    sName.Text = splitString[1];
                    lEmail.Text = splitString[2];
                    lTel.Text = splitString[3];
                    lStreet.Text = splitString[4];
                    lTown.Text = splitString[5];
                    lCountry.Text = splitString[6];
                    Rscores.Text = splitString[7];
                    Sscores.Text = splitString[8];
                }
            }

        }

       
        private void MyAccountSettings()
        {

        }

        private void HomeBtn_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new MainPage());
        }

        private void EditBtn_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new EditMyAccount(usernamer.Text));
        }

        private void DeleteBtn_Clicked(object sender, EventArgs e)
        {
            switch (Device.RuntimePlatform)
            {
                case Device.Android:

                    break;
                case Device.UWP:
                    DAO d = new DAO();
                    d.DeleteUserAccount(usernamer.Text);
                    break;
                default:
                    break;
            }

            Navigation.PushAsync(new MainPage());
        }

        private void SignOutBtn_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Account());
        }
    }
}