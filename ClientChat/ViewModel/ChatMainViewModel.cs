using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using ClientChat.Model;
using System.Windows;

namespace ClientChat.ViewModel
{
    public class ChatMainViewModel : VMInterface
    {
        private ObservableCollection<ListUsers> _users;
        private ListUsers _selectedUser;
        private VMInterface _exitApp;
        private VMInterface _sendMessage;

        public ChatMainViewModel()
        {

        }

        public ListUsers SelectedUser
        {
            get
            {
                return _selectedUser;
            }
            set
            {
                _selectedUser = value;
                OnPropertyChanged("SelectedUser");
            }
        }

        public ObservableCollection<ListUsers> Users
        {
            get
            {
                return _users;
            }
            set
            {
                _users = value;
                OnPropertyChanged("Users");
            }
        }

        public VMInterface ExitApp
        {
            get
            {
                return _exitApp ??
                    (_exitApp = new VMInterface(obj =>
                    {
                        Application.Current.MainWindow.Close();
                    }));
            }
        }

        public VMInterface SendMessage
        {
            get
            {
                return _sendMessage ??
                    (_sendMessage = new VMInterface(obj =>
                    {

                    }));
            }
        }
    }
}
