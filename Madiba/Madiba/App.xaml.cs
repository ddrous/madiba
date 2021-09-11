using System;
using System.IO;
using Xamarin.Forms;

namespace Madiba {
    public partial class App : Application {
        public static string FolderPath { get; private set; }

        public App() {
            InitializeComponent();
            FolderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData));
            MainPage = new NavigationPage(new QuestionnairesPage());
            //MainPage = new NavigationPage(new MainPage());
        }
        // ...

        protected override void OnStart() {
        }

        protected override void OnSleep() {
        }

        protected override void OnResume() {
        }
    }
}
