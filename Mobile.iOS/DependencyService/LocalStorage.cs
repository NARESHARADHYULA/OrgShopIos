using Xamarin.Forms;
using System.IO;
using SQLite;
using TheOrganicShop.Mobile.DataService.Interfaces;
using TheOrganicShop.Mobile.iOS.DependencyService;
using Environment = System.Environment;

[assembly: Dependency(typeof(LocalStorage))]
namespace TheOrganicShop.Mobile.iOS.DependencyService
{
    public class LocalStorage: ILocalStorage
    {
        public SQLiteConnection GetConnection()
        {
            var path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            path = Path.Combine(path, "TheOrganicShop.db3");
            var connection = new SQLiteConnection(path);
            return connection;
        }
    }
}