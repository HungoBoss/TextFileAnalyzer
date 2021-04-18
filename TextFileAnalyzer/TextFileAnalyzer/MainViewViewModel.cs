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
        #region Properties
        private string _pathToSourceDirectory;
        private string _longestFile;
        private string _shortestFile;
        private string _mostOccurredWord;
        private string _longestWord;
        private string _shortestWord;
        private string _longestSentence;
        private string _shortestSentence;
        private int _numberOfWords;
        private int _numberOfSentences;
        private ObservableCollection<TextFile> _textFiles = new ObservableCollection<TextFile>();
        #endregion

        #region Commands
        public SelectSourceDirectoryCommand SelectSourceDirectory => new SelectSourceDirectoryCommand(this);
        #endregion

        #region Setters and Getters
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

        public string LongestFile
        {
            get => _longestFile;
            set
            {
                _longestFile = value;
                OnPropertyChanged(nameof(LongestFile));
            }
        }

        public string ShortestFile
        {
            get => _shortestFile;
            set
            {
                _shortestFile = value;
                OnPropertyChanged(nameof(ShortestFile));
            }
        }

        public string MostOccurredWord
        {
            get => _mostOccurredWord;
            set
            {
                _mostOccurredWord = value;
                OnPropertyChanged(nameof(MostOccurredWord));
            }
        }

        public string LongestWord
        {
            get => _longestWord;
            set
            {
                _longestWord = value;
                OnPropertyChanged(nameof(LongestWord));
            }
        }

        public string ShortestWord
        {
            get => _shortestWord;
            set
            {
                _shortestWord = value;
                OnPropertyChanged(nameof(ShortestWord));
            }
        }

        public string LongestSentence
        {
            get => _longestSentence;
            set
            {
                _longestSentence = value;
                OnPropertyChanged(nameof(LongestSentence));
            }
        }

        public string ShortestSentence
        {
            get => _shortestSentence;
            set
            {
                _shortestSentence = value;
                OnPropertyChanged(nameof(ShortestSentence));
            }
        }
        #endregion

        public void FindMostOccurredWordInFolder()
        {
            var wordCount = new Dictionary<string, int>();

            foreach (var file in TextFiles)
            {
                var fileContent = TextFile.RemoveSpecificCharacters(file.Text);
                var words = fileContent.Split(' ');

                foreach (var word in words)
                {
                    if (wordCount.ContainsKey(word))
                    {
                        wordCount[word] += 1;
                    }
                    else
                    {
                        wordCount.Add(word, 1);
                    }
                }
            }

            var orderedWords = 
                from words 
                in wordCount 
                orderby words.Value descending 
                select words;

            MostOccurredWord = orderedWords.First().Key;
        }

        public void FindLongestAndShortestFile()
        {
            var orderedFiles = TextFiles.OrderBy(x => x.NumberOfWords);

            LongestFile = orderedFiles.Last().Name;
            ShortestFile = orderedFiles.First().Name;
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
            _viewModel.FindMostOccurredWordInFolder();
            _viewModel.FindLongestAndShortestFile();
        }

        public event EventHandler CanExecuteChanged;
    }
}
