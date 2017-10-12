using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FriendScrip.Models.Repositorys
{
    public static class DatabaseFunctions
    {
        private static ScripEntities _db = new ScripEntities();

        public static List<Friendscrip_Items> GetItems(string uid)
        {
            ScripEntities _db = new ScripEntities();
            var user = _db.Friendscrip_Accounts.Where(x => x.ID.Equals(uid)).FirstOrDefault();
            if(user != null)
            {
                var userItems = user.Friendscrip_Items.ToList();
                return userItems;
            }
            return null;
        }


    }
}