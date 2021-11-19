using AirlineReservationSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using static AirlineReservationSystem.Models.PagedResultBase;

namespace AirlineReservationSystem.Controllers
{
    public class AdminController : Controller
    {
        private readonly DBAirlineReservationContext db;
        public AdminController(DBAirlineReservationContext context)
        {
            db = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        #region Add Flight
        public IActionResult AddFlight()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddFlight(TblFlightMaster tblFlightMaster)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.TblFlightMasters.Add(tblFlightMaster);
                    db.SaveChanges();
                    return RedirectToAction("DisplayFlight");
                }
                return View();
            }
            catch(Exception e)
            {
                ViewBag.msg = e.Message;
                return View();
            }
        }
        #endregion
        #region Display Flight
        public IActionResult DisplayFlight(string searching, int p = 1)
        {
            try
            {
                
                //ViewBag.CurrentFilter = searching;
                PagedResult<TblFlightMaster>  FlightMaster = db.TblFlightMasters.Where(x => x.Fname.Contains(searching) || searching == null).GetPaged(p, 3);
                
               
                return View(FlightMaster);
            }
            catch(Exception e)
            {
                ViewBag.msg = e.Message;
                return View();
            }
        }
        public IActionResult DisplayFlightDetails(string searching,string sortOrder, int p=1)
        {
            
            ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";
            
            List<FlightDetails> flightDetails1 = new List<FlightDetails>();
            try
            {
                //tblFlightDetails = db.TblFlightDetails.ToList();
                var tblFlightDetail = (from f in db.TblFlightDetails join m in db.TblFlightMasters
                                   on f.Flightid equals m.Flightid where m.Fname.Contains(searching) || searching == null
                                    select f);
                switch (sortOrder)
                {
                    case "Date":
                        tblFlightDetail = tblFlightDetail.OrderBy(s => s.DepatureDate);
                        break;
                    case "date_desc":
                        tblFlightDetail = tblFlightDetail.OrderByDescending(s => s.DepatureDate);
                        break;
                }
                List<TblFlightDetail> tblFlightDetails = tblFlightDetail.ToList();
                
                foreach (var item in tblFlightDetails)
                {
                    FlightDetails flightDetails = new FlightDetails();
                    var FlightName = db.TblFlightMasters.Where(i => i.Flightid == item.Flightid).Select(n => n.Fname).FirstOrDefault();
                    flightDetails.Detailsid = item.FlightDetailsId;
                    flightDetails.FlightName = FlightName;
                    flightDetails.Source = item.Source;
                    flightDetails.Destination = item.Destination;
                    flightDetails.DepartureDate = item.DepatureDate.ToString().Substring(0, 10);
                    flightDetails.DepartureTime = item.DepatureTime.ToString();
                    flightDetails.AvailableEconomySeats = item.AvailableEconomySeats;
                    flightDetails.EconomyClassFare = item.EconomyclassFare;
                    flightDetails.AvailableBusinessSeats = item.AvailableBusinessSeats;
                    flightDetails.BusinessClassFare = item.BusinessclassFare;
                   
                    flightDetails1.Add(flightDetails);
                }
                PagedResult<FlightDetails> pagedResult = (PagedResult<FlightDetails>)flightDetails1.AsQueryable().GetPaged(p, 3);


                return View(pagedResult);
            }
            catch(Exception e)
            {
                ViewBag.msg = e.Message;
                return View(flightDetails1);
            }

        }
        #endregion
        #region Get Flight Details By id
        public IActionResult GetFlightDetails(int id, int p=1)
        {
            List<TblFlightDetail> tblFlightDetails = new List<TblFlightDetail>();
            try
            {
                ViewBag.Flightid = id;
                var FlightDetailid = db.TblFlightDetails.Where(i => i.Flightid == id).Select(i => i.FlightDetailsId).ToList();
                foreach (var item in FlightDetailid)
                {
                    TblFlightDetail tblFlightDetail = db.TblFlightDetails.Find(item);
                    tblFlightDetails.Add(tblFlightDetail);
                }
                PagedResult<TblFlightDetail> pagedResult = (PagedResult<TblFlightDetail>)tblFlightDetails.AsQueryable().GetPaged(p, 3);
                return View(pagedResult);

            }
            catch(Exception e)
            {
                ViewBag.msg = e.Message;
                return View(tblFlightDetails);
            }
        }
        #endregion
        #region Add/Delete/Edit Flight Details
        public IActionResult AddFlightDetails(int id)
        {
            ViewBag.Flightid = id;
            TempData["Flightid"] = id;
            return View();
        }
        [HttpPost]
        public IActionResult AddFlightDetails(TblFlightDetail tblFlightDetail)
        {
            ViewBag.Flightid = TempData["Flightid"];
            TempData.Keep();
            if (ModelState.IsValid)
            {
                try
                {
                    db.TblFlightDetails.Add(tblFlightDetail);
                    db.SaveChanges();
                    return RedirectToAction("DisplayFlightDetails");
                }
                catch(Exception e)
                {
                    ViewBag.msg = e.Message;
                    return View();
                }
            }
            return View();
        }
        public IActionResult EditFlightDetails(int id)
        {
            var fid = db.TblFlightDetails.Where(i => i.FlightDetailsId == id).Select(i => i.Flightid).FirstOrDefault();
            ViewBag.Flightid = fid;
            TempData["fid"] = fid;
            TblFlightDetail tblFlightDetail = db.TblFlightDetails.Find(id);
            return View(tblFlightDetail);
        }
        [HttpPost]
        public IActionResult EditFlightDetails(TblFlightDetail tblFlightDetail)
        {
            ViewBag.Flightid = TempData["fid"];
            if (ModelState.IsValid)
            {
                try
                {
                    db.Entry(tblFlightDetail).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("DisplayFlightDetails");
                }
                catch
                {
                    ViewBag.msg = "An error occurred while processing your request!!!!";
                    return View();
                }
            }
            return View();
        }
        public IActionResult DeleteFlightDetails(int id)
        {
            Delete(id);

            try
            {
                TblFlightDetail tblFlightDetail = db.TblFlightDetails.Find(id);
                db.TblFlightDetails.Remove(tblFlightDetail);
                db.SaveChanges();
            }
            catch(Exception e)
            {
                ViewBag.msg = e.Message;
                return RedirectToAction("DisplayFlightDetails");
            }
            return RedirectToAction("DisplayFlightDetails");
        }
        public void Delete(int id)
        {
            var Passengerid = db.TblPassengers.Where(i => i.FlightDetailsId == id).Select(i => i.PassengerId).ToList();
            foreach (var item in Passengerid)
            {
                TblPassenger tblPassenger = db.TblPassengers.Find(item);
                db.TblPassengers.Remove(tblPassenger);
                db.SaveChanges();
            }
            var ticketid = db.Tbltickets.Where(i => i.FlightDetailsId == id).Select(i => i.Tid).ToList();
            foreach (var item in ticketid)
            {
                Tblticket tblticket = db.Tbltickets.Find(item);
                db.Tbltickets.Remove(tblticket);
                db.SaveChanges();
            }

        }
        public IActionResult DeleteFlight(int id)
        {
            try
            {
                var Detailid = db.TblFlightDetails.Where(i => id == i.Flightid).Select(i => i.FlightDetailsId).ToList();
                foreach (var item in Detailid)
                {
                    Delete(item);
                    TblFlightDetail tblFlightDetail = db.TblFlightDetails.Find(item);
                    db.TblFlightDetails.Remove(tblFlightDetail);
                    db.SaveChanges();
                }
                TblFlightMaster tblFlightMaster = db.TblFlightMasters.Find(id);
                db.TblFlightMasters.Remove(tblFlightMaster);
                db.SaveChanges();
                return RedirectToAction("DisplayFlight");
            }
            catch(Exception e)
            {
                ViewBag.msg = e.Message;
                return RedirectToAction("DisplayFlight");
            }
        }
        #endregion
        #region Get Details By id
        public IActionResult GetTicketDetails(int id, int p=1)
        {
            List<Tblticket> tbltickets = new List<Tblticket>();
            try
            {
                var ticketid = db.Tbltickets.Where(i => i.FlightDetailsId == id).Select(i => i.Tid).ToList();
               
                foreach (var item in ticketid)
                {
                    Tblticket tblticket = db.Tbltickets.Find(item);
                    TblUser tblUser = new TblUser();
                    var username = (from t in db.Tbltickets join u in db.TblUsers on t.Userid equals u.Userid where t.Tid == item select u.FirstName).FirstOrDefault();
                    tblUser.FirstName = username;
                    tblticket.User = tblUser;
                    tbltickets.Add(tblticket);
                }
                PagedResult<Tblticket> pagedResult = (PagedResult<Tblticket>)tbltickets.AsQueryable().GetPaged(p, 3);
                return View(pagedResult);
            }
            catch(Exception e)
            {
                ViewBag.msg = e.Message;
                return View(tbltickets);
            }
        }
       
       
        public IActionResult GetPassengerDetails(int id)
        {
            List<TblPassenger> tblPassengers = new List<TblPassenger>();
            try
            {
                var passengerid = db.TblPassengers.Where(i => i.Tid == id).Select(i => i.PassengerId).ToList();
               
                foreach (var item in passengerid)
                {
                    TblPassenger tblPassenger = db.TblPassengers.Find(item);
                    tblPassengers.Add(tblPassenger);
                }
                return View(tblPassengers);
            }
            catch(Exception e)
            {
                ViewBag.msg = e.Message;
                return View(tblPassengers);
            }
        }
        #endregion
        #region Display details
        public IActionResult DisplayTicketDetails(string name,string fname, int p=1)
        {
            
            List<Tblticket> tbltickets = new List<Tblticket>();
            try
            {
                //var ticketid = db.Tbltickets.Select(i => i.Tid).ToList();
                var ticketid = (from t in db.Tbltickets
                              join u in db.TblUsers on t.Userid equals u.Userid
                              join f in db.TblFlightDetails on t.FlightDetailsId equals f.FlightDetailsId
                              join m in db.TblFlightMasters on f.Flightid equals m.Flightid
                              where (u.FirstName.Contains(name) || name == null) && (m.Fname.Contains(fname) || fname == null)
                              select t.Tid).ToList();
                
                foreach (var item in ticketid)
                {
                    TblUser tblUser = new TblUser();
                    Tblticket tblticket = db.Tbltickets.Find(item);
                    var username = (from t in db.Tbltickets join u in db.TblUsers on t.Userid equals u.Userid where t.Tid == item select u.FirstName).FirstOrDefault();
                    tblUser.FirstName = username;
                    tblticket.User = tblUser;
                    var FlightDetail = (from t in db.Tbltickets
                                        join f in db.TblFlightDetails
                                        on t.FlightDetailsId equals f.FlightDetailsId
                                        join fm in db.TblFlightMasters
                                        on f.Flightid equals fm.Flightid
                                        where t.Tid == item
                                        select new { fm.Fname, f.DepatureDate }).ToList();
                    foreach (var detail in FlightDetail)
                    {
                        TblFlightDetail tblFlightDetail = new TblFlightDetail();
                        TblFlightMaster tblFlightMaster = new TblFlightMaster();
                        tblFlightDetail.DepatureDate = detail.DepatureDate;
                        tblFlightMaster.Fname = detail.Fname;
                        tblFlightDetail.Flight = tblFlightMaster;
                        tblticket.FlightDetails = tblFlightDetail;
                        tbltickets.Add(tblticket);
                    }


                }
                PagedResult<Tblticket> pagedResult = (PagedResult<Tblticket>)tbltickets.AsQueryable().GetPaged(p, 3);
                return View(pagedResult);
            }
            catch(Exception e)
            {
                ViewBag.msg = e.Message;
                return View(tbltickets);
            }

        }
        public IActionResult DisplayPassengerDetails(string fname, string date, int p=1)
        {
            List<TblPassenger> tblPassengers = new List<TblPassenger>();
            try
            {

                //var passengerid = db.TblPassengers.Select(i => i.PassengerId).ToList();
                var passengerid = (from tp in db.TblPassengers
                                 join d in db.TblFlightDetails on tp.FlightDetailsId equals d.FlightDetailsId
                                 join m in db.TblFlightMasters
                                 on d.Flightid equals m.Flightid
                                 where (m.Fname.Contains(fname) || fname == null) && (d.DepatureDate.ToString().Substring(0, 10).Contains(date) || date == null)
                                 select tp.PassengerId).ToList();
                
                foreach (var item in passengerid)
                {
                    TblUser tblUser = new TblUser();
                    TblPassenger tblPassenger = db.TblPassengers.Find(item);
                    var username = (from t in db.TblPassengers join u in db.TblUsers on t.Userid equals u.Userid where t.PassengerId == item select u.FirstName).FirstOrDefault();
                    tblUser.FirstName = username;
                    tblPassenger.User = tblUser;
                    var FlightDetail = (from t in db.TblPassengers
                                        join f in db.TblFlightDetails
                                        on t.FlightDetailsId equals f.FlightDetailsId
                                        join fm in db.TblFlightMasters
                                        on f.Flightid equals fm.Flightid
                                        where t.PassengerId == item
                                        select new { fm.Fname, f.DepatureDate }).ToList();
                    foreach (var detail in FlightDetail)
                    {
                        TblFlightDetail tblFlightDetail = new TblFlightDetail();
                        TblFlightMaster tblFlightMaster = new TblFlightMaster();
                        tblFlightDetail.DepatureDate = detail.DepatureDate;
                        tblFlightMaster.Fname = detail.Fname;
                        tblFlightDetail.Flight = tblFlightMaster;
                        tblPassenger.FlightDetails = tblFlightDetail;
                        tblPassengers.Add(tblPassenger);
                    }

                }
                PagedResult<TblPassenger> pagedResult = (PagedResult<TblPassenger>)tblPassengers.AsQueryable().GetPaged(p, 3);
                return View(pagedResult);
            }
            catch(Exception e)
            {
                ViewBag.msg = e.Message;
                return View(tblPassengers);
            }
        }
        #endregion

    }
}
