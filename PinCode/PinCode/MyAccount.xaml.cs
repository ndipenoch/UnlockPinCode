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
		public MyAccount (string uName)
		{
			InitializeComponent ();
            usernamer.Text = "Welcome " + uName+"!";
            MyAccountSettings();

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
            Navigation.PushAsync(new EditMyAccount());
        }

        private void DeleteBtn_Clicked(object sender, EventArgs e)
        {

        }

        private void SignOutBtn_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Account());
        }
    }
}