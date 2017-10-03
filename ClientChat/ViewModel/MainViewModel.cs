using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using System.Windows.Input;
using GalaSoft.MvvmLight.CommandWpf;
using System.Windows;
using System.Collections.ObjectModel;
using ClientChat.Model;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Windows.Threading;
using ClientChat.View;

namespace ClientChat.ViewModel
{
    class MainViewModel : ViewModelBase
    {
        public ObservableCollection<GetMessage> Messages { get; set; }
        //private Dispatcher Disp;
        private ConsumerMessage _consumer;


        public MainViewModel()
        {
            Messages = new ObservableCollection<GetMessage>();
            //Disp = Dispatcher.CurrentDispatcher;
            _consumer = new ConsumerMessage();
            
        }

        private ICommand _clickButton;

        public ICommand ClickButton
        {
            get
            {
                return _clickButton ?? (_clickButton = new RelayCommand(() =>
                {
                    SecondView view = new SecondView();
                    view.Show();
                }));
            }
        }
    }
}
