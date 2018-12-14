using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ActivityCenter.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;

namespace ActivityCenter.Controllers
{
    public class ACController : Controller
    {
        private ActivityCenterContext dbContext;

        public ACController(ActivityCenterContext context)
        {
            dbContext = context;
        }

// -----ActivityCenter main page----------

        [HttpGet("/Home")]
        public IActionResult Home()
        {
            if(HttpContext.Session.GetInt32("UserId") == null) 
            {
                return RedirectToAction("Index", "LoginReg");
            }
            
            ViewBag.LoggedInUserId = HttpContext.Session.GetInt32("UserId");
            ViewBag.LoggedInUserName = HttpContext.Session.GetString("UserName");
            
            List<Event> allEvents = dbContext.Events
            .Include(e => e.Participants)
            .Include(e => e.Coordinator)
            .OrderBy(e => e.Date)
            .OrderBy(e => e.Time)
            .ToList();
            
            ViewBag.allEvents = allEvents;

            return View();
        }

// --------show form for new event ------------
        [HttpGet("/New")]
        public IActionResult AddEvent()
        {
            if(HttpContext.Session.GetInt32("UserId") == null) 
            {
                return RedirectToAction("Index", "LoginReg");
            }
            ViewBag.UserId = HttpContext.Session.GetInt32("UserId");

            

            return View();
        }
        
// ----------create new  event-------------
        [HttpPost("/Create")]
        public IActionResult Create(Event newEvent)
        {
            if(ModelState.IsValid)
            {
                if (newEvent.Date < DateTime.Now)
                {
                    ModelState.AddModelError("Date", "Date must be in the future");
                    ViewBag.UserId = HttpContext.Session.GetInt32("UserId");
                    return View("AddEvent");
                }

                else
                {
                    dbContext.Add(newEvent);
                    dbContext.SaveChanges();
                
                    return RedirectToAction("Details", new { id = newEvent.EventId });
                }
            }

            else
            {
                System.Console.WriteLine("********Error Count*********");
                System.Console.WriteLine(ModelState.ErrorCount);
                System.Console.WriteLine("*****************");
        
                ViewBag.UserId = HttpContext.Session.GetInt32("UserId");
                return View("AddEvent");
            }
        }

// ----------event detail page ----------------
        [HttpGet("/activity/{id}")]
        public IActionResult Details(int id)
        {
            if(HttpContext.Session.GetInt32("UserId") == null) 
            {
                return RedirectToAction("Index", "LoginReg");
            }
            
            Event EventDetail = dbContext.Events
            .Where(e => e.EventId == id)
            .Include(e => e.Coordinator)
            .Include(e => e.Participants)
            .ThenInclude(r => r.User)
            .FirstOrDefault();

            ViewBag.EventDetail =EventDetail;

            ViewBag.LoggedInUserId = HttpContext.Session.GetInt32("UserId");

            if(EventDetail.Participants.Any(p => p.UserId ==ViewBag.LoggedInUserId)){
                ViewBag.IsParticipant = true;
            }
            else{ViewBag.IsParticipant = false; }
            
            return View();
        }

// ---------delete Event--------------
        [HttpGet("/deleteEvent/{id}")]
        public IActionResult deleteEvent(int id)
        {
            if(HttpContext.Session.GetInt32("UserId") == null) 
            {
                return RedirectToAction("Index", "LoginReg");
            }
            

            Event deletingEvent = dbContext.Events
            .Where(r => r.EventId == id)
            .FirstOrDefault();

            if(HttpContext.Session.GetInt32("UserId") != deletingEvent.UserId) 
            {
                return RedirectToAction("Home");
            }

            dbContext.Events.Remove(deletingEvent);
            dbContext.SaveChanges();

            return RedirectToAction("Home");
        }


// -----------Add new Participant----------
        [HttpPost("/addParticipant")]
        public IActionResult addParticipant(Participant newParticipant)
        {
            dbContext.Add(newParticipant);
            dbContext.SaveChanges();
            
            return RedirectToAction("Home");
        } 

// ----------delete Participant----------
        [HttpGet("/deleteParticipant/{id}")]
        public IActionResult deleteParticipant(int id)
        {
            if(HttpContext.Session.GetInt32("UserId") == null) 
            {
                return RedirectToAction("Index", "LoginReg");
            }
            Participant deletingParticipant = dbContext.Participants
            .Where(r => r.ParticipantId == id && r.UserId == HttpContext.Session.GetInt32("UserId"))
            .FirstOrDefault();

            dbContext.Participants.Remove(deletingParticipant);
            dbContext.SaveChanges();

            return RedirectToAction("Home");
        }

    }
}
