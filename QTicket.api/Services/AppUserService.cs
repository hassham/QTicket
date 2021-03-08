using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using QTicket.api.Dtos;
using QTicket.api.Entities;

namespace QTicket.api.Services
{
    public class AppUserService
    {
        private readonly IMongoCollection<AppUser> _appUser;

        public AppUserService(IDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _appUser = database.GetCollection<AppUser>("AppUsers");
        }

        public async Task<List<AppUser>> Get() => await _appUser.Find(appUser => true).ToListAsync();

        public async Task<AppUser> Get(string id) => await _appUser.Find<AppUser>(appUser => appUser.Id == id).FirstOrDefaultAsync();

        public async Task<AppUser> Create(AppUser appUser)
        {
            await _appUser.InsertOneAsync(appUser);
            return appUser;
        }

        public async Task Update(string id, AppUser appUserIn) => await _appUser.ReplaceOneAsync(appUser => appUser.Id == id, appUserIn);

        public async Task Remove(AppUser appUserIn) => await _appUser.DeleteOneAsync(appUser => appUser.Id == appUserIn.Id);

        public async Task Remove(string id) => await _appUser.DeleteOneAsync(appUser => appUser.Id == id);

        public async Task<bool> UserExists(string username)
        {
            var user = await _appUser.Find(x => x.UserName == username).FirstOrDefaultAsync();
            if (user != null)
                return true;

            return false;
        }

        public async Task<AppUser> Login(LoginDto loginDto)
        {
            return await _appUser.Find(x => x.UserName == loginDto.Username && x.Password == loginDto.Password).FirstOrDefaultAsync();
        }
    }
}
