using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SQLite;
using TheWeatherApp.Models;

namespace TheWeatherApp.Repository
{
    public class FavoritesRepository
    {
        readonly SQLiteAsyncConnection _database;

        public FavoritesRepository(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);

            _database.CreateTableAsync<Favorites>().Wait();
        }

        public async Task<List<Favorites>> GetFavoritesAsync()
        {
            return await _database.Table<Favorites>().ToListAsync();
        }
        
        public async Task<bool> SaveFavoriteAsync(List<Favorites> items)
        {
            var currentList = await GetFavoritesAsync();

            foreach (var item in items)
            {
                if (currentList.All(c => c.ZipCode.Trim() == item.ZipCode))
                {
                    await _database.InsertAsync(item);
                }
            }

            return true;
        }

        public async Task<int> DeleteFavoriteAsync(Favorites item)
        {
            return await _database.DeleteAsync(item);
        }
    }
}

