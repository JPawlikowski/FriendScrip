//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FriendScrip.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Friendscrip_Accounts
    {
        public Friendscrip_Accounts()
        {
            this.Friendscrip_Items = new HashSet<Friendscrip_Items>();
            this.Friendscrip_Meetups = new HashSet<Friendscrip_Meetups>();
        }
    
        public string ID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int Friendscrip { get; set; }
        public bool IsAdmin { get; set; }
    
        public virtual ICollection<Friendscrip_Items> Friendscrip_Items { get; set; }
        public virtual ICollection<Friendscrip_Meetups> Friendscrip_Meetups { get; set; }
    }
}
