using FriendScrip.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Stripe;

namespace Friendscrip.Controllers
{
    public class HomeController : Controller
    {
        // started implementing the stripe api but had a flat tire on the way
        public ActionResult Charge(string stripeEmail, string stripeToken)
        {
            return null;
        }

        private ScripEntities _db = new ScripEntities(); // connection to database

        //// will store the calendar items of the currently logged in user in the viewbag so it can be accessed by the view
        //public ActionResult Index()
        //{
            
        //}

        public ActionResult Index(int? itemID) // accountid and itemid of seller shit
        {
            if(this.Session["ID"] == null)
            {
                return RedirectToAction("Login");
            }
            if (itemID == null) {
                var userId = this.Session["ID"].ToString();
                var account = _db.Friendscrip_Accounts.Where(x => x.ID == userId).FirstOrDefault();
                if (account != null)
                {


                    var itemsForSale = account.Friendscrip_Items.ToList();
                    var scheduledMeetups = new List<Meetups>();

                    foreach (var item in itemsForSale)
                    {
                        var meetupForItem = item.Friendscrip_Meetups.Where(x => x.Active).FirstOrDefault();

                        if (meetupForItem == null) { continue; }
                        scheduledMeetups.Add(new Meetups
                        {
                            ID = meetupForItem.ID,
                            ItemID = meetupForItem.ItemID,
                            SellerId = meetupForItem.Friendscrip_Items.AccountID,
                            BuyerID = meetupForItem.BuyerID,
                            Title = meetupForItem.Title,
                            StartTime = meetupForItem.StartTime.ToShortDateString(),
                            Price = meetupForItem.Price,
                            Location = meetupForItem.Location,
                            Active = meetupForItem.Active,
                        });
                    }

                    ViewBag.CalendarItems = scheduledMeetups;// list of calendar items
                    
                }
            }
            //var calendarItems = _db.Friendscrip_Meetups.Where(x => x.AccountID.Equals(accountId)).ToList();
            //ViewBag["CalendarItems"] = Json(calendarItems, JsonRequestBehavior.AllowGet); // list of calendar items
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        //Account settings 
        public ActionResult Settings()
        {
            try {
                string currentUserId = this.Session["ID"].ToString();

                var model = _db.Friendscrip_Accounts.Where(x => x.ID.Equals(currentUserId)).FirstOrDefault();
                return View(model);

            }
            catch (Exception e) {
                return RedirectToAction("Login");
            }
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Login()
        {
            ViewBag.Message = "Login Page";
            return View();
        }

        public ActionResult Confirm(string email, string ID)
        {
            ConfirmEmail emails = new ConfirmEmail
            {
                Email = email,
                ID = ID
            };
            ViewBag.Message = "Confirm Page";
            return View(emails);
        }

        [HttpPost]
        public ActionResult Confirm(ConfirmEmail emails)
        {
            if(emails.Email.Equals(emails.SameEmail))
            {
                var account = _db.Friendscrip_Accounts.Where(x => x.ID.Equals(emails.ID)).FirstOrDefault();
                if (account != null)
                {
                    if (account.Email.Equals(emails.Email))
                    {
                        // do nothing
                    }
                    else
                    {
                        account.Email = emails.Email;
                        _db.SaveChanges();
                    }
                }
                return RedirectToAction("Index");
            }
            return View(emails.Email, emails.ID);
        }

        public ActionResult Items()
        {
            ViewBag.Message = "Items Page";
            return View();
        }

        public ActionResult NewItems()
        {
            return View();
        }

        public ActionResult linkCalendar()
        {
            ViewBag.Message = "linkCalendar Page";
            return View();
        }
        public ActionResult LoginCalendar()
        {
            ViewBag.Message = "LoginCalendar Page";
            return View();
        }
    }
}
