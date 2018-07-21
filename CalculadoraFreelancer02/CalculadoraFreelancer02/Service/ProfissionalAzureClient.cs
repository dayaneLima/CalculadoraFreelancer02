﻿using CalculadoraFreelancer02.Models;
using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CalculadoraFreelancer02.Service
{
    public class ProfissionalAzureClient
    {
        private IMobileServiceClient Client;
        private IMobileServiceTable<Profissional> Table;

        public ProfissionalAzureClient()
        {
            string MyAppServiceURL = "Sua Url Aqui";
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

        public async void Delete(Profissional profissional)
        {
            await Table.DeleteAsync(profissional);
        }
    }
}
