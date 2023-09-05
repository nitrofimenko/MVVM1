using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

namespace MVVM
{
    class MainWindowViewModel : INotifyPropertyChanged
    {
        public MainWindowViewModel()
        {
            CurrentName = "Имя";
            CurrentSurname = "Фамилия";
            IsButtonEnabled = true;
        }

        private string _currentSurname;

        public string CurrentSurname
        {
            get
            {
                return _currentSurname;
            }
            set
            {
                _currentSurname = value;
                IsButtonEnabled = value == string.Empty ? false : true;
                RaisePropertyChanged(nameof(CurrentSurname));
            }
        }
        private string _currentName;

        public string CurrentName
        {
            get
            {
                return _currentName;
            }
            set
            {
                _currentName = value;
                IsButtonEnabled = value == string.Empty ? false : true;
                RaisePropertyChanged(nameof(CurrentName));
            }
        }

        private ObservableCollection<Person> _persons = new ObservableCollection<Person>();

        public ObservableCollection<Person> Persons
        {
            get
            {
                return _persons;
            }
            set
            {
                _persons = value;
                RaisePropertyChanged(nameof(Persons));
            }
        }

        private bool _isButtonEnabled;

        public bool IsButtonEnabled
        {
            get
            {
                return _isButtonEnabled;
            }
            set
            {
                _isButtonEnabled = value;
                RaisePropertyChanged(nameof(IsButtonEnabled));
            }
        }

        private DelegateCommand _AddCommand;
        public ICommand ButtonClick
        {
            get
            {
                if (_AddCommand == null)
                {
                    _AddCommand = new DelegateCommand(Add, CanAdd);
                }
                return _AddCommand;
            }
        }
        private void Add(object o)
        {
            Persons.Add(new Person() { Name = CurrentName, Surname = CurrentSurname });
            CurrentName = "";
            CurrentSurname = "";
        }

        private bool CanAdd(object o)
        {
            if (CurrentName == "" || CurrentSurname == "")
                return false;
            return true;
        }

        private void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
