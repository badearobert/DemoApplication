using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DemoApplication
{
    public partial class App : Application
    {
        public static DatabaseAccess database { get; private set; }

        
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new MainPage());
        }

        public App(string dbpath_accounts)
        {
            InitializeComponent();
            database = new DatabaseAccess();
            database.PATH_ACCOUNTS = dbpath_accounts;
            //App.database.DeleteAll();

            MainPage = new NavigationPage(new MainPage());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
