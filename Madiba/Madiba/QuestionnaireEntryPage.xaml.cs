using System;
using System.IO;
using Xamarin.Forms;
using Madiba.Models;


namespace Madiba {
    public partial class QuestionnaireEntryPage : ContentPage {
        public QuestionnaireEntryPage() {
            InitializeComponent();
        }

        async void OnSaveButtonClicked(object sender, EventArgs e) {
            var questionnaire = (Questionnaire)BindingContext;

            if (string.IsNullOrWhiteSpace(questionnaire.Filename)) {
                // Save
                var filename = Path.Combine(App.FolderPath, $"{Path.GetRandomFileName()}.questionnaires.txt");
                File.WriteAllText(filename, questionnaire.Text);
            } else {
                // Update
                File.WriteAllText(questionnaire.Filename, questionnaire.Text);
            }

            await Navigation.PopAsync();
        }

        async void OnDeleteButtonClicked(object sender, EventArgs e) {
            var questionnaire = (Questionnaire)BindingContext;

            if (File.Exists(questionnaire.Filename)) {
                File.Delete(questionnaire.Filename);
            }

            await Navigation.PopAsync();
        }
    }
}