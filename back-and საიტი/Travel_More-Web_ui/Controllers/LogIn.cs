using Microsoft.AspNetCore.Mvc;
using Models;
using Service;
using Service.interfaces;

namespace Travel_More_Web_ui.Controllers
{

    public class LogIn : Controller
    {
        private readonly IDatabaseConnection _connection;
        public LogIn(IDatabaseConnection connection)
        {
            _connection = connection;
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(User passedModel)
        {
            try
            {
                    List<User> allUsers = await _connection.GetAllUSers();
                    User userThatLogsIn = allUsers.FirstOrDefault(user => user.Email.Equals(passedModel.Email, StringComparison.OrdinalIgnoreCase));
                    userThatLogsIn.Password = passedModel.Password;

                    bool passwordIsCorrect = GlobalConfig.VerifyPassword(userThatLogsIn.Password, userThatLogsIn.PasswordHash, userThatLogsIn.PasswordSalt);

                if (passwordIsCorrect)
                {
                    return RedirectToAction("homepage", "HomePage");
                }
            }
            catch (Exception)
            {
                throw;
            }
           return View();
        }
    }
}
