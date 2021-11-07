using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using TheOrganicShop.Mobile.DataService.Interfaces;
using TheOrganicShop.Models.Dtos.User;
using Xamarin.Forms;

namespace TheOrganicShop.Mobile.DataService
{
    /// <summary>
    /// This class is used to maintain the data in local.
    /// </summary>
    [Preserve(AllMembers = true)]
    public class LocalStorage
    {
        private static LocalStorage database;

        public static LocalStorage Shared
        {
            get
            {
                if (database == null) database = new LocalStorage();
                return database;
            }
        }

        private SQLiteConnection Connection { get; set; }

        public bool Initialized { get; private set; }

        /// <summary>
        /// Create and initialize the local database connection.
        /// Create table.
        /// </summary>
        public void Init()
        {
            Connection = DependencyService.Get<ILocalStorage>().GetConnection();
            Connection.CreateTable<LocalStorageUser>();
            Initialized = true;
        }

        #region

        /// <summary>
        /// To get user logged details from local database.
        /// </summary>
        public LocalStorageUser GetUserInfo()
        {
            LocalStorageUser userInfo = null;
            if (Connection != null) userInfo = Connection.Table<LocalStorageUser>().OrderByDescending(x => x.Id).FirstOrDefault();
            return userInfo;
        }

        /// <summary>
        /// Insert user details.
        /// </summary>
        public void InsertUserDetail(LocalStorageUser userInfo)
        {
            if (Connection != null) Connection.Insert(userInfo);
        }

        /// <summary>
        /// Update user details.
        /// </summary>
        public void UpdateUserDetail(LocalStorageUser userInfo)
        {
            if (Connection != null) Connection.Update(userInfo);
        }

        /// <summary>
        /// Update user details.
        /// </summary>
        public void DeleteUserDetail(LocalStorageUser userInfo)
        {
            if (Connection != null) Connection.Delete(userInfo);
        }

        #endregion
    }
}
