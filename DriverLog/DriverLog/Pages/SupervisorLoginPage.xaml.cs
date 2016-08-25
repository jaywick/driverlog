using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace DriverLog.Pages
{
    public partial class SupervisorLoginPage : ContentPage, INotifyPropertyChanged, IResultPage<string>
    {
        /// <summary>
        /// Awaiters are used to <code>await</code> the result of a IResultPage
        /// </summary>
        public TaskCompletionSource<string> Awaiter { get; } = new TaskCompletionSource<string>();

        // fyi: observable collections update UI on adding and removing items automagically
        public ObservableCollection<string> Supervisors { get; } = new ObservableCollection<string>();

        public SupervisorLoginPage()
        {
            BindingContext = this;
            InitializeComponent();

            PopulateSupervisors();
        }

        private void PopulateSupervisors()
        {
            Supervisors.Add("Bob");
            Supervisors.Add("Jane");
            Supervisors.Add("Yolo");
        }

        public void SelectSupervisor(object sender, SelectedItemChangedEventArgs e)
        {
            var supervisor = (string)e.SelectedItem;
            App.Current.Storyboard.Return(supervisor, this);
        }
    }
}
