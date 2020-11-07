using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using webapplication.Models;

namespace webapplication.Services
{
    public class FavoriteCryptosService : IFavoriteCryptosService
    {
        public readonly AppDbContext _db;
        public FavoriteCryptosService(AppDbContext _db)
        {
            this._db = _db;
        }

        public List<FavoriteCryptos> Get(Guid userId)
        {
            List<FavoriteCryptos> favoriteCryptos = _db.FavoriteCryptos.Where(x => x.UserId == userId).ToList();
            return favoriteCryptos;
        }

        public List<FavoriteCryptos> Post(FavoriteCryptos favoriteCryptos)
        {
                _db.FavoriteCryptos.Add(favoriteCryptos);
                _db.SaveChanges();
            
            return null;
        }
        public bool Delete(Guid user) 
        {
            List<FavoriteCryptos> favoriteCryptos = Get(user);
            foreach (var item in favoriteCryptos) 
            {
                _db.FavoriteCryptos.Remove(item);
                _db.SaveChanges();
            }
            return true;
        }
    }
}
