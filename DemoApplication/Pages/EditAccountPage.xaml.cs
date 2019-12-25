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
    public partial class EditAccountPage : ContentPage
    {
        private int accountIndex;
        private Account account;

        public EditAccountPage(int index)
        {
            InitializeComponent();
            accountIndex = index;
            UpdateFieldsFromAccount();
        }
        private void UpdateFieldsFromAccount()
        {
            account = App.database.GetAccount(accountIndex);
            if (account != null)
            {
                accountName.Text = account.Username;
                accountPassword.Text = account.Password;
                accountDetail.Text = account.Detail;
            }
        }
        private void UpdateAccountFromFields()
        {
            if (account != null)
            {
                account.Username = accountName.Text;
                account.Password = accountPassword.Text;
                account.Detail = accountDetail.Text;
            }
        }
        private void ButtonUpdateAccount_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(accountName.Text) || string.IsNullOrEmpty(accountPassword.Text))
            {
                DisplayAlert("Failure", "Please insert data into the fields", "Ok!");
                return;
            }

            buttonSubmit.BackgroundColor = Color.FromRgba(52, 152, 219, 255);
            UpdateAccountFromFields();

            if (App.database.UpdateAccount(account))
            {
                DisplayAlert("Success", "Account successfully updated", "Great!");
            }
            else
            {
                DisplayAlert("Failure", "Unable to updated account", "Ok!");
            }

            Application.Current.MainPage.Navigation.PopAsync();
        }
    }
}