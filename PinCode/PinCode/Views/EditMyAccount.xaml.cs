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
	public partial class EditMyAccount : ContentPage
	{
		public  EditMyAccount (string uName)
		{
			InitializeComponent ();
            BackgroundImage = "Assets/background.png";
            EditUsernamer.Text = uName;
            App.CurrentUser= uName;
            DAO d = new DAO();
            FirebaseDAO fb = new FirebaseDAO();

            switch (Device.RuntimePlatform)
            {
                case Device.Android:
                    ReadFbAdroid();
                    break;
                case Device.UWP:
                    fb.ReadDataFbUWP( uName);
                    //Read from local table/SQLite and display to the user
                    try
                    {
                        UserDetails ud = d.GetUserByUserName(uName);
                        EditName.Text = ud.firstname;
                        EditSurname.Text = ud.surname;
                        EditEmail.Text = ud.email;
                        EditTel.Text = ud.telephone;
                        EditStreet.Text = ud.street;
                        EditTown.Text = ud.town;
                        EditCountry.Text = ud.country;
                        EditSscores.Text = ud.bestSquareScore.ToString();
                        EditRScores.Text = ud.bestRollutteScore.ToString();
                    }
                    catch
                    {
                        System.Diagnostics.Debug.WriteLine("User not found");
                    }

                    break;
                default:
                    break;
            }

        }

        /// <summary>
        /// Read user details from Firebase and display it to user
        /// </summary>
        async void ReadFbAdroid()
        {
            FirebaseDAO fb = new FirebaseDAO();
            string result = await fb.ReadDataFbAndroid(EditUsernamer.Text);
            if (result != "")
            {
                string[] splitString = result.Split('*');
                EditName.Text = splitString[0];
                EditSurname.Text = splitString[1];
                EditEmail.Text = splitString[2];
                EditTel.Text = splitString[3];
                EditStreet.Text = splitString[4];
                EditTown.Text = splitString[5];
                EditCountry.Text = splitString[6];
                EditSscores.Text = splitString[7];
                EditRScores.Text = splitString[8];
            }
        }

        /// <summary>
        /// Update and save information
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveBtn_Clicked(object sender, EventArgs e)
        {
            FirebaseDAO fb = new FirebaseDAO();
            DAO d = new DAO();
            App.EdittedAcount = true;

            float s = (float)Convert.ToDouble(EditSscores.Text);
            float r = (float)Convert.ToDouble(EditRScores.Text);

            //Display default info if fields are empty.
            if (EditName.Text == null)
            {
                EditName.Text = "First name";
            }
            if (EditSurname.Text == null)
            {
                EditSurname.Text = "Surname";
            }
            if (EditEmail.Text == null)
            {
                EditEmail.Text = "Email";
            }
            if (EditTel.Text == null)
            {
                EditTel.Text = "phone Number";
            }
            if (EditStreet.Text == null)
            {
                EditStreet.Text = "Street";
            }
            if (EditTown.Text == null)
            {
                EditTown.Text = "Town";
            }
            if (EditCountry.Text == null)
            {
                EditCountry.Text = "Country";
            }

            switch (Device.RuntimePlatform)
            {
                case Device.Android:
                    fb.UpdateUserDetailsFB(EditUsernamer.Text, EditName.Text, EditSurname.Text, EditEmail.Text, EditTel.Text, EditStreet.Text, EditTown.Text, EditCountry.Text, s, r);
                    break;
                case Device.UWP:
                    //Update SQLite if it is already in the database, existing user
                    int cnt = d.GetUserByUserName1(EditUsernamer.Text);
                    if (cnt>0)
                     {
                        d.UpdateUserDetails(EditUsernamer.Text, EditName.Text, EditSurname.Text, EditEmail.Text, EditTel.Text, EditStreet.Text, EditTown.Text, EditCountry.Text, s, r);
                        fb.UpdateUserDetailsFB(EditUsernamer.Text, EditName.Text, EditSurname.Text, EditEmail.Text, EditTel.Text, EditStreet.Text, EditTown.Text, EditCountry.Text, s, r);
                    }
                     else
                     {
                        //Add new info to SQLite DB if it is a first timer or new user.
                        d.AddUserDetails(EditUsernamer.Text, EditName.Text, EditSurname.Text, EditEmail.Text, EditTel.Text, EditStreet.Text, EditTown.Text, EditCountry.Text, s, r);
                        fb.AddUserDetailsFB(EditUsernamer.Text, EditName.Text, EditSurname.Text, EditEmail.Text, EditTel.Text, EditStreet.Text, EditTown.Text, EditCountry.Text, s, r);
                    }
                    break;
                default:
                    break;
            }
         
           Navigation.PushAsync(new MyAccount(EditUsernamer.Text));
        }


        private void HomeBtn_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new MainPage());
        }
    }
}