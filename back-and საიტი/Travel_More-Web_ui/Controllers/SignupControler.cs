using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR.Protocol;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using Models;
using Service.interfaces;

namespace Travel_More_Web_ui.Controllers
{
    public class SignupControler : Controller
    {
        private readonly IDatabaseConnection _connection;
        public SignupControler(IDatabaseConnection connection)
        {
            _connection = connection;
        }
        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(User model)
        {
            if (ModelState.IsValid)
            {

                var allUsers = await _connection.GetAllUSers();

                if (!allUsers.Any(user => user.Email.Equals(model.Email)))
                {
                    User newUser = new()
                    {
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        Email = model.Email,
                        Password = model.Password,
                    };

                    await _connection.RegisterUser(newUser);


                }
                //model.FirstName = string.Empty;
                //model.LastName = string.Empty;
                //model.Email = string.Empty;
                //model.Password = string.Empty;
                RedirectToAction("LogIn", "Login");
            }
                return View();
        }
    } 
}
