using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Essentials;
using Acr.UserDialogs;

namespace TheOrganicShop.Mobile.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        #region Private

        private bool isBusy;
        public bool IsNotConnected { get; set; }

        #endregion
        public BaseViewModel()
        {
            Connectivity.ConnectivityChanged += Connectivity_ConnectivityChanged;
            IsNotConnected = Connectivity.NetworkAccess != NetworkAccess.Internet;

        }
        ~BaseViewModel()
        {
            Connectivity.ConnectivityChanged -= Connectivity_ConnectivityChanged;
        }

        void Connectivity_ConnectivityChanged(object sender, ConnectivityChangedEventArgs e)
        {
            if (e.NetworkAccess != NetworkAccess.Internet)
                UserDialogs.Instance.Toast("Oops, looks like you don't have internet connection :(");
            else
                UserDialogs.Instance.Toast("Your internet connection is back :)");
        }
        #region Properties

        public bool IsBusy
        {
            get => isBusy;
            set
            {
                isBusy = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null) return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}