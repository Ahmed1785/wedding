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
    public class WeddingController : Controller
    {

        private MainContext _context;
       
        public WeddingController(MainContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("weddingform")]
        public IActionResult Weddingform()
        {

            return View("Wedding");
        }

        [HttpPost]
        [Route("addwedding")]
        public IActionResult AddWedding(WeddingPlanner weddingdetails)
        {

            // if(ActiveUser == null)
            //     return RedirectToAction("Index", "Home");

            int? loggedperson = HttpContext.Session.GetInt32("loggedperson");

            // System.Console.WriteLine(int? loggedperson);
            if(ModelState.IsValid)
            
            {

            WeddingPlanner weddingplan = new WeddingPlanner
            {
                bride = weddingdetails.bride,
                groom = weddingdetails.groom,
                Date = weddingdetails.Date,
                Address = weddingdetails.Address,
                user_id = (int)loggedperson

            };
            
                _context.weddingplanner.Add(weddingplan);
                _context.SaveChanges();
                System.Console.WriteLine("USER____ID FOR WEDDING PERSON" + weddingplan.user_id);

                return RedirectToAction("LandingPage", "Home");
            }

            return View("Wedding");

            
        }

        [HttpGet]
        [Route("rsvp/{rsvpwedding_id}")]

        public IActionResult RSVP(int rsvpwedding_id)
        
        {
            int? loggedperson = HttpContext.Session.GetInt32("loggedperson");
            System.Console.WriteLine("LOGGESPERSON " + loggedperson);
            if (loggedperson == null){
                return RedirectToAction("Index", "Home");
            }else{
        
            
            
            RSVP rsvp = new RSVP 
            {                
            user_id = (int) loggedperson,
            wedding_id = rsvpwedding_id
            };

            RSVP reservation = _context.Add(rsvp).Entity;
            _context.SaveChanges();
            
            return RedirectToAction("LandingPage", "Home");
            }


        }

        [HttpGet]
        [Route("deletewedding/{rsvpwedding_id}")]

        public IActionResult deletewedding(int rsvpwedding_id)
        
        {
            int? loggedperson = HttpContext.Session.GetInt32("loggedperson");
            System.Console.WriteLine("LOGGESPERSON " + loggedperson);
            if (loggedperson == null){
                return RedirectToAction("Index", "Home");
            }else{
        
            
            
            var deleteawedding = _context.weddingplanner.Include(w => w.Guests).ThenInclude(g => g.Guest).Where(w => w.wedding_id == rsvpwedding_id).SingleOrDefault();
            _context.Remove(deleteawedding);
            _context.SaveChanges();
            return RedirectToAction("LandingPage", "Home");
            }

        }

        [HttpGet]
        [Route("UNRSVP/{item_id}")]

        public IActionResult UNRSVP(int item_id)
        
        {
            int? loggedperson = HttpContext.Session.GetInt32("loggedperson");
            System.Console.WriteLine("LOGGESPERSON " + loggedperson);
            if (loggedperson == null){
                return RedirectToAction("Index", "Home");
            }else{
        
            
            
            var unrsvp = _context.RSVP.Where(w => w.rsvp_id == item_id)
                                        .Where(g => g.user_id == loggedperson)
                                        .SingleOrDefault();
            _context.RSVP.Remove(unrsvp);
            _context.SaveChanges();
            return RedirectToAction("LandingPage", "Home");
            }

        }

        [HttpGet]
        [Route("showawedding/{rsvpwedding_id}")]

         public IActionResult Show(int rsvpwedding_id)
        {
            if(HttpContext.Session.GetInt32("loggedperson") == null)
               return RedirectToAction("Index");
            
            
            var onewedding = _context.weddingplanner.Include(w => w.Guests).Include(w => w.Guests).ThenInclude(w => w.Guest).Where(w => w.wedding_id == rsvpwedding_id).SingleOrDefault();
            ViewBag.onewedding = onewedding;

            return View("ShowWedding");
        }

        

        [HttpGet]

        [Route("showawedding/logout")]
        public IActionResult Logout()
        {

            return RedirectToAction("Logout", "Home");
        }
        
    }
    }
    