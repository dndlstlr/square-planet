using System;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Net.NetworkInformation;

using Windows.UI.Xaml;

using AppStudio.Services;
using AppStudio.Data;

namespace AppStudio.ViewModels
{
    public class MainViewModel : BindableBase
    {
       private LatestHeadlinesViewModel _latestHeadlinesModel;
       private NZNewsViewModel _nZNewsModel;
       private WorldViewModel _worldModel;
       private PoliticalViewModel _politicalModel;
       private BusinessViewModel _businessModel;
       private MoreHeadlinesViewModel _moreHeadlinesModel;
        private PrivacyViewModel _privacyModel;

        private ViewModelBase _selectedItem = null;

        public MainViewModel()
        {
            _selectedItem = LatestHeadlinesModel;
            _privacyModel = new PrivacyViewModel();

        }
 
        public LatestHeadlinesViewModel LatestHeadlinesModel
        {
            get { return _latestHeadlinesModel ?? (_latestHeadlinesModel = new LatestHeadlinesViewModel()); }
        }
 
        public NZNewsViewModel NZNewsModel
        {
            get { return _nZNewsModel ?? (_nZNewsModel = new NZNewsViewModel()); }
        }
 
        public WorldViewModel WorldModel
        {
            get { return _worldModel ?? (_worldModel = new WorldViewModel()); }
        }
 
        public PoliticalViewModel PoliticalModel
        {
            get { return _politicalModel ?? (_politicalModel = new PoliticalViewModel()); }
        }
 
        public BusinessViewModel BusinessModel
        {
            get { return _businessModel ?? (_businessModel = new BusinessViewModel()); }
        }
 
        public MoreHeadlinesViewModel MoreHeadlinesModel
        {
            get { return _moreHeadlinesModel ?? (_moreHeadlinesModel = new MoreHeadlinesViewModel()); }
        }

        public void SetViewType(ViewTypes viewType)
        {
            LatestHeadlinesModel.ViewType = viewType;
            NZNewsModel.ViewType = viewType;
            WorldModel.ViewType = viewType;
            PoliticalModel.ViewType = viewType;
            BusinessModel.ViewType = viewType;
            MoreHeadlinesModel.ViewType = viewType;
        }

        public ViewModelBase SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                SetProperty(ref _selectedItem, value);
                UpdateAppBar();
            }
        }

        public Visibility AppBarVisibility
        {
            get
            {
                return SelectedItem == null ? AboutVisibility : SelectedItem.AppBarVisibility;
            }
        }

        public Visibility AboutVisibility
        {

         get { return Visibility.Visible; }
        }

        public void UpdateAppBar()
        {
            OnPropertyChanged("AppBarVisibility");
            OnPropertyChanged("AboutVisibility");
        }

        /// <summary>
        /// Load ViewModel items asynchronous
        /// </summary>
        public async Task LoadDataAsync(bool forceRefresh = false)
        {
            var loadTasks = new Task[]
            { 
                LatestHeadlinesModel.LoadItemsAsync(forceRefresh),
                NZNewsModel.LoadItemsAsync(forceRefresh),
                WorldModel.LoadItemsAsync(forceRefresh),
                PoliticalModel.LoadItemsAsync(forceRefresh),
                BusinessModel.LoadItemsAsync(forceRefresh),
                MoreHeadlinesModel.LoadItemsAsync(forceRefresh),
            };
            await Task.WhenAll(loadTasks);
        }

        //
        //  ViewModel command implementation
        //
        public ICommand RefreshCommand
        {
            get
            {
                return new DelegateCommand(async () =>
                {
                    await LoadDataAsync(true);
                });
            }
        }

        public ICommand AboutCommand
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    NavigationServices.NavigateToPage("AboutThisAppPage");
                });
            }
        }

        public ICommand PrivacyCommand
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    NavigationServices.NavigateTo(_privacyModel.Url);
                });
            }
        }
    }
}
