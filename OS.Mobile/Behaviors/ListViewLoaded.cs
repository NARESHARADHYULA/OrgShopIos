using Syncfusion.ListView.XForms;
using System.Windows.Input;
using TheOrganicShop.Models.Dtos.Order;
using Xamarin.Forms;
namespace TheOrganicShop.Mobile.Behaviors
{
    class ListViewLoaded : Behavior<SfListView>
    {
        SfListView listView;
        public string EventType { get; set; }
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

            listView.SelectionChanged += listView_SelectionChanged;
            base.OnAttachedTo(bindable);
        }

        private void listView_SelectionChanged(object sender, ItemSelectionChangedEventArgs e)
        {
            if (Command == null)
            {
                return;
            }
            if (Command.CanExecute(listView)) Command.Execute(listView);
        }



        protected override void OnDetachingFrom(SfListView bindable)
        {
            listView.SelectionChanged += listView_SelectionChanged;
            base.OnDetachingFrom(bindable);
        }
    }
}
