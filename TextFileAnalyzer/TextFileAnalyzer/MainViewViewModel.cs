using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
using TextFileAnalyzer.Annotations;

namespace TextFileAnalyzer
{
    public class MainViewViewModel : INotifyPropertyChanged
    {
        private string _pathToSourceDirectory;
        private string _longestFileName;
        private string _shortestFileName;
        private int _numberOfWords;
        private int _numberOfSentences;
        private ObservableCollection<TextFile> _textFiles = new ObservableCollection<TextFile>();

        public SelectSourceDirectoryCommand SelectSourceDirectory => new SelectSourceDirectoryCommand(this);

        public string PathToSourceDirectory
        {
            get => _pathToSourceDirectory;
            set
            {
                _pathToSourceDirectory = value;
                OnPropertyChanged(nameof(PathToSourceDirectory));
            }
        }

        public ObservableCollection<TextFile> TextFiles
        {
            get => _textFiles;
            set
            {
                _textFiles = value;
                OnPropertyChanged(nameof(TextFiles));
            }
        }

        public int NumberOfWords
        {
            get => _numberOfWords;
            set
            {
                _numberOfWords = value;
                OnPropertyChanged(nameof(NumberOfWords));
            }
        }

        public int NumberOfSentences
        {
            get => _numberOfSentences;
            set
            {
                _numberOfSentences = value;
                OnPropertyChanged(nameof(NumberOfSentences));
            }
        }

        public string LongestFileName
        {
            get => _longestFileName;
            set
            {
                _longestFileName = value;
                OnPropertyChanged(nameof(LongestFileName));
            }
        }

        public string ShortestFileName
        {
            get => _shortestFileName;
            set
            {
                _shortestFileName = value;
                OnPropertyChanged(nameof(ShortestFileName));
            }
        }

        public void LoadTextFiles()
        {
            var path = PathToSourceDirectory;
            var fileNames = Directory.GetFiles(path);

            foreach (var file in fileNames)
            {
                var filename = file.Split('\\').Last();

                if (filename.Split('.').Last() == "txt")
                {
                    var textFile = new TextFile(filename);

                    using (var reader = new StreamReader(file))
                    {
                        textFile.Text = reader.ReadToEnd();
                    }

                    textFile.GetNumberOfWords();
                    textFile.GetNumberOfSentences();
                    textFile.GetMostOccurredWord();
                    textFile.GetLongestAndShortestWord();
                    textFile.GetLongestAndShortestSentence();

                    NumberOfWords += textFile.NumberOfWords;
                    NumberOfSentences += textFile.NumberOfSentences;
                    
                    TextFiles.Add(textFile);
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class SelectSourceDirectoryCommand : ICommand
    {
        private readonly MainViewViewModel _viewModel;

        public SelectSourceDirectoryCommand(MainViewViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            var dialog = new FolderBrowserDialog();
            dialog.ShowDialog();
            _viewModel.PathToSourceDirectory = dialog.SelectedPath;
            _viewModel.LoadTextFiles();
        }

        public event EventHandler CanExecuteChanged;
    }
}
