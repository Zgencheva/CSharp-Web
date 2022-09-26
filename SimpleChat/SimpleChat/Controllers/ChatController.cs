using Microsoft.AspNetCore.Mvc;
using SimpleChat.Models;

namespace SimpleChat.Controllers
{
    public class ChatController : Controller
    {
        private static List<KeyValuePair<string, string>> messages =
            new List<KeyValuePair<string, string>>();

        public IActionResult Show() 
        {
            if (messages.Count() <1)
            {
                return View(new ChatViewModel());
            }

            var chatModel = new ChatViewModel()
            {
                Messages = messages
                .Select(x => new MessageViewModel()
                {
                    Sender = x.Key,
                    MessageText = x.Value,
                })
                .ToList()
            };
            return View(chatModel);
        }

        [HttpPost]
        public IActionResult Send(ChatViewModel chat)
        {
            var newMessage = chat.CurrentMessage;
            messages.Add(new KeyValuePair<string, string>(newMessage.Sender, newMessage.MessageText));
            return RedirectToAction(nameof(Show));
        }
    }
}
