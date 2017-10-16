using FriendScrip.Models;
using FriendScrip.Models.Repositorys;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FriendScrip.Controllers
{
    public class DataController : Controller
    {
        // GET: Data
        //public ActionResult Index()
        //{
        //    return View();
        //}

        private ScripEntities _db = new ScripEntities(); // connection to database

        public JsonResult AddNewItem(Friendscrip_Items newItem)
        {
            try
            {
                _db.Entry(newItem).State = EntityState.Added;
                _db.SaveChanges();
                // return newItem instead of "Success"
                return Json("Success", JsonRequestBehavior.AllowGet);
            } catch(Exception e)
            {
                return Json("Failure", JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult EditItem(Friendscrip_Items updatedItem)
        {
            try
            {
                _db.Entry(updatedItem).State = EntityState.Modified;
                _db.SaveChanges();

                //catch (Exception e)
                //{
                //    _db.Entry(updatedItem).State = EntityState.Added;
                //    _db.SaveChanges();
                //}
                return Json("Success", JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json("Failure", JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult RemoveItem(Friendscrip_Items item)
        {
            try
            {
                _db.Entry(item).State = EntityState.Deleted;
                _db.SaveChanges();
                return Json("Success", JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json("Failure", JsonRequestBehavior.AllowGet);
            }

        }

        public JsonResult GetItems(string uid)
        {
            try
            {
                var result = DatabaseFunctions.GetItems(uid);
                var resultList = new List<Items>();
                foreach (var item in result)
                {
                    resultList.Add(new Items
                    {
                        AccountID = item.AccountID,
                        Condition = item.Condition,
                        Description = item.Description,
                        ID = item.ID,
                        ItemName = item.ItemName,
                        MainIntersection = item.MainIntersection,
                        PickupLocation = item.PickupLocation,
                    });
                }

                return Json(resultList, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json("Failure", JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult GetMeetups(int? itemID)
        {

            var scheduledMeetups = new List<Meetups>();

            if (itemID == null)
            {
                var userId = this.Session["ID"].ToString();
                var account = _db.Friendscrip_Accounts.Where(x => x.ID == userId).FirstOrDefault();


                if (account != null)
                {
                    var itemsForSale = account.Friendscrip_Items.ToList();

                    foreach (var item in itemsForSale)
                    {
                        var meetupForItem = item.Friendscrip_Meetups.Where(x => x.Active).FirstOrDefault();

                        if (meetupForItem == null) { continue; }
                        // convert to different type
                        scheduledMeetups.Add(new Meetups
                        {
                            ID = meetupForItem.ID,
                            ItemID = meetupForItem.ItemID,
                            SellerId = meetupForItem.Friendscrip_Items.AccountID,
                            BuyerID = meetupForItem.BuyerID,
                            Title = meetupForItem.Title,
                            StartTime = meetupForItem.StartTime.ToString(),
                            Price = meetupForItem.Price,
                            Location = meetupForItem.Location,
                            Active = meetupForItem.Active,
                        });
                    }
                }
            }

            return Json(scheduledMeetups, JsonRequestBehavior.AllowGet);
        }

        public JsonResult CancelMeetup(int MeetingID, string cancellingUserID)
        {
            var meeting = _db.Friendscrip_Meetups.Where(x => x.ID == MeetingID).FirstOrDefault();

            var buyer = _db.Friendscrip_Accounts.Where(x => x.ID.Equals(cancellingUserID)).FirstOrDefault();

            var seller = meeting.Friendscrip_Accounts;

            if(cancellingUserID == buyer.ID) // cancelled by buyer
            {
                meeting.Active = false;
                buyer.Friendscrip += 10;
                seller.Friendscrip += 10;
                _db.SaveChanges();
            }
            else if (cancellingUserID == seller.ID) // cancelled by seller
            {
                meeting.Active = false;
                buyer.Friendscrip += 10;
                seller.Friendscrip += 10;
                _db.SaveChanges();
            }
            else
            {
                // do nothing
            }


            return Json("Success", JsonRequestBehavior.AllowGet);
        }

        //public ActionResult AddAccount()
        //{
        //    var newAccount = new Friendscrip_Accounts();
        //    return View(newAccount);
        //}

        //[HttpPost]
        //public ActionResult AddAccount(Friendscrip_Accounts accountToAdd)
        //{
        //    var exists = _db.Friendscrip_Accounts.Where(x => x.ID == accountToAdd.ID).Any();

        //    if(!exists)
        //    {
        //        _db.Entry(accountToAdd).State = EntityState.Added;
        //    }

        //    _db.SaveChanges();

        //    return RedirectToAction("Index", "");
        //}

        public JsonResult AddAccount(Friendscrip_Accounts accountInfo)
        {
            try
            {
                var existingAccount = _db.Friendscrip_Accounts.Where(x => x.ID.Equals(accountInfo.ID)).FirstOrDefault();
                if (existingAccount == null)
                {
                    _db.Entry(accountInfo).State = EntityState.Added;
                    _db.SaveChanges();
                    ConfirmEmail emailConfirmationModel = new ConfirmEmail
                    {
                        Email = accountInfo.Email,
                        ID = accountInfo.ID
                    };
                    return Json(emailConfirmationModel, JsonRequestBehavior.AllowGet);
                }
                // SET SESSION VALUES #LITSQUADFAM
                Session["ID"] = accountInfo.ID;
                Session["Email"] = accountInfo.Email;
                Session["Friendscrip"] = accountInfo.Friendscrip;
                Session["Name"] = accountInfo.Name;

                return Json("AlreadyInDB", JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json("Failure", JsonRequestBehavior.AllowGet);
            }
        }

        // either bind from passed in database model or ensure that view always passes db model type
        public JsonResult UpdateAccountInfo(Friendscrip_Accounts updatedInfo)
        {
            try
            {
                _db.Entry(updatedInfo).State = EntityState.Modified;

                _db.SaveChanges();

                return Json("Success", JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                // we need a place to log our errors to
                //ApplicationLogger.LogError.
                return Json("Failure", JsonRequestBehavior.AllowGet);
            }
            
        }

        public JsonResult RemoveAccount(Friendscrip_Accounts account)
        {
            try
            {
                _db.Entry(account).State = EntityState.Deleted;
                _db.SaveChanges();
                return Json("Success", JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json("Failure", JsonRequestBehavior.AllowGet);
            }

        }

        //Method adds friendscrip, called when the user enters an amount of friendscrip to purchase, and completes payment using the stripe API
        public JsonResult AddFriendscrip(string accountId, int amount)
        {
            try
            {
                var account = _db.Friendscrip_Accounts.Where(x => x.ID.Equals(accountId)).FirstOrDefault();

                if (account != null)
                {
                    account.Friendscrip = account.Friendscrip + amount;

                    _db.Entry(account).State = EntityState.Modified;
                    _db.SaveChanges();
                    Session["Friendscrip"] = account.Friendscrip;
                    return Json("Success", JsonRequestBehavior.AllowGet);
                }
                return Json("NotFound", JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json("Faulure", JsonRequestBehavior.AllowGet);
            }
        }

    }
}
