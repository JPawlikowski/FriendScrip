using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FriendScrip.Models
{
    public class Items
    {

        public int ID { get; set; }

        public string AccountID { get; set; }

        public string ItemName { get; set; }

        public string Condition { get; set; }

        public string PickupLocation { get; set; }

        public string MainIntersection { get; set; }

        public string Description { get; set; }

    }
}