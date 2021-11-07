using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Newtonsoft.Json;
using TheOrganicShop.Mobile.Commands;
using TheOrganicShop.Mobile.DataService.Interfaces;
using TheOrganicShop.Models.Dtos;
using TheOrganicShop.Models.Dtos.Category;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace TheOrganicShop.Mobile.ViewModels
{
    /// <summary>
    /// ViewModel for Category page.
    /// </summary>
    [Preserve(AllMembers = true)]
    [DataContract]
    public class CategoryPageViewModel : BaseViewModel
    {
        #region Public properties

        private bool isLoading = false;

        private string orderingDate;

        public ICommand BackButtonClickedCommand => new Command(BackButtonClicked);

        public bool IsLoading
        {
            get { return isLoading; }
            set
            {
                this.isLoading = value;
                OnPropertyChanged("IsLoading");
            }
        }
        public ObservableCollection<GetCategoryDtoMobileForView> Categories
        {
            get => categories;
            set
            {
                if (categories == value) return;

                categories = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region Commands
        /// <summary>
        /// Gets or sets the command that will be executed when the Category is selected.
        /// </summary>
        public DelegateCommand CategorySelectedCommand =>
            categorySelectedCommand ?? (categorySelectedCommand = new DelegateCommand(CategorySelected));
        public DelegateCommand BackButtonCommand =>
           backButtonCommand ?? (backButtonCommand = new DelegateCommand(BackButtonClicked));

        #endregion

        #region Fields

        private ObservableCollection<GetCategoryDtoMobileForView> categories;

        private DelegateCommand categorySelectedCommand;
        private DelegateCommand backButtonCommand;


        private readonly ICategoryDataService categoryDataService;

        #endregion


        #region Constructor

        /// <summary>
        /// Initializes a new instance for the <see cref="CategoryPageViewModel" /> class.
        /// </summary>
        public CategoryPageViewModel(ICategoryDataService categoryDataService, int userId, string currentDate)
        {
            IsLoading = true;
            this.categoryDataService = categoryDataService;
            orderingDate = currentDate;
            Device.InvokeOnMainThreadAsync(async () => { await FetchCategories(); });
        }

        #endregion


        public async Task FetchCategories()
        {

            try
            {
                var categories = await categoryDataService.GetCategoriesAsync();
                if (categories != null && categories.Count > 0)
                {
                    Categories = new ObservableCollection<GetCategoryDtoMobileForView>(categories);
                }

            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }
            IsLoading = false;
        }

        /// <summary>
        /// Invoked when the Category is selected.
        /// </summary>
        /// <param name="obj">The Object</param>
        private async void CategorySelected(object attachedObject)
        {
            if (!(attachedObject is GetCategoryDtoMobileForView) && attachedObject is string)
            {
                // TODO: ISR: Show respective page here
                return;
            }

            var categoryDto = attachedObject as GetCategoryDtoMobileForView;
            var queryParams = new QueryParamsDto { Date = orderingDate, CategoryId = categoryDto.Id };

            await Shell.Current.GoToAsync($"productdetail?queryParams={JsonConvert.SerializeObject(queryParams)}");

        }

        private async void onBackButtonClicked(object attachedObject)
        {

            //Application.Current.MainPage = new AppShell();
            await Application.Current.MainPage.Navigation.PushAsync(new AppShell());

        }

        /// <summary>
        /// Invoked when an back button is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>
        private async void BackButtonClicked(object obj)
        {
            await Shell.Current.GoToAsync("..");
        }
    }
}
