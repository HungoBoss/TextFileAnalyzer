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
        private ObservableCollection<TextFileModel> _textFiles = new ObservableCollection<TextFileModel>();
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

        public ObservableCollection<TextFileModel> TextFiles
        {
            get => _textFiles;
            set
            {
                _textFiles = value;
                OnPropertyChanged(nameof(TextFiles));
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
                    var textFile = new TextFileModel(filename);

                    using (var reader = new StreamReader(file))
                    {
                        textFile.Text = reader.ReadToEnd();
                    }

                    textFile.GetNumberOfWords();
                    textFile.GetNumberOfSentences();
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
