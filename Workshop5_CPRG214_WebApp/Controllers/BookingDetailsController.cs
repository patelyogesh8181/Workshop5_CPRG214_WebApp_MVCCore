using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Workshop5_CPRG214_WebApp.Models;

namespace Workshop5_CPRG214_WebApp.Controllers
{
    public class BookingDetailsController : Controller
    {
        private readonly TravelExpertDBContext _context;

        public BookingDetailsController(TravelExpertDBContext context)
        {
            _context = context;
        }

        // GET: BookingDetails
        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetString("_CustomerId") != string.Empty)
            {
                if (Convert.ToInt32(HttpContext.Session.GetString("_CustomerId")) > 0)
                {
                    int inCustomerID = Convert.ToInt32(HttpContext.Session.GetString("_CustomerId"));
                    var travelExpertDBContext = _context.BookingDetails.Include(b => b.Booking).Include(b => b.Class).Include(b => b.Fee).Include(b => b.Region).Where(b => b.Booking.CustomerId == inCustomerID);
                    return View(await travelExpertDBContext.ToListAsync());
                }
                else
                {
                    return RedirectToAction("Login", "Home");
                }
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }

        // GET: BookingDetails/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookingDetails = await _context.BookingDetails
                .Include(b => b.Booking)
                .Include(b => b.Class)
                .Include(b => b.Fee)
                .Include(b => b.Region)
                .FirstOrDefaultAsync(m => m.BookingDetailId == id);
            if (bookingDetails == null)
            {
                return NotFound();
            }

            return View(bookingDetails);
        }

        // GET: BookingDetails/Create
        public IActionResult Create()
        {
            ViewData["BookingId"] = new SelectList(_context.Bookings, "BookingId", "BookingId");
            ViewData["ClassId"] = new SelectList(_context.Set<Classes>(), "ClassId", "ClassId");
            ViewData["FeeId"] = new SelectList(_context.Set<Fees>(), "FeeId", "FeeId");
            ViewData["RegionId"] = new SelectList(_context.Set<Regions>(), "RegionId", "RegionId");
            return View();
        }

        // POST: BookingDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BookingDetailId,ItineraryNo,TripStart,TripEnd,Description,Destination,BasePrice,AgencyCommission,BookingId,RegionId,ClassId,FeeId,ProductSupplierId")] BookingDetails bookingDetails)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bookingDetails);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BookingId"] = new SelectList(_context.Bookings, "BookingId", "BookingId", bookingDetails.BookingId);
            ViewData["ClassId"] = new SelectList(_context.Set<Classes>(), "ClassId", "ClassId", bookingDetails.ClassId);
            ViewData["FeeId"] = new SelectList(_context.Set<Fees>(), "FeeId", "FeeId", bookingDetails.FeeId);
            ViewData["RegionId"] = new SelectList(_context.Set<Regions>(), "RegionId", "RegionId", bookingDetails.RegionId);
            return View(bookingDetails);
        }

        // GET: BookingDetails/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookingDetails = await _context.BookingDetails.FindAsync(id);
            if (bookingDetails == null)
            {
                return NotFound();
            }
            ViewData["BookingId"] = new SelectList(_context.Bookings, "BookingId", "BookingId", bookingDetails.BookingId);
            ViewData["ClassId"] = new SelectList(_context.Set<Classes>(), "ClassId", "ClassId", bookingDetails.ClassId);
            ViewData["FeeId"] = new SelectList(_context.Set<Fees>(), "FeeId", "FeeId", bookingDetails.FeeId);
            ViewData["RegionId"] = new SelectList(_context.Set<Regions>(), "RegionId", "RegionId", bookingDetails.RegionId);
            return View(bookingDetails);
        }

        // POST: BookingDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BookingDetailId,ItineraryNo,TripStart,TripEnd,Description,Destination,BasePrice,AgencyCommission,BookingId,RegionId,ClassId,FeeId,ProductSupplierId")] BookingDetails bookingDetails)
        {
            if (id != bookingDetails.BookingDetailId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bookingDetails);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookingDetailsExists(bookingDetails.BookingDetailId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["BookingId"] = new SelectList(_context.Bookings, "BookingId", "BookingId", bookingDetails.BookingId);
            ViewData["ClassId"] = new SelectList(_context.Set<Classes>(), "ClassId", "ClassId", bookingDetails.ClassId);
            ViewData["FeeId"] = new SelectList(_context.Set<Fees>(), "FeeId", "FeeId", bookingDetails.FeeId);
            ViewData["RegionId"] = new SelectList(_context.Set<Regions>(), "RegionId", "RegionId", bookingDetails.RegionId);
            return View(bookingDetails);
        }

        // GET: BookingDetails/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookingDetails = await _context.BookingDetails
                .Include(b => b.Booking)
                .Include(b => b.Class)
                .Include(b => b.Fee)
                .Include(b => b.Region)
                .FirstOrDefaultAsync(m => m.BookingDetailId == id);
            if (bookingDetails == null)
            {
                return NotFound();
            }

            return View(bookingDetails);
        }

        // POST: BookingDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bookingDetails = await _context.BookingDetails.FindAsync(id);
            _context.BookingDetails.Remove(bookingDetails);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookingDetailsExists(int id)
        {
            return _context.BookingDetails.Any(e => e.BookingDetailId == id);
        }
    }
}
