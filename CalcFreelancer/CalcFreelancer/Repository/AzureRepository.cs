using CalcFreelancer.Models;
using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CalcFreelancer.Repository
{
    class AzureRepository
    {
        private IMobileServiceClient Client;
        private IMobileServiceTable<Profissional> Table;

        public AzureRepository()
        {
            string MyAppServiceURL = "https://calculadorafreelancer01.azurewebsites.net";
            Client = new MobileServiceClient(MyAppServiceURL);
            Table = Client.GetTable<Profissional>();
        }

        public async Task<IEnumerable<Profissional>> GetAll()
        {
            var empty = new Profissional[0];
            try
            {
                return await Table.ToEnumerableAsync();
            }
            catch (Exception)
            {
                return empty;
            }
        }

        public async void Insert(Profissional profissional)
        {
            await Table.InsertAsync(profissional);
        }

        public async Task Update(Profissional profissional)
        {
            await Table.UpdateAsync(profissional);
        }

        public async void Delete(Profissional profissional)
        {
            await Table.DeleteAsync(profissional);
        }

        public async Task<Profissional> Find(string id)
        {
            var itens = await Table.Where(i => i.Id == id).ToListAsync();
            return (itens.Count > 0) ? itens[0] : null;
        }

        public async Task<Profissional> GetFirst()
        {
            var itens = await Table.ToListAsync();
            return (itens.Count > 0) ? itens[0] : null;
        }
    }
}
