using Syncfusion.ListView.XForms;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TheOrganicShop.Mobile.ViewModels;
using TheOrganicShop.Mobile.Views;
using TheOrganicShop.Models.Dtos.ProductDetail;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace TheOrganicShop.Mobile.Behaviors
{
    /// <summary>
    /// This class extends the behavior of the catalog page and detail page
    /// </summary>
    [Preserve(AllMembers = true)]
    public class CartBehavior : Behavior<ContentPage>
    {
        #region Fields

        private ContentPage contentPage;
        SfListView ListView;
        SearchBar SearchBar;

        #endregion

        #region Method

        /// <summary>
        /// Invoked when adding catalog page and detail page.
        /// </summary>
        /// <param name="bindableContentPage">Bindable ContentPage</param>
        protected override void OnAttachedTo(ContentPage bindableContentPage)
        {
            ListView = bindableContentPage.FindByName<SfListView>("listView");
            SearchBar = bindableContentPage.FindByName<SearchBar>("productsfilter");
            if (SearchBar != null)
            {
                SearchBar.TextChanged += SearchBar_TextChanged;
            }
            base.OnAttachedTo(bindableContentPage);
            contentPage = bindableContentPage;
            bindableContentPage.Appearing += async (o, args) => await Bindable_Appearing(o, args);
        }

        /// <summary>
        /// Invoked when exit from the page.
        /// </summary>
        /// <param name="bindableContentPage">Content Page</param>
        protected override void OnDetachingFrom(ContentPage bindableContentPage)
        {
            base.OnDetachingFrom(bindableContentPage);
            bindableContentPage.Appearing -= async (o, args) => await Bindable_Appearing(o, args);
        }

        /// <summary>
        /// Invoked when appearing the page.
        /// </summary>
        /// <param name="sender">Content Page</param>
        /// <param name="e">Event Args</param>
        private async Task Bindable_Appearing(object sender, EventArgs e)
        {
            if (contentPage != null)
            {
                if (contentPage.BindingContext is UserCartViewModel cartPageViewModel)
                     await cartPageViewModel.FetchCartProducts();
                //    if (contentPage.BindingContext is DetailPageViewModel detailPageViewModel &&
                //        detailPageViewModel != null)
                //        detailPageViewModel.UpdateCartItemCount();
                //    else if (contentPage.BindingContext is CatalogPageViewModel catalogPageViewModel &&
                //             catalogPageViewModel != null)
                //        catalogPageViewModel.UpdateCartItemCount();
                //    else if (contentPage.BindingContext is WishlistViewModel wishlistViewModel && wishlistViewModel != null)
                //        wishlistViewModel.UpdateCartItemCount();
                //    else if (contentPage.BindingContext is CartPageViewModel cartPageViewModel && cartPageViewModel != null)
                //        cartPageViewModel.FetchCartProducts();
                //}
            }

            #endregion
        }

        private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (ListView.DataSource != null)
            {
                ListView.DataSource.Filter = FilterProducts;
                ListView.DataSource.RefreshFilter();
            }
            ListView.RefreshView();
        }

        private bool FilterProducts(object obj)
        {
            if (SearchBar == null || SearchBar.Text == null)
                return true;

            var product = obj as GetProductDetailDtoMobileForView;
            return product.Name.ToLower().Contains(SearchBar.Text.ToLower());
               
        }
    }
}
