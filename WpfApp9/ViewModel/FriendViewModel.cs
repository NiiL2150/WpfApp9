using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp9.Model;
using WpfApp9.Service;
using WpfApp9.Service.Command;

namespace WpfApp9.ViewModel
{
    public class FriendViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private IFileService _fileService;
        public ObservableCollection<Friend> Friends { get; set; }

        public FriendViewModel(IFileService fileService)
        {
            this._fileService = fileService;

            Friends = new ObservableCollection<Friend>
            {
                new Friend{Name="Friend1", TrustLVL=1},
                new Friend{Name="Friend2", TrustLVL=2},
                new Friend{Name="Friend3", TrustLVL=3},
                new Friend{Name="Friend4", TrustLVL=4},
                new Friend{Name="Friend5", TrustLVL=5}
            };
        }

        private string _tmpString;
        public string TmpName
        {
            get => _tmpString;
            set
            {
                _tmpString = value;
                OnPropertyChanged(nameof(TmpName));
            }
        }
        private int selectedFriendIndex;
        private Friend selectedFriend;

        public int SelectedFriendIndex { get => selectedFriendIndex; set => selectedFriendIndex = value; }
        public Friend SelectedFriend
        {
            get => selectedFriend;
            set
            {
                selectedFriend = value;
                OnPropertyChanged(nameof(SelectedFriend));
            }
        }

        private RelayCommand _saveCommand;
        private RelayCommand _openCommand;
        private RelayCommand _addCommand;
        private RelayCommand _removeCommand;

        public RelayCommand SaveCommand
        {
            get
            {
                return _saveCommand ?? (_saveCommand = new RelayCommand(obj =>
                _fileService.Save("friends.json", Friends.Select(f => new Friend
                {
                    Name = f.Name,
                    TrustLVL = f.TrustLVL
                }).ToList())
                ));
            }
        }

        public RelayCommand OpenCommand
        {
            get
            {
                return _openCommand ?? (_openCommand = new RelayCommand(obj =>
                {
                    var friends = _fileService.Open("friends.json");
                    Friends.Clear();
                    foreach (var f in friends)
                    {
                        Friends.Add(f);
                    }
                }
                ));
            }
        }

        public RelayCommand AddCommand
        {
            get
            {
                return _addCommand ?? (_addCommand = new RelayCommand(obj =>
                {
                    Friend friend = new Friend();
                    friend.TrustLVL = 1;
                    friend.Name = TmpName;
                    TmpName = "";
                    Friends.Add(friend);
                    SelectedFriend = friend;
                }));
            }
        }

        public RelayCommand RemoveCommand
        {
            get
            {
                return _removeCommand ?? (_removeCommand = new RelayCommand(obj =>
                {
                    Friend friend = obj as Friend;
                    if (friend != null)
                    {
                        Friends.Remove(friend);
                    }
                    SelectedFriend = Friends.Count > 0 ? Friends[0] : null;
                }));
            }
        }
    }
}
