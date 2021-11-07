using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace TheOrganicShop.Mobile.DataService.Interfaces
{
    public interface ILocalStorage
    {
        SQLiteConnection GetConnection();
    }
}
