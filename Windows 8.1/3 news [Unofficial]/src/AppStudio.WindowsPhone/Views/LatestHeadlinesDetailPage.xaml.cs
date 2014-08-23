using System;
using System.Net.NetworkInformation;

using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Windows.ApplicationModel.DataTransfer;

using AppStudio.Services;
using AppStudio.ViewModels;

namespace AppStudio.Views
{
    public sealed partial class LatestHeadlinesDetail : Page
    {
        private NavigationHelper _navigationHelper;

        private DataTransferManager _dataTransferManager;

        public LatestHeadlinesDetail()
        {
            this.InitializeComponent();
            _navigationHelper = new NavigationHelper(this);

            LatestHeadlinesModel = new LatestHeadlinesViewModel();
        }

        public LatestHeadlinesViewModel LatestHeadlinesModel { get; private set; }

        public NavigationHelper NavigationHelper
        {
            get { return _navigationHelper; }
        }

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            _dataTransferManager = DataTransferManager.GetForCurrentView();
            _dataTransferManager.DataRequested += OnDataRequested;

            _navigationHelper.OnNavigatedTo(e);

            await LatestHeadlinesModel.LoadItemsAsync();
            LatestHeadlinesModel.SelectItem(e.Parameter);

            if (LatestHeadlinesModel != null)
            {
                LatestHeadlinesModel.ViewType = ViewTypes.Detail;
            }
            DataContext = this;
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            _navigationHelper.OnNavigatedFrom(e);
            _dataTransferManager.DataRequested -= OnDataRequested;
        }

        private void OnDataRequested(DataTransferManager sender, DataRequestedEventArgs args)
        {
            if (LatestHeadlinesModel != null)
            {
                LatestHeadlinesModel.GetShareContent(args.Request);
            }
        }
    }
}
