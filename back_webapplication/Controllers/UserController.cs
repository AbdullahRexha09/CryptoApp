using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using webapplication.DtoModels;
using webapplication.Models;
using webapplication.Services;
using webapplication.Utils;

namespace webapplication.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IFavoriteCryptosService favoriteCryptosService;
        public UserController(IFavoriteCryptosService favoriteCryptosService)
        {
            this.favoriteCryptosService = favoriteCryptosService;
        }
        // GET api/values
        //
        [HttpGet, Route("getcrypto"), Authorize(Roles = "User")]
        public List<CryptoModel> GetCrypto()
        {
            List<FavoriteCryptos> favoriteCryptos = favoriteCryptosService.Get(AuthController.userId);
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(Utilities.URL);
            CryptoObject cryptoObject = new CryptoObject();

            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = client.GetAsync(Utilities.URL).Result;  
            if (response.IsSuccessStatusCode)
            {
                var dataObjects = response.Content.ReadAsStringAsync();
                cryptoObject = JsonConvert.DeserializeObject<CryptoObject>(dataObjects.Result.ToString());
                foreach (var item in favoriteCryptos)
                {
                    var itemToChange = cryptoObject.Data.First(d => d.Id == item.CryptoId).isChecked = true;
                }
            }

            client.Dispose();
            return cryptoObject.Data;
        }
        [HttpGet, Route("getfavoritecrypto"), Authorize(Roles = "User")]
        public List<CryptoModel> GetFavoriteCrypto() 
        {
            List<FavoriteCryptos> favoriteCryptos = favoriteCryptosService.Get(AuthController.userId);
            List<CryptoModel> cryptoToReturn = new List<CryptoModel>();
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(Utilities.URL);
            CryptoObject cryptoObject = new CryptoObject();

            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = client.GetAsync(Utilities.URL).Result;
             if (response.IsSuccessStatusCode)
            {
                var dataObjects = response.Content.ReadAsStringAsync();
                cryptoObject = JsonConvert.DeserializeObject<CryptoObject>(dataObjects.Result.ToString());
                foreach (var item in favoriteCryptos)
                {
                    cryptoToReturn.Add(cryptoObject.Data.First(d => d.Id == item.CryptoId));
                }
            }
            return cryptoToReturn;
        }
        [HttpPost, Route("selectedCryptos"), Authorize(Roles = "User")]
        public List<CryptoModel> SelectedCryptos(List<CryptoModel> cryptoModels)
        {
            favoriteCryptosService.Delete(AuthController.userId);
            foreach (var item in cryptoModels)
            {
                FavoriteCryptos favoriteCryptos = new FavoriteCryptos();
                favoriteCryptos.UserId = AuthController.userId;
                favoriteCryptos.CryptoId = item.Id;

                favoriteCryptosService.Post(favoriteCryptos);
            }
            return null;
        }

    }
}
