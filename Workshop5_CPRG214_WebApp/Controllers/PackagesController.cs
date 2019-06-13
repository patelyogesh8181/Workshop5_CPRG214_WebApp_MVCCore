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
    public class PackagesController : Controller
    {
        private readonly TravelExpertDBContext _context;

        public PackagesController(TravelExpertDBContext context)
        {
            _context = context;
        }

        // GET: Packages
        public async Task<IActionResult> Index()
        {
            ViewBag._Message = "0";
            return View(await _context.Packages.ToListAsync());
        }

        // GET: Packages/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            if (HttpContext.Session.GetString("_CustomerId") != string.Empty)
            {
                if (Convert.ToInt32(HttpContext.Session.GetString("_CustomerId")) > 0)
                {
                    var packages = await _context.Packages
                .FirstOrDefaultAsync(m => m.PackageId == id);
                    if (packages == null)
                    {
                        return NotFound();
                    }

                    return View(packages);
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

        // GET: Packages/Insert/5
        public async Task<IActionResult> Insert(int? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }

                if (HttpContext.Session.GetString("_CustomerId") != string.Empty)
                {
                    if (Convert.ToInt32(HttpContext.Session.GetString("_CustomerId")) > 0)
                    {
                        var packages = await _context.Packages
                            .FirstOrDefaultAsync(m => m.PackageId == id);
                        if (packages == null)
                        {
                            return NotFound();
                        }
                        Guid nGuid = new Guid();
                        Bookings objBookings = new Bookings();
                        objBookings.BookingDate = DateTime.Now;
                        objBookings.BookingNo = nGuid.ToString();
                        objBookings.TravelerCount = 0.1;
                        objBookings.CustomerId = Convert.ToInt32(HttpContext.Session.GetString("_CustomerId"));
                        objBookings.TripTypeId = "B";
                        objBookings.PackageId = Convert.ToInt32(packages.PackageId);

                        _context.Add(objBookings);
                        _context.SaveChanges();


                        BookingDetails obj = new BookingDetails();
                        obj.TripStart = packages.PkgStartDate;
                        obj.TripEnd = packages.PkgEndDate;
                        obj.Description = packages.PkgDesc;
                        obj.Destination = packages.PkgName;
                        obj.BasePrice = packages.PkgBasePrice;
                        obj.AgencyCommission = packages.PkgAgencyCommission;
                        obj.BookingId = objBookings.BookingId;
                        obj.RegionId = "AFR";
                        obj.ClassId = "BSN";
                        obj.FeeId = "CH";

                        _context.Add(obj);
                        _context.SaveChanges();

                        ViewBag._MessageNo = "1";

                        return RedirectToAction("Index", "Packages");
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
            catch(Exception ex)
            {
                ViewBag._MessageNo = "-1";
                return RedirectToAction("Index", "Packages");
            }
        }

        // GET: Packages/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Packages/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PackageId,PkgName,PkgStartDate,PkgEndDate,PkgDesc,PkgBasePrice,PkgAgencyCommission")] Packages packages)
        {
            if (ModelState.IsValid)
            {
                _context.Add(packages);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(packages);
        }

        // GET: Packages/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var packages = await _context.Packages.FindAsync(id);
            if (packages == null)
            {
                return NotFound();
            }
            return View(packages);
        }

        // POST: Packages/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PackageId,PkgName,PkgStartDate,PkgEndDate,PkgDesc,PkgBasePrice,PkgAgencyCommission")] Packages packages)
        {
            if (id != packages.PackageId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(packages);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PackagesExists(packages.PackageId))
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
            return View(packages);
        }

        // GET: Packages/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var packages = await _context.Packages
                .FirstOrDefaultAsync(m => m.PackageId == id);
            if (packages == null)
            {
                return NotFound();
            }

            return View(packages);
        }

        // POST: Packages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var packages = await _context.Packages.FindAsync(id);
            _context.Packages.Remove(packages);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PackagesExists(int id)
        {
            return _context.Packages.Any(e => e.PackageId == id);
        }
    }
}
