using Syncfusion.SfAutoComplete.XForms;
using System.Windows.Input;
using Xamarin.Forms;
using SelectionChangedEventArgs = Syncfusion.SfAutoComplete.XForms.SelectionChangedEventArgs;

namespace TheOrganicShop.Mobile.Behaviors
{
    public class SelectionChangedBehavior : Behavior<SfAutoComplete>
    {
        SfAutoComplete autocomplete;
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


        protected override void OnAttachedTo(SfAutoComplete bindable)
        {
            autocomplete = bindable;
            base.OnAttachedTo(bindable);
            bindable.SelectionChanged += Bindable_SelectionChanged;
        }

        protected override void OnDetachingFrom(SfAutoComplete bindable)
        {
            autocomplete = bindable;
            base.OnDetachingFrom(bindable);
            bindable.SelectionChanged -= Bindable_SelectionChanged;
        }

        void Bindable_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (Command == null)
            {
                return;
            }

            if (Command.CanExecute(e.Value)) Command.Execute(e.Value);


        }
    }
}
