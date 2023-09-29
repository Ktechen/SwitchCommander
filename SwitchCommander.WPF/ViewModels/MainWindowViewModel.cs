using System.ComponentModel;
using System.Windows.Input;
using SwitchCommander.WebAPI.Client;
using SwitchCommander.WPF.Common;

namespace SwitchCommander.WPF.ViewModels;

public class MainWindowViewModel : INotifyPropertyChanged
{
    private string _email;
    private string _name;
    private string _nameTwo;
    
    public event PropertyChangedEventHandler PropertyChanged;
    
    public string Email 
    { 
        get => _email;
        set
        {
            if (value == _email)
                return;
            _email = value;
            OnPropertyChanged(nameof(Email));
        }
    }

    public string Name 
    { 
        get => _name;
        set
        {
            if (value == _name)
                return;
            _name = value;
            OnPropertyChanged(nameof(Name));
        }
    }

    public string NameTwo 
    { 
        get => _nameTwo;
        set
        {
            if (value == _nameTwo)
                return;
            _nameTwo = value;
            OnPropertyChanged(nameof(NameTwo));
        }
    }

    public ICommand CreateUserCommand { get; }

    public MainWindowViewModel() 
    {
        CreateUserCommand = new DelegateCommand(CreateUser, CanCreateUser);
    }

    private bool CanCreateUser() 
    {
        // Return true or false depending on some rule.
        return true;
    }

    private void CreateUser() 
    {
        // Create user here.
        var model = new CreateUserRequest
        {
            Email = _email,
            Name = _name
        };
    }

    protected virtual void OnPropertyChanged(string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}