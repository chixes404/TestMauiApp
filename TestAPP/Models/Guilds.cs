using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace TestAPP.Models
{
    public class Guilds : ObservableObject
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public string Equipment { get; set; }
        public int Rank { get; set; }
        public int Members { get; set; }
        private bool _isExpanded;
        public bool IsExpanded
        {
            get => _isExpanded;
            set => SetProperty(ref _isExpanded, value);
        }

        public ICommand ToggleExpandCommand { get; }

        public Guilds()
        {
            ToggleExpandCommand = new Command(() => IsExpanded = !IsExpanded);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
