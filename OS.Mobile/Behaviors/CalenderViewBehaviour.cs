using Syncfusion.ListView.XForms;
using System.Windows.Input;
using TheOrganicShop.Models.Dtos.DomainData;
using TheOrganicShop.Models.Dtos.Order;
using Xamarin.Forms;
using ItemTappedEventArgs = Syncfusion.ListView.XForms.ItemTappedEventArgs;

namespace TheOrganicShop.Mobile.Behaviors
{
    public class CalenderViewBehaviour : Behavior<SfListView>
    {
        SfListView listView;
        /// <summary>
        /// Gets or sets the CommandProperty, and it is a bindable property.
        /// </summary>
        public static readonly BindableProperty CommandProperty =
            BindableProperty.Create("Command", typeof(ICommand), typeof(Behavior));

        /// <summary>
        /// Gets or sets the Command.
        /// </summary>
        public ICommand Command
        {
            get => (ICommand)GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }
        protected override void OnAttachedTo(SfListView bindable)
        {
            listView = bindable;
            listView.SelectionChanging += ListView_SelectionChanging;
            bindable.ItemTapped += BindableListView_ItemTapped;
            base.OnAttachedTo(bindable);
        }

        private void ListView_SelectionChanging(object sender, ItemSelectionChangingEventArgs e)
        {
            if (listView.SelectionMode == Syncfusion.ListView.XForms.SelectionMode.Single)
            {
                if (e.AddedItems.Count > 0)
                {
                    var item = e.AddedItems[0] as GetCalenderDtoMobileForView;
                    item.LabelTextColor = Color.FromHex("#FFFFFF");
                }
                if (e.RemovedItems.Count > 0)
                {
                    var item = e.RemovedItems[0] as GetCalenderDtoMobileForView;
                    item.LabelTextColor = Color.FromHex("#FF56565A");
                }
            }

        }

        /// <summary>
        /// Invoked when tapping the listview item.
        /// </summary>
        /// <param name="sender">The Sender</param>
        /// <param name="e">ItemTapped EventArgs</param>
        private void BindableListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (Command == null)
            {
                return;
            }
            if (Command.CanExecute(e.ItemData)) Command.Execute(e.ItemData);
        }

        protected override void OnDetachingFrom(SfListView bindable)
        {
            //listView.SelectionChanging -= ListView_SelectionChanging;
            bindable.ItemTapped -= BindableListView_ItemTapped;
            base.OnDetachingFrom(bindable);
        }
    }
}
