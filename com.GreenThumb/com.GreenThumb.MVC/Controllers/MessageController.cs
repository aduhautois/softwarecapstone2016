using com.GreenThumb.BusinessLogic;
using com.GreenThumb.MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using com.GreenThumb.BusinessObjects;

namespace com.GreenThumb.MVC.Controllers
{
    [Authorize]
    public class MessageController : Controller
    {
        private WebMessageManager _messageManager = new WebMessageManager();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult MessageCount()
        {
            int messageCount;
            messageCount = _messageManager.GetUnreadMessageCount(User.Identity.GetUserName());
            return View(messageCount);
        }

        public ActionResult Inbox()
        {
            var username = User.Identity.GetUserName();
            var messages = GetMessages(username);
            messages = messages.Where(m => m.MessageReceiver.ToUpper() == username.ToUpper()
                && m.RecieverDeleted == false).OrderByDescending(m => m.MessageDate).ToList();

            List<MessageViewModelInbox> inbox = CreateMessageInbox(messages);

            return View(inbox);
        }

        [HttpPost]
        public ActionResult Inbox(List<MessageViewModelInbox> model)
        {
            if (model == null)
            {
                return RedirectToAction("Index", "Message");
            }
            var username = User.Identity.GetUserName();
            foreach (var message in model)
            {
                if (message.Delete == true)
                {
                    _messageManager.EditMessageDeletedReceiver(username, message.MessageID);
                }
            }

            return RedirectToAction("Inbox", "Message");
        }

        public ActionResult Outbox()
        {
            var username = User.Identity.GetUserName();
            var messages = GetMessages(username);
            messages = messages.Where(m => m.MessageSender.ToUpper() == username.ToUpper()
                && m.SenderDeleted == false).OrderByDescending(m => m.MessageDate).ToList();

            List<MessageViewModelOutbox> outbox = CreateMessageOutbox(messages);
            return View(outbox);
        }

        [HttpPost]
        public ActionResult Outbox(List<MessageViewModelOutbox> model)
        {
            if (model == null)
            {
                return RedirectToAction("Index", "Message");
            }
            var username = User.Identity.GetUserName();
            foreach (var message in model)
            {
                if (message.Delete == true)
                {
                    _messageManager.EditMessageDeletedReceiver(username, message.MessageID);
                }
            }
            return RedirectToAction("Inbox", "Message");
        }

        public ActionResult Compose()
        {
            MessageViewModelCompose message = new MessageViewModelCompose();
            message.UserList = GetUserList();
            return View(message);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Compose(MessageViewModelCompose message)
        {
            if (!ModelState.IsValid)
            {
                return View(message);
            }

            var username = User.Identity.GetUserName();
            if (!_messageManager.SendMessage(message.Content, message.Subject, username, message.Receiver))
            {
                ModelState.AddModelError("Failed", "Could not send message");
            }

            return RedirectToAction("Index", "Message");
        }

        #region Helper Methods
        private List<Message> GetMessages(string username)
        {
            List<Message> messages = _messageManager.GetUserMessages(username);
            return messages;
        }

        /// <summary>
        /// Ryan Taylor
        /// Created: 04/26/2016
        /// 
        /// Helper method to create the list of messages that the Inbox View will
        /// be based on.
        /// </summary>
        /// <param name="messages"></param>
        /// <returns></returns>
        private List<MessageViewModelInbox> CreateMessageInbox(List<Message> messages)
        {
            List<MessageViewModelInbox> inbox = new List<MessageViewModelInbox>();

            foreach (var message in messages)
            {
                inbox.Add(new MessageViewModelInbox()
                    {
                        MessageID = message.MessageID,
                        Content = message.MessageContent,
                        Subject = message.MessageSubject,
                        Sender = message.MessageSender,
                        Date = message.MessageDate.ToShortDateString(),
                        Unread = message.Unread
                    });
            }

            return inbox;
        }

        /// <summary>
        /// Ryan Taylor
        /// Created: 04/26/2016
        /// 
        /// Helper method to create the list of messages that the Outbox View will
        /// be based on.
        /// </summary>
        /// <param name="messages"></param>
        /// <returns></returns>
        private List<MessageViewModelOutbox> CreateMessageOutbox(List<Message> messages)
        {
            List<MessageViewModelOutbox> outbox = new List<MessageViewModelOutbox>();

            foreach (var message in messages)
            {
                outbox.Add(new MessageViewModelOutbox()
                {
                    MessageID = message.MessageID,
                    Content = message.MessageContent,
                    Subject = message.MessageSubject,
                    Receiver = message.MessageReceiver,
                    Date = message.MessageDate.ToShortDateString()
                });
            }

            return outbox;
        }

        private List<MessageViewModelUser> GetUserList()
        {
            List<MessageViewModelUser> viewList = new List<MessageViewModelUser>();
            List<User> userList = _messageManager.GetUserNames();
            if (userList != null)
            {
                foreach (var user in userList)
                {
                    viewList.Add(new MessageViewModelUser()
                        {
                            Username = user.UserName,
                            FirstName = user.FirstName,
                            LastName = user.LastName
                        });
                }
            }

            return viewList;
        }

        #endregion

    }
}
