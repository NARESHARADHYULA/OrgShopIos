using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace TheOrganicShop.Mobile.ViewModels
{
    public class SubCategoryPageViewModel
    {
        private ObservableCollection<TabItem> tabList;

        public ObservableCollection<TabItem> TabItemsList
        {
            get { return tabList; }
            set { tabList = value; }
        }

        public SubCategoryPageViewModel()
        {
            TabItemsList = new ObservableCollection<TabItem>();
            TabItemsList.Add(new TabItem { Name = "All", Id = 1 });
            TabItemsList.Add(new TabItem { Name = "Organic", Id = 2 });
            TabItemsList.Add(new TabItem { Name = "NonOrganic", Id = 3 });

        }
    }

    public class TabItem
    {
        public string Name { get; set; }
        public long Id { get; set; }
    }

}
