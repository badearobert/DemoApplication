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
    public partial class ChangeAccountPage : ContentPage
    {
        public delegate void AccountChangedDelegate(object sender, AccountChangeEventArgs e);
        public event AccountChangedDelegate OnAccountChanged;
        public ChangeAccountPage()
        {
            InitializeComponent();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            AccountListView.ItemsSource = App.database.GetAccounts();
        }

        private void AccountListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            OnAccountChanged?.Invoke(this, new AccountChangeEventArgs((e.Item as Account).Id));
            Application.Current.MainPage.Navigation.PopAsync();
        }
    }

}