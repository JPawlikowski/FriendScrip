using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Mail;

namespace FriendScrip.Controllers
{
    public class EmailController : Controller
    {
        // LINKS NEED TO BE IMPLEMENTED IN EMAILS
        // GET: Email
        public ActionResult Index()
        {
            return View();
        }
        //TEST method to send emails
        public JsonResult SendTestEmail()
        {
            string reciever = "amella2008@hotmail.com";
            string sender = "anthonymella@northfieldsoftware.ca";
            string buyerName = "tony";
            string date = "time";
            string price = "40";
            string time = "When things happen";
            string address = "main st";
            string meetupTime = "sometime";



            MailMessage message = new MailMessage(sender, reciever);
            SmtpClient client = new SmtpClient();
            client.Port = 25;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.Credentials = new System.Net.NetworkCredential("apikey", "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");
            client.Host = "smtp.sendgrid.net";

            
            var stringage = string.Format("");
            message.Subject = "Test email for friendscrip";
            message.Body = stringage;
            client.Send(message);

            return Json("", JsonRequestBehavior.AllowGet);
        }

        

        public JsonResult BuyerChoosesTimeEmail(string sellerEmail, string buyerEmail, string buyerName, string price, string date, string time)
        {
            //POTENTIALLY NEED TO INCLUDE LINK AS INPUT VARIABLE
            MailMessage message = new MailMessage(sellerEmail, buyerEmail);
            SmtpClient client = new SmtpClient();
            // If the port gives you troubles, switch to "25, 587" or "465"
            client.Port = 25;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.Credentials = new System.Net.NetworkCredential("apikey", "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");
            client.Host = "smtp.sendgrid.net";
            var stringage = string.Format("{0} has offered ${1} and asked for a pickup on {2}, between {3}. To accept, counter, or decline this offer and pickup, click this link to return to your ShowsUp dashboard and make your choice. Once you have accepted the buyer’s offer and pickup time - slot, the buyer is obligated to show up for the appointment within the 15 minute time - slot, or cancel at least one hour before the scheduled pickup.Should the buyer not do either, return to the calendar dashboard and click on “withhold appointment deposit”. By doing this you are ensuring the buyer who disrespects your time will not have the opportunity to disrespect the time of any other sellers.\n Thanks for using ShowsUp to manage your sales.", buyerName, price, date, time);

            message.Subject = "ShowsUp - New Meetup Request!";
            message.Body = stringage;
            client.Send(message);

            return Json("", JsonRequestBehavior.AllowGet);
        }

        public JsonResult AcceptEmail(string sellerEmail, string buyerEmail, string address, string meetupTime)
        {
            MailMessage message = new MailMessage(sellerEmail, buyerEmail);
            SmtpClient client = new SmtpClient();
            // If the port gives you troubles, switch to "25, 587" or "465"
            client.Port = 25;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.Credentials = new System.Net.NetworkCredential("apikey", "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");
            client.Host = "smtp.sendgrid.net";
            var stringage = string.Format("The seller has accepted your price offer and time-slot for pickup. Please attend your appointment at {0} on day of {1}. Important: If you have sold in local online marketplaces before you likely know that many people will never show up for their appointment. As part of booking the time slot the ShowsUp software automatically attached a refundable $10 deposit for free on your behalf to incentivize showing up, or at least formally cancelling your appointment.If you are unable to attend the pickup for any reason, please cancel your appointment at least one hour before with the seller directly, or from this link. If you do not attend the appointment, and do not cancel with the seller or system, your deposit will be lost and you will be unable to book with any other sellers using the system until you pay $10 to buy an additional deposit. TL; DR: Can’t make your appointment? Cancel at least an hour before the scheduled pickup and no penalty. Don’t show or cancel? You will lose your refundable $10 deposit and it will cost you $10 to book your next pickup appointment.", address, meetupTime);

            message.Subject = "ShowsUp - Seller has accepted your offer";
            message.Body = stringage;
            client.Send(message);

            return Json("", JsonRequestBehavior.AllowGet);
        }

        public JsonResult DeclineEmail(string sellerEmail, string buyerEmail)
        {
            MailMessage message = new MailMessage(sellerEmail, buyerEmail);
            SmtpClient client = new SmtpClient();
            // If the port gives you troubles, switch to "25, 587" or "465"
            client.Port = 25;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.Credentials = new System.Net.NetworkCredential("apikey", "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");
            client.Host = "smtp.sendgrid.net";
            // Delete if waitlist gets implemented
            var stringage = string.Format("Thanks for your offer. The seller has successfully negotiated a deal with another buyer. If you would like the option to restart negotiation should the first buyer fall through, join the wait list by clicking on this link. Thanks again for your offer.");

            message.Subject = "ShowsUp - Seller has declined your offer";
            message.Body = stringage;
            client.Send(message);

            return Json("", JsonRequestBehavior.AllowGet);
        }

        public JsonResult CounterPriceEmail(string newPrice, string sellerEmail, string buyerEmail)
        {
            MailMessage message = new MailMessage(sellerEmail, buyerEmail);
            SmtpClient client = new SmtpClient();
            // If the port gives you troubles, switch to "25, 587" or "465"
            client.Port = 25;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.Credentials = new System.Net.NetworkCredential("apikey", "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");
            client.Host = "smtp.sendgrid.net";
            var stringage = string.Format("The seller has countered your offer with ${0}. To accept, counter, decline, click this link and make your choice. Seller may be simultaneously negotiating with multiple buyers, so it’s in your best interests to make your choice as quickly as possible to not lose the deal.", newPrice);

            message.Subject = "ShowsUp - Seller has countered your offer";
            message.Body = stringage;
            client.Send(message);

            return Json("", JsonRequestBehavior.AllowGet);
        }

        public JsonResult CounterTimeEmail(string sellerEmail, string buyerEmail, string date)
        {
            MailMessage message = new MailMessage(sellerEmail, buyerEmail);
            SmtpClient client = new SmtpClient();
            // If the port gives you troubles, switch to "25, 587" or "465"
            client.Port = 25;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.Credentials = new System.Net.NetworkCredential("apikey", "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");
            client.Host = "smtp.sendgrid.net";
            var stringage = string.Format("Thanks for choosing a time-slot for pickup. The seller has accepted your price but has countered your time-slot with ${0}. To accept, counter, or decline this pickup, click this link to return to your ShowsUp dashboard and make your choice. Seller may be simultaneously negotiating with multiple buyers, so it is in your best interests to make your choice as quickly as possible to not lose the deal.", date);

            message.Subject = "ShowsUp - Seller has countered your offer";
            message.Body = stringage;
            client.Send(message);

            return Json("", JsonRequestBehavior.AllowGet);
        }


        //this method has to be modified to take in the link to the waitlist, if the waitlist gets implemented
        public JsonResult BuyerCancelEmail(string sellerEmail, string buyerEmail)
        {
            MailMessage message = new MailMessage(sellerEmail, buyerEmail);
            SmtpClient client = new SmtpClient();
            // If the port gives you troubles, switch to "25, 587" or "465"
            client.Port = 25;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.Credentials = new System.Net.NetworkCredential("apikey", "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");
            client.Host = "smtp.sendgrid.net";
            var stringage = string.Format("Sorry, but the pickup appointment has been cancelled by the buyer. If you would like to notify the wait list click this link to return to your dashboard and press the “wait list” button to notify them that the item is available again and that they can make an offer. Thanks for using ShowsUp to manage your sales.");

            message.Subject = "ShowsUp - Meetup Cancellation";
            message.Body = stringage;
            client.Send(message);

            return Json("", JsonRequestBehavior.AllowGet);
        }

        public JsonResult SellerCancellationEmail(string sellerEmail, string buyerEmail)
        {
            MailMessage message = new MailMessage(sellerEmail, buyerEmail);
            SmtpClient client = new SmtpClient();
            // If the port gives you troubles, switch to "25, 587" or "465"
            client.Port = 25;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.Credentials = new System.Net.NetworkCredential("apikey", "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");
            client.Host = "smtp.sendgrid.net";
            var stringage = string.Format("Sorry, but the pickup appointment has been cancelled by the seller. Your time-slot deposit has been returned to your account. Thanks for using ShowsUp. Happy treasure hunting.");

            message.Subject = "ShowsUp - Meetup Cancellation";
            message.Body = stringage;
            client.Send(message);

            return Json("", JsonRequestBehavior.AllowGet);
        }
    }
}
