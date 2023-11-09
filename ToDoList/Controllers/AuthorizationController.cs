using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net.Mail;
using ToDoList.Models;
using Domain.Entity;
using Service;

namespace ToDoList.Controllers
{
    public class AuthorizationController : Controller
    {
        private readonly ILogger<AuthorizationController> _logger;

        public AuthorizationController(ILogger<AuthorizationController> logger)
        {
            _logger = logger;
        }

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Registration()
        {

            return View();
        }

        public IActionResult SignIn(Users users)
        {
            UserService.Add(users);
            return RedirectToAction("Login");
        }

        public IActionResult Restore()
        {
            Users users = new Users();
            return View(users);
        }

        public IActionResult RestorePass(Users users)
        {
            Users restore = UserService.GetEmail(users.Email);
            if (restore != null)
            {
                using (StreamReader sr = new StreamReader("wwwroot/Message/RestorePass.txt"))
                {
                    string mess = sr.ReadToEnd();
                    mess = mess.Replace("[Имя пользователя]", restore.Name);
                    mess = mess.Replace("[Пароль]", restore.Password);

                    MailMessage mail = Mail.CreateMessage(restore.Email, "Востановление пароля", mess);
                    Mail.SendMail(mail);
                    return View("Login");
                }
            }
            return View("Restore");
        }



        public IActionResult Error()
        {
            return View();
        }
    }
}