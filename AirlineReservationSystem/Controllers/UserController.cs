using Microsoft.AspNetCore.Mvc;
using AirlineReservationSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using static AirlineReservationSystem.Models.PagedResultBase;

namespace AirlineReservationSystem.Controllers
{
    public class UserController : Controller
    {
        private readonly DBAirlineReservationContext db;
        public UserController(DBAirlineReservationContext context)
        {
            db = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        #region Display Reserved Flight
        public IActionResult ReservedFlight(string sortOrder)
        {
            ViewData["AmountSortParam"] = string.IsNullOrEmpty(sortOrder) ? "Amount_desc" : "";
            ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";
            

            List<ReservedFlight> reservedFlights = new List<ReservedFlight>();
            try
            {
                ReservedFlight reservedFlight = new ReservedFlight();
                var email = TempData["Loginlist"].ToString();
                int uid = (from user in db.TblUsers
                           where user.Emailid == email
                           select user.Userid).FirstOrDefault();
                TempData["uid"] = uid;
                TempData.Keep();
                var ticketdetails = (from t in db.Tbltickets
                                     join f in db.TblFlightDetails on t.FlightDetailsId equals f.FlightDetailsId
                                     join m in db.TblFlightMasters on f.Flightid equals m.Flightid
                                     where t.Userid == uid
                                     select new { m.Fname, f.Source, f.Destination, f.DepatureDate, f.DepatureTime, t.Classtype, t.TotalSeatsReserved, t.Type, t.ReturnDate, t.TotalAmount, t.Tid });
                switch (sortOrder)
                {
                   
                    case "Date":
                        ticketdetails = ticketdetails.OrderBy(s => s.DepatureDate);
                        break;
                    case "date_desc":
                        ticketdetails = ticketdetails.OrderByDescending(s => s.DepatureDate);
                        break;
                    case "Amount_desc":
                        ticketdetails = ticketdetails.OrderByDescending(s => s.TotalAmount);
                        break;
                    default:
                        ticketdetails = ticketdetails.OrderBy(s => s.TotalAmount);
                        break;

                }
                var ticketdetails1 = ticketdetails.ToList();
                reservedFlight.Userid = uid;

                foreach (var item in ticketdetails1)
                {
                    ReservedFlight reservedFlight1 = new ReservedFlight();
                    reservedFlight1.Tid = item.Tid;
                    reservedFlight1.Userid = reservedFlight.Userid;
                    reservedFlight1.FlightName = item.Fname;
                    reservedFlight1.Source = item.Source;
                    reservedFlight1.Destination = item.Destination;
                    reservedFlight1.DepatureDate = item.DepatureDate.ToString().Substring(0, 10);
                    reservedFlight1.DepatureTime = item.DepatureTime.ToString();
                    reservedFlight1.ClassType = item.Classtype;
                    reservedFlight1.TotalSeatsReserved = (int)item.TotalSeatsReserved;
                    reservedFlight1.TicketType = item.Type;
                    reservedFlight1.ReturnDate = item.ReturnDate;
                    reservedFlight1.Amount = (decimal)item.TotalAmount;
                    reservedFlights.Add(reservedFlight1);
                }
                
                return View(reservedFlights);
            }
            catch(Exception e)
            {
                ViewBag.msg = e.Message;
                return View(reservedFlights);
            }
        }
        #endregion
        #region Cancel Reserved Flight
        public IActionResult CancelFlight(int id)
        {
            try
            {

                var flidhtdetailid = (from f in db.Tbltickets
                                      where f.Tid == id
                                      select f.FlightDetailsId).FirstOrDefault();


                var passengerId = (from p in db.TblPassengers
                                   where p.Tid == id
                                   select p.PassengerId).ToList();


                foreach (var item in passengerId)
                {
                    TblPassenger tblPassenger = db.TblPassengers.Find(item);
                    db.TblPassengers.Remove(tblPassenger);
                    db.SaveChanges();
                }
                Tblticket tblticket = db.Tbltickets.Find(id);
                db.Tbltickets.Remove(tblticket);
                db.SaveChanges();

                TblFlightDetail tblFlightDetail = db.TblFlightDetails.Find(flidhtdetailid);
                if (tblticket.Classtype == "Economy")
                {
                    tblFlightDetail.AvailableEconomySeats += tblticket.TotalSeatsReserved;
                }
                else
                {
                    tblFlightDetail.AvailableBusinessSeats += tblticket.TotalSeatsReserved;
                }
                db.Entry(tblFlightDetail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("ReservedFlight");
            }
            catch(Exception e)
            {
                ViewBag.msg = e.Message;
                return RedirectToAction("ReservedFlight");
            }
        }
        #endregion
        #region Get Ticket Details By Id
        public IActionResult TicketDetails(int id)
        {
            List<Passenger> passengers = new List<Passenger>();
            try
            {
                var PassengerDetails = (from p in db.TblPassengers
                                        where p.Tid == id
                                        select new { p.PassengerName, p.Age, p.Seatno }).ToList();

                foreach (var item in PassengerDetails)
                {
                    Passenger passenger = new Passenger();
                    passenger.PassengerName = item.PassengerName;
                    passenger.Age = item.Age;
                    passenger.Seatno = item.Seatno;
                    passengers.Add(passenger);
                }
                return View(passengers);
            }
            catch(Exception e)
            {
                ViewBag.msg = e.Message;
                return View(passengers);
            }
        }
        #endregion
        #region Search Flight
        public IActionResult SearchFlight()
        {
            return View();
        }
        [HttpPost]
        public IActionResult SearchFlight(Flight flight)
        {
            TempData["Flight"] = JsonConvert.SerializeObject(flight);           
            //TempData.Keep();
            return RedirectToAction("FlightDetails");
        }
        public IActionResult FlightDetails(string sortOrder, string fname, int p=1)
        {            
            Flight flights= JsonConvert.DeserializeObject<Flight>((string)TempData["Flight"]);
            TempData.Keep();
            ViewData["TimeSortParam"] = string.IsNullOrEmpty(sortOrder) ? "Time_desc" : "";
            List<FlightDetails> flightDetails = new List<FlightDetails>();
            try
            {
                var Flight = (from f in db.TblFlightDetails
                              join m in db.TblFlightMasters
                              on f.Flightid equals m.Flightid
                              where flights.From == f.Source && flights.To == f.Destination && flights.Date == f.DepatureDate &&(m.Fname.Contains(fname) || fname==null)
                              select f);
                switch (sortOrder)
                {
                    case "Time_desc":
                        Flight = Flight.OrderByDescending(s => s.DepatureTime);
                        break;
                    default:
                        Flight = Flight.OrderBy(s => s.DepatureTime);
                        break;
                }

                var Fid = Flight.Select(s => s.FlightDetailsId).ToList();

                //var Fid = (from f in db.TblFlightDetails
                //           where flight.From == f.Source && flight.To == f.Destination && flight.Date == f.DepatureDate
                //           select f.FlightDetailsId).ToList();
                if (Fid != null)
                {
                    foreach (var item in Fid)
                    {
                        FlightDetails flightDetail = new FlightDetails();
                        TblFlightDetail tblFlightDetail = db.TblFlightDetails.Find(item);
                        var FlightName = (from d in db.TblFlightDetails
                                          join m in db.TblFlightMasters
                                          on d.Flightid equals m.Flightid
                                          where d.FlightDetailsId == item
                                          select m.Fname).FirstOrDefault();

                        flightDetail.Detailsid = item;
                        flightDetail.FlightName = FlightName;
                        flightDetail.Source = flights.From;
                        flightDetail.Destination = flights.To;
                        flightDetail.DepartureDate = flights.Date.ToString().Substring(0, 10);
                        flightDetail.DepartureTime = tblFlightDetail.DepatureTime.ToString();
                        flightDetail.AvailableEconomySeats = tblFlightDetail.AvailableEconomySeats;
                        flightDetail.EconomyClassFare = tblFlightDetail.EconomyclassFare;
                        flightDetail.AvailableBusinessSeats = tblFlightDetail.AvailableBusinessSeats;
                        flightDetail.BusinessClassFare = tblFlightDetail.BusinessclassFare;
                        flightDetails.Add(flightDetail);
                    }
                }
                PagedResult<FlightDetails> pagedResult = (PagedResult<FlightDetails>)flightDetails.AsQueryable().GetPaged(p, 3);
                return View(pagedResult);
            }
            catch(Exception e)
            {
                ViewBag.msg = e.Message;
                return View(flightDetails);
            }

        }
        #endregion
        #region Book Flight
        public IActionResult SelectFlight(int id)
        {
          
            TempData["FlightdetailsId"] = id;
            TempData.Keep();
            return View();
        }
        [HttpPost]
        public IActionResult SelectFlight(Tblticket tblticket)
        {
            int FlightDetailsId = (int)TempData["FlightdetailsId"];
            int Userid = (int)TempData["uid"];
            TempData["Seats"] = tblticket.TotalSeatsReserved;
            TempData.Keep();
            tblticket.FlightDetailsId = FlightDetailsId;
            tblticket.Userid = Userid;

            TblFlightDetail tblFlightDetail = new TblFlightDetail();
            try { 
                tblFlightDetail = db.TblFlightDetails.Find(FlightDetailsId);


           
                if (tblticket.Classtype == "Economy")
                {
                

                    if (tblFlightDetail.AvailableEconomySeats > tblticket.TotalSeatsReserved)
                    {
                        tblticket.TotalAmount = tblFlightDetail.EconomyclassFare * tblticket.TotalSeatsReserved;
                        tblFlightDetail.AvailableEconomySeats -= tblticket.TotalSeatsReserved;
                        db.Entry(tblFlightDetail).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                    else
                    {
                    
                        ViewBag.msg = "Sorry Seats Unavailable";
                        return View();
                    }
                }
                else
                {
               

                    if (tblFlightDetail.AvailableBusinessSeats > tblticket.TotalSeatsReserved)
                    {
                        tblticket.TotalAmount = tblFlightDetail.BusinessclassFare * tblticket.TotalSeatsReserved;
                        tblFlightDetail.AvailableBusinessSeats -= tblticket.TotalSeatsReserved;
                        db.Entry(tblFlightDetail).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                    else
                    {
                        ViewBag.msg = "Sorry Seats Unavailable";
                        return View();
                    }
                }
                if (tblticket.Type == "Return")
                {
                    tblticket.TotalAmount = tblticket.TotalAmount * 2;
                }
            
                db.Tbltickets.Add(tblticket);
                db.SaveChanges();
                TempData["Amount"] = JsonConvert.SerializeObject(tblticket.TotalAmount);
                TempData.Keep();
                return RedirectToAction("PassengerDetails");
            }
            catch
            {
                return RedirectToAction("PassengerDetails");
            }
        }
        #endregion
        #region Passenger Details
        public IActionResult PassengerDetails()
        {
            int fid = (int)TempData["FlightdetailsId"];
            TempData.Keep();
            var seats = db.TblPassengers.Where(i => i.FlightDetailsId == fid).Select(i => i.Seatno).ToList();
            ViewData["seats"] = seats;
            return View();
        }
        [HttpPost]
        public IActionResult PassengerDetails(TblPassenger tblPassenger)
        {
            tblPassenger.Userid = (int)TempData["uid"];
            tblPassenger.FlightDetailsId = (int)TempData["FlightdetailsId"];
            TempData.Keep();
            var seats = db.TblPassengers.Where(i => i.FlightDetailsId == tblPassenger.FlightDetailsId).Select(i => i.Seatno).ToList();
            ViewData["seats"] = seats;
            foreach (var seat in seats)
            {
                if (tblPassenger.Seatno == seat)
                {
                    ViewBag.msg = "Seat unavailable!! Please choose other seats";
                    return View();
                }
            }
            int Ticketid = (from t in db.Tbltickets
                            where t.FlightDetailsId == tblPassenger.FlightDetailsId
                            orderby t.Tid descending
                            select t.Tid ).FirstOrDefault();
            TempData["Ticketid"] = Ticketid;
            TempData.Keep();
            tblPassenger.Tid = Ticketid;
            try
            {
                if (ModelState.IsValid)
                {
                    db.TblPassengers.Add(tblPassenger);
                    db.SaveChanges();
                    if ((int)TempData["Seats"] > 1)
                    {
                        TempData["Seats"] = (int)TempData["Seats"] - 1;
                        TempData.Keep();
                        return RedirectToAction("PassengerDetails");
                    }
                    return RedirectToAction("PaymentDetails");
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
        #region Payment
        public IActionResult PaymentDetails()
        {
            Payment payment = new Payment();
            payment.Amount = decimal.Parse((string)TempData["Amount"]);
            ViewBag.Amount = payment.Amount;
            TempData.Keep();
            return View();
        }
       [HttpPost]
       public IActionResult PaymentDetails(Payment payment)
        {
            payment.Amount = decimal.Parse((string)TempData["Amount"]);
            ViewBag.Amount = payment.Amount;
            TempData.Keep();
            if (ModelState.IsValid)
            {
                return RedirectToAction("PaymentSuccessfull");
            }
            return View();
        }
        #endregion
        #region Ticket Download
        public IActionResult PaymentSuccessfull()
        {
            var FlightDetails = (from d in db.TblFlightDetails
                              join m in db.TblFlightMasters
                              on d.Flightid equals m.Flightid
                              join t in db.Tbltickets on d.FlightDetailsId equals t.FlightDetailsId
                              where d.FlightDetailsId == (int)TempData["FlightdetailsId"] && t.Tid == (int)TempData["Ticketid"]
                              select new { m.Fname, d.Source, d.Destination, d.DepatureDate, d.DepatureTime, t.Classtype, t.TotalAmount }).ToList();
            TempData.Keep();
            var PassengerDetails = (from p in db.TblPassengers
                                    where p.Tid == (int)TempData["Ticketid"]
                                    select new { p.PassengerName, p.Seatno, p.Age }).ToList();
            List<Ticket> tickets = new List<Ticket>();
            

            foreach (var flight in FlightDetails)
            {
               
                ViewBag.FlightName = flight.Fname;
                ViewBag.Source = flight.Source;
                ViewBag.Destination = flight.Destination;
                ViewBag.DepartureDate = flight.DepatureDate.ToString().Substring(0,10);
                ViewBag.DepartureTime = flight.DepatureTime.ToString();
                ViewBag.ClassType = flight.Classtype;
                ViewBag.TotalFare = flight.TotalAmount;
                foreach (var passenger in PassengerDetails)
                {
                    Ticket ticket = new Ticket();

                    ticket.PassengerName = passenger.PassengerName;
                    ticket.Age = passenger.Age;
                    ticket.seatno = passenger.Seatno;
                    tickets.Add(ticket);
                }
                

            }
            
            return View(tickets);
        }
        #endregion

        public IActionResult Seats()
        {
            return View();
        }


        
         
    }
}


