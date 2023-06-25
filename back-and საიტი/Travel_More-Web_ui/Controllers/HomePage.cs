using Microsoft.AspNetCore.Mvc;
using Service.interfaces;

namespace Travel_More_Web_ui.Controllers
{
    public class HomePage : Controller
    {
        private IDatabaseConnection _conection;

        public HomePage(IDatabaseConnection conection)
        {
            _conection = conection;
        }
        public async  Task<IActionResult> homepage()
        {
            var GetAllRitual = await _conection.GetAllRituals();
            return View(GetAllRitual);
        }
    }
}
