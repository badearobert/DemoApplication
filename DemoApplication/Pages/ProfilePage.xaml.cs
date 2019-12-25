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
        private static Account currentAccount;
        public ProfilePage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            accountImage.IsEnabled = false;
            AnimateProfilePicture();
            LoadProfile();

            HandleLabelsVisibility();
        }
        private async void AnimateProfilePicture()
        {
            this.InnerStack.IsVisible = false;
            this.OuterStack.Opacity = 0;

            await Animate.FadeIn(this.OuterStack, 1000, Easing.CubicOut);
            await Animate.BallAnimate(this.OuterStack, 50, 10, 2);
            this.InnerStack.IsVisible = true;
        }
        private void HandleLabelsVisibility()
        {
            // cannot change account if no accounts available
            bool AccountsAvailable = (App.database.GetAccounts().Count > 0);
            bool AccountNotNull = (currentAccount != null);
            Label_ChangeAccount.IsVisible = AccountsAvailable;
            Label_EditAccount.IsVisible = AccountsAvailable && AccountNotNull;
        }
        private void LoadProfile()
        {
            if (currentAccount != null)
            {
                LoadProfile(currentAccount.Id);
            }
            else
            {
                LoadProfileUnknown();
            }
        }
        private void LoadProfile(int profile_key)
        {
            currentAccount = App.database.GetAccount(profile_key);

            if (currentAccount != null)
            {
                accountImage.Source = currentAccount.ProfilePicturePath;
                accountName.Text = "Welcome back, " + currentAccount.Username + "!"; ;
                accountImage.IsEnabled = true;
            }
        }
        private void LoadProfileUnknown()
        {
            accountImage.Source = ImageSource.FromFile(Account.PathDefaultPicture);
            currentAccount = null;
            accountImage.IsEnabled = false;
            accountName.Text = "Hello, stranger. Who are you?";
        }

        private async void ChangeAccount_Tapped(object sender, EventArgs e)
        {
            ChangeAccountPage changeAccountPage = new ChangeAccountPage();
            changeAccountPage.OnAccountChanged += OnAccountChange;
            changeAccountPage.OnAccountRemoved += OnAccountRemoved;

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
                // Should not be null, button should have be hidden in this case
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

        private void OnAccountChange(object sender, AccountDataEventArgs e)
        {
            if (currentAccount == null || currentAccount.Id != e.AccountIndex)
            {
                LoadProfile(e.AccountIndex);
            }
        }
        private void OnAccountRemoved(object sender, AccountDataEventArgs e)
        {
            if (currentAccount != null && currentAccount.Id == e.AccountIndex) 
            {
                LoadProfileUnknown();
            }
        }

        private async void EditAccount_Tapped(object sender, EventArgs e)
        {
            if (currentAccount == null) return;
            EditAccountPage editAccountPage = new EditAccountPage(currentAccount.Id);
            await Navigation.PushAsync(editAccountPage);
        }
    }
}