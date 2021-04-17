using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        public SelectSourceDirectory setSourceDirectory => new SelectSourceDirectory(this);

        public string PathToSourceDirectory
        {
            get => _pathToSourceDirectory;
            set
            {
                _pathToSourceDirectory = value;
                OnPropertyChanged(nameof(PathToSourceDirectory));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class SelectSourceDirectory : ICommand
    {
        private readonly MainViewViewModel _viewModel;

        public SelectSourceDirectory(MainViewViewModel viewModel)
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
        }

        public event EventHandler CanExecuteChanged;
    }
}
