using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xamarin.Forms;
using Madiba.Models;
using System.Reflection;    // To read embedded ressources
using System.Xml;   // To read xml files

namespace Madiba {

    public partial class QuestionnairesPage : ContentPage {

        static int NUMBER_OF_QUIZZES = 2;

        public QuestionnairesPage() {
            InitializeComponent();
        }

        protected override void OnAppearing() {
            base.OnAppearing();

            var questionnaires = new List<Questionnaire>();

            //var files = Directory.EnumerateFiles(App.FolderPath, "*.questionnaires.txt");
            var files = Directory.EnumerateFiles(App.FolderPath, "*.xml");

            // If enumerate files is empty, copy embedded ressources to local app folder
            var numberOfFiles = files.Count();
            //if (numberOfFiles != NUMBER_OF_QUIZZES) {
            if (true) {
                // Empty the local data directory
                System.IO.DirectoryInfo di = new DirectoryInfo(App.FolderPath);
                foreach (FileInfo file in di.GetFiles()) {
                    file.Delete();
                }

                var assembly = IntrospectionExtensions.GetTypeInfo(typeof(MainPage)).Assembly;
                List<string> quizzesNames = new List<string>(assembly.GetManifestResourceNames());
                int numberOfQuizzes = quizzesNames.Count();
                for (int i = 0; i < numberOfQuizzes; i++) {
                    Stream stream = assembly.GetManifestResourceStream(quizzesNames[i]);

                    string text = "";
                    using (var reader = new System.IO.StreamReader(stream)) {
                        text = reader.ReadToEnd();

                    var quizName = Path.Combine(App.FolderPath, "_"+ i.ToString()+ ".xml");
                    File.WriteAllText(quizName, text);
                    }
                }

                files = Directory.EnumerateFiles(App.FolderPath, "*.xml");
            }

            foreach (var filename in files) {
                // Get the title of the quizz
                XmlTextReader reader = new XmlTextReader(filename);
                // Read quiz title
                while (reader.Read()) {
                    if (reader.NodeType == XmlNodeType.Element) {
                        if (reader.Name == "title")
                            break;
                    }
                }
                reader.Read();
                var quizTitle = reader.Value;
                // Read last played date
                while (reader.Read()) {
                    if (reader.NodeType == XmlNodeType.Element) {
                        if (reader.Name == "date")
                            break;
                    }
                }
                reader.Read();
                var quizDate = reader.Value;

                questionnaires.Add(new Questionnaire {
                    Filename = filename,
                    //Text = File.ReadAllText(filename),
                    Text = quizTitle,
                    //Text = quizDate,
                    //Date = File.GetCreationTime(filename)
                    //Date = DateTime.Parse(quizDate)
                    Date = Convert.ToDateTime(quizDate)
                });
            }

            listView.ItemsSource = questionnaires
                .OrderBy(d => d.Date)
                .ToList();
        }

        async void OnQuestionnaireAddedClicked(object sender, EventArgs e) {
            await Navigation.PushAsync(new QuestionnaireEntryPage {
                BindingContext = new Questionnaire()
            });
        }

        async void OnListViewItemSelected(object sender, SelectedItemChangedEventArgs e) {
            if (e.SelectedItem != null) {
                await Navigation.PushAsync(new QuestionnaireEntryPage {
                    BindingContext = e.SelectedItem as Questionnaire
                });
            }
        }
    }
}