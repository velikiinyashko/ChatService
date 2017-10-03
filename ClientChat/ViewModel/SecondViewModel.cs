using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using ClientChat.Model;

namespace ClientChat.ViewModel
{
    class SecondViewModel : VMInterface
    {
        private SecondModel _selectedList;
        private VMInterface _addList;

        public ObservableCollection<SecondModel> ListCollection { get; set; }

        public SecondModel SelectedList { get
            {
                return _selectedList;
            }
            set
            {
                _selectedList = value;
                OnPropertyChanged("SelectedList");
            }
        }

        public SecondViewModel()
        {
            ListCollection = new ObservableCollection<SecondModel>
            {
                new SecondModel { Name = "Tom", SecondName = "Gensly", Age = 34 },
                new SecondModel { Name = "Semmy", SecondName = "Tomson", Age = 26 },
                new SecondModel { Name = "Anny", SecondName = "Magyaer", Age = 19}
            };
        }

        public VMInterface AddList
        {
            get
            {
                return _addList ??
                    (_addList = new VMInterface(obj =>
                    {
                        SecondModel model = new SecondModel();
                        ListCollection.Add(model);
                        SelectedList = model;
                    }));
            }
        }


    }
}
