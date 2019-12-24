using DemoApplication.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DemoApplication.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfilePage : ContentPage
    {
        private Account currentAccount = null;
        public ProfilePage()
        {
            InitializeComponent();
            
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            var accounts = App.database.GetAccounts();
            if (accounts.Count != 0)
            {
                LoadProfile(accounts[0].Id);
            }
        }
        private void LoadProfile(int profile_key)
        {
            currentAccount = App.database.GetAccount(profile_key);

            if (currentAccount != null)
            {
                accountImage.Source = currentAccount.ProfilePicturePath;
                accountName.Text = currentAccount.Username;
            }
        }
        private async void ChangeAccount_Tapped(object sender, EventArgs e)
        {
            ChangeAccountPage changeAccountPage = new ChangeAccountPage();
            changeAccountPage.OnAccountChanged += OnAccountChange;
            await Navigation.PushAsync(changeAccountPage);
        }

        private async void CreateAccount_Tapped(object sender, EventArgs e)
        {
            RegisterPage registerPage = new RegisterPage();
            registerPage.OnAccountChanged += OnAccountChange;
            await Navigation.PushAsync(registerPage);
        }

        private void ChangeProfilePicture_Tapped(object sender, EventArgs e)
        {
            if (currentAccount == null)
            {
                // TODO
                return;
            }
            // TODO - temp
            string new_path = "profile_image_1.jpg";
            if ((accountImage.Source as FileImageSource).File == "profile_image_1.jpg")
            {
                new_path = "profile_image_2.jpg";
            }
            accountImage.Source = ImageSource.FromFile(new_path);

            // update DB
            currentAccount.ProfilePicturePath = new_path;
            App.database.UpdateAccount(currentAccount);
        }

        private void OnAccountChange(object sender, AccountChangeEventArgs e)
        {
            if (currentAccount == null || currentAccount.Id != e.AccountIndex)
            {
                LoadProfile(e.AccountIndex);
            }
        }    
    }
}