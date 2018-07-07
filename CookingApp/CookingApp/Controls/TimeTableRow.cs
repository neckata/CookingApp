using CookingApp.ViewModels.CookersPage;
using System.Collections;
using System.Collections.Generic;
using Xamarin.Forms;

namespace CookingApp.Controls
{
    public class TimeTableRow : Grid
    {
        public static readonly BindableProperty ItemsSourceProperty = BindableProperty.Create(nameof(ItemsSource), typeof(IEnumerable), typeof(TimeTableRow), new List<TimeTableCellViewModel>(), BindingMode.OneWay, propertyChanged: ItemsChanged);

        public IEnumerable ItemsSource
        {
            get { return (IEnumerable)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        private static void ItemsChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var grid = bindable as Grid;
            foreach (var item in newValue as IEnumerable<TimeTableCellViewModel>)
                grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(item.Percentage, GridUnitType.Star) });

            int counter = 0;
            foreach (var item in newValue as IEnumerable<TimeTableCellViewModel>)
            {
                grid.Children.Add(new BoxView() { HeightRequest = 16, BackgroundColor = item.IsTaken ? Color.Red : Color.Green, Margin = 1 }, counter, 0);
                counter++;
            }
        }
    }
}
