using no.rag.NerdDice.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorTest1
{
    public interface IAvailableDiceRolls
    {
        Task<List<string>> GetStandardRolls();
        Task<List<string>> GetFavoriteRolls();

        Task AddFavoriteRoll(string roll);
        Task RemoveFavoriteRoll(string roll);
    }
    public class AvailableDiceRolls : IAvailableDiceRolls
    {
        public async Task<List<string>> GetStandardRolls()
        {
            return await Task.FromResult(new List<string>
            {
                "1d6","2d6","3d6","4d6","5d6","6d6","3d4"
            });
        }
        public async Task<List<string>> GetFavoriteRolls()
        {
            return await LocalStorage.GetItem<List<string>>("favorites") ?? new List<string>();
        }

        public async Task AddFavoriteRoll(string roll)
        {
            var favoriteRolls = await GetFavoriteRolls();
            if (!favoriteRolls.Contains(roll))
            {
                favoriteRolls.Add(roll);

                var favorites = favoriteRolls.ToArray();
                await LocalStorage.SetItem("favorites", favorites);
            }
        }

        public async Task RemoveFavoriteRoll(string roll)
        {
            var favoriteRolls = await GetFavoriteRolls();
            if(favoriteRolls.Contains(roll))
            {
                favoriteRolls.Remove(roll);

                var favorites = favoriteRolls.ToArray();
                await LocalStorage.SetItem("favorites", favorites);
            }
        }
    }
}
