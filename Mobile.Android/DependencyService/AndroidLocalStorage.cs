using System.IO;
using SQLite;
using TheOrganicShop.Mobile.DataService.Interfaces;
using TheOrganicShop.Mobile.Droid.DependencyService;
using Xamarin.Forms;
using Environment = System.Environment;

[assembly: Dependency(typeof(AndroidLocalStorage))]
namespace TheOrganicShop.Mobile.Droid.DependencyService
{
    public class AndroidLocalStorage : ILocalStorage
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