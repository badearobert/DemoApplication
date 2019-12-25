using DemoApplication.Events;
using DemoApplication.Pages;
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
    public partial class ChangeAccountPage : ContentPage
    {
        public delegate void AccountChangedDelegate(object sender, AccountDataEventArgs e);
        public delegate void AccountRemovedDelegate(object sender, AccountDataEventArgs e);

        public event AccountChangedDelegate OnAccountChanged;
        public event AccountRemovedDelegate OnAccountRemoved;
        public ChangeAccountPage()
        {
            InitializeComponent();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            UpdateList();
        }

        private void UpdateList()
        {
            AccountListView.ItemsSource = App.database.GetAccounts();
        }
        private void AccountListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            OnAccountChanged?.Invoke(this, new AccountDataEventArgs((e.Item as Account).Id));
            Application.Current.MainPage.Navigation.PopAsync();
        }

        private void ButtonDelete_Clicked(object sender, EventArgs e)
        {
            Button listView = (sender as Button);
            var param = listView.CommandParameter;
            int index = int.Parse(param.ToString());

            bool deleted = App.database.Delete(index);
            if (deleted)
            {
                UpdateList();
                OnAccountRemoved?.Invoke(this, new AccountDataEventArgs(index));
            }
        }

        private async void ButtonEdit_Clicked(object sender, EventArgs e)
        {
            Button listView = (sender as Button);
            var param = listView.CommandParameter;
            int index = int.Parse(param.ToString());

            EditAccountPage editAccountPage = new EditAccountPage(index);
            await Navigation.PushAsync(editAccountPage);
        }
    }

}