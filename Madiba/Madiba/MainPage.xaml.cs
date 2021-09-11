using System;
using System.Reflection;
using System.IO;
using Xamarin.Forms;
//using Madiba.Data;

namespace Madiba {

    //public partial class MainPage : ContentPage {
    //    string _fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Madiba.txt");



    //    public MainPage() {
    //        InitializeComponent();

    //        if (File.Exists(_fileName)) {
    //            editor.Text = File.ReadAllText(_fileName);
    //        }
    //    }

    //    void OnSaveButtonClicked(object sender, EventArgs e) {
    //        File.WriteAllText(_fileName, editor.Text);
    //    }

    //    void OnDeleteButtonClicked(object sender, EventArgs e) {
    //        if (File.Exists(_fileName)) {
    //            File.Delete(_fileName);
    //        }
    //        editor.Text = string.Empty;
    //    }
    //}

    public partial class MainPage : ContentPage {
        string _fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Madiba.txt");



        public MainPage() {
            InitializeComponent();

            // How to inlcude embedded ressources in my application
            var assembly = IntrospectionExtensions.GetTypeInfo(typeof(MainPage)).Assembly;
            //Stream stream = assembly.GetManifestResourceStream("Madiba.Madiba.txt");
            Stream stream = assembly.GetManifestResourceStream("Madiba.Questionnaire_1.xml");

            string text = "";
            using (var reader = new System.IO.StreamReader(stream)) {
                text = reader.ReadToEnd();
            }

            if (File.Exists(_fileName)) {
                //editor.Text = File.ReadAllText(_fileName);
                editor.Text = text;
            }
        }

        void OnSaveButtonClicked(object sender, EventArgs e) {
            File.WriteAllText(_fileName, editor.Text);
        }

        void OnDeleteButtonClicked(object sender, EventArgs e) {
            if (File.Exists(_fileName)) {
                File.Delete(_fileName);
            }
            editor.Text = string.Empty;
        }
    }
}