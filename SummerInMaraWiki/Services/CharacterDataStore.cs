﻿using SQLite;
using SummerInMaraWiki.Models;
using SummerInMaraWiki.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

[assembly: Dependency(typeof(CharacterDataStore))]
namespace SummerInMaraWiki.Services
{
    public class CharacterDataStore : SQLiteManager
    {
        public CharacterDataStore()
        {

        }

        public async Task<int> SaveItemAsync(Character item)
        {
            if (await GetItemByCodeAsync(item.Code) == null)
            {
                return await Database.InsertAsync(item);
            }
            else
            {
                return await Database.UpdateAsync(item);
            }
        }

        public async Task<int> DeleteItemAsync(int code)
        {
            return await Database.DeleteAsync(await GetItemByCodeAsync(code));
        }

        public async Task<Character> GetItemByCodeAsync(int code)
        {
            return await Database.Table<Character>().Where(i => i.Code == code).FirstOrDefaultAsync();
        }

        public async Task<List<Character>> GetItemsAsync()
        {
            return await Database.Table<Character>().ToListAsync();
        }
    }
}
