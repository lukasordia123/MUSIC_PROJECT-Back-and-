using Microsoft.AspNetCore.Mvc;
using Models;
using Service.interfaces;

namespace Travel_More_Web_ui.Controllers
{
    public class Addritual : Controller
    {

        private readonly IDatabaseConnection _connection;
        //private readonly User _loginuser;
        public Addritual(IDatabaseConnection connection)
        {
            _connection = connection;
        }

        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> add(Posts posts) {

            if(ModelState.IsValid)
            {
                Posts posts1 = new()
                {
                    Title = posts.Title,
                    Location = posts.Location,
                    SearchAddress = posts.SearchAddress,
                    Text = posts.Text,
                    ImgUrl = posts.ImgUrl,
                    Datetime = posts.Datetime,

                };
            await _connection.InsertRitual(posts1);
                return RedirectToAction("homepage", "HomePage");
            }

            return View();
        }



    }
}

