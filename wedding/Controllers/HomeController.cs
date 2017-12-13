using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using wedding.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;



namespace wedding.Controllers
{
    public class HomeController : Controller
    {

        private MainContext _context;

        public HomeController(MainContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            // wrap the session validation around all of the methods in all the pages!!
            // for example if(user not in session{
            // return redirectToAvtion("Index")}else{
            // ALL THE OTHER SHIT ON THE PAGEEEE
            //}
            return View();
        }

        [HttpPost]
        [Route("Register")]
        public IActionResult Register(User user)
        {
            if (ModelState.IsValid)
            {
                // System.Console.WriteLine(registeredcheck);
                // System.Console.WriteLine("EMAILLL" + user.Email);
                // System.Console.WriteLine("THISSSSS****"+returnedid.id);

                System.Console.WriteLine(user.user_id);

                User registeredcheck = _context.user.SingleOrDefault(str => str.email == user.email);


                // System.Console.WriteLine("THE EMAILLL", registeredcheck.Email);
                // string email = registeredcheck.Email;
                if (registeredcheck == null)
                {
                    PasswordHasher<User> Hasher = new PasswordHasher<User>();
                    user.password = Hasher.HashPassword(user, user.password);
                    User NewPerson = new User

                    {
                        first_name = user.first_name,
                        last_name = user.last_name,
                        email = user.email,
                        password = user.password,

                    };

                    User theuser = _context.Add(NewPerson).Entity;
                    _context.SaveChanges();
                    System.Console.WriteLine("NEW PERSON", NewPerson.first_name);
                    ViewBag.Success = "You have been added to the database! Please log in now!";
                    return View("Index");

                }
                else
                {
                    System.Console.WriteLine("ALREADY IN THE DATABASE");
                    return View("Index");
                }
            }
            return View("Index");
        }

        [HttpPost]
        [Route("Login")]
        public IActionResult Login(LoginUser user)
        {
            User userfound = new User
            {
                first_name = "john",
                last_name = "doe",
                email = user.LogEmail,
                password = user.LogPassword,
            };

            User loggeduser = _context.user.SingleOrDefault(str => str.email == userfound.email);
            if (loggeduser == null)
            {
                ViewBag.loginerror = "Login failed, email and password did not match the information in the database. If you haven't registered please register first!";
                return View("Index");
            }
            else
            {
                PasswordHasher<User> Hasher = new PasswordHasher<User>();

                if (0 != Hasher.VerifyHashedPassword(loggeduser, loggeduser.password, userfound.password))
                {

                    HttpContext.Session.SetInt32("loggedperson", (int)loggeduser.user_id);

                    return RedirectToAction("LandingPage");
                }
                else
                {

                    ViewBag.loginerror = "Login failed, email and password did not match the information in the database. If you haven't registered please register first!";
                    return View("Index");
                }
            }
        }

        [HttpGet]
        [Route("dashboard")]

        public IActionResult LandingPage()
        {

            int? loggedperson = HttpContext.Session.GetInt32("loggedperson");
            if (loggedperson == null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                var onewedding = _context.weddingplanner.Include(w => w.Guests).ThenInclude(g => g.Guest).ToList();

                ViewBag.weddings = onewedding;
                
                System.Console.WriteLine("HEYYY" + loggedperson);
                User findtheperson = _context.user.SingleOrDefault(x => x.user_id == loggedperson);

                System.Console.WriteLine("FOUND PESON " + findtheperson.user_id);

                ViewBag.User = findtheperson;
                return View("About");


            }
        }





        [HttpGet]
        [Route("logout")]
        public IActionResult Logout()
        {


            HttpContext.Session.Clear();

            return RedirectToAction("Index");
        }
    }


}

//-------------------------------------------------------------------WEDDING STUFF



