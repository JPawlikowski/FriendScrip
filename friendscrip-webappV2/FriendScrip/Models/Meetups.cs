using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FriendScrip.Models
{
    public class Meetups
    {


        public int ID { get; set; }

        public int ItemID { get; set; }

        public string SellerId { get; set; }

        public string BuyerID { get; set; }

        public string Title { get; set; }

        public string StartTime { get; set; }

        public float Price { get; set; }

        public string Location { get; set; }

        public bool Active { get; set; }

    }
}