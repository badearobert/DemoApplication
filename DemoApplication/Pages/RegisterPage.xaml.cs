using DemoApplication.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DemoApplication
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterPage : ContentPage
    {
        public delegate void AccountChangedDelegate(object sender, AccountDataEventArgs e);
        public event AccountChangedDelegate OnAccountChanged;
        public RegisterPage()
        {
            InitializeComponent();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(accountName.Text) || string.IsNullOrEmpty(accountPassword.Text))
            {
                DisplayAlert("Failure", "Please insert data into the fields", "Ok!");
                return;
            }

            buttonSubmit.BackgroundColor = Color.FromRgba(52, 152, 219, 255);
            Account account = new Account()
            {
                Username = accountName.Text,
                Password = accountPassword.Text,
                Detail = accountDetail.Text
            };
            if (App.database.Add(ref account))
            {
                DisplayAlert("Success", "Account successfully added", "Great!");
                OnAccountChanged?.Invoke(this, new AccountDataEventArgs(account.Id));

            }
            else
            {
                DisplayAlert("Failure", "Unable to add account", "Ok!");
            }
                
            Application.Current.MainPage.Navigation.PopAsync();
        }
    }
}