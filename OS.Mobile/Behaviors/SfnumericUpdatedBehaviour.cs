using Syncfusion.SfNumericUpDown.XForms;
using System;
using System.Windows.Input;
using TheOrganicShop.Models.Dtos.UserCart;
using Xamarin.Forms;

namespace TheOrganicShop.Mobile.Behaviors
{
    public class SfNumericUpdatedBehaviour : Behavior<SfNumericUpDown>
    {


        SfNumericUpDown sfNumericUpDown;
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


        protected override void OnAttachedTo(SfNumericUpDown bindable)
        {
            sfNumericUpDown = bindable;
            base.OnAttachedTo(bindable);
            //bindable.Completed += onNumericUpdated;
            bindable.ValueChanged += onNumericValueChanged;

        }
        void onNumericValueChanged(object sender, ValueEventArgs e)
        {

            if (Command == null)
            {
                return;
            }

            var productUpdatedQuantityDto = new CartItemChangedDto()
            {
                CartItemId = int.Parse(sfNumericUpDown.ClassId),
                OldQuantity = int.Parse(e.OldValue.ToString()),
                NewQuantity = int.Parse(e.Value.ToString())
            };
            if (Command.CanExecute(productUpdatedQuantityDto)) Command.Execute(productUpdatedQuantityDto);
        }
        void onNumericUpdated(object sender, EventArgs e)
        {
            if (Command == null)
            {
                return;
            }
            if (Command.CanExecute(sfNumericUpDown)) Command.Execute(sfNumericUpDown);
        }
        protected override void OnDetachingFrom(SfNumericUpDown bindable)
        {
            sfNumericUpDown = bindable;
            base.OnDetachingFrom(bindable);
            bindable.ValueChanged -= onNumericValueChanged;

        }


    }


}
