using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using webapplication.Models;

namespace webapplication.Services
{
    public interface IFavoriteCryptosService
    {
        List<FavoriteCryptos> Post(FavoriteCryptos favoriteCryptos);
        List<FavoriteCryptos> Get(Guid userId);
        bool Delete(Guid user);
    }
}
