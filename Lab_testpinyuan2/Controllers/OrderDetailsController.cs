using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Lab_testpinyuan2.Models;
using Lab_testpinyuan2.ViewModels;

namespace Lab_testpinyuan2.Controllers
{
    public class OrderDetailsController : Controller
    {
        private readonly PinyuanContext _context;

        public OrderDetailsController(PinyuanContext context)
        {
            _context = context;
        }

        // GET: OrderDetails
        public async Task<IActionResult> Index()
        {
            var data = from a in _context.OrderDetails
                       join b in _context.Orders on a.OrderId equals b.OrderId
                       join c in _context.Clients on b.CompanyId equals c.ClientId
                       select new OrderDetailIndexViewModel
                       {
                           CompanyName = c.CompanyName,
                           OrderDate = b.OrderDate,
                           QuoteNumber = b.QuoteNumber,
                           ProductName = a.ProductName,
                           Amount = a.Amount,
                           Price = a.Price
                       };

            return View(await data.ToListAsync());
        }

        [HttpPost]
        public async Task<IActionResult> Index(string searchText)
        {
            var data = from a in _context.OrderDetails
                       join b in _context.Orders on a.OrderId equals b.OrderId
                       join c in _context.Clients on b.CompanyId equals c.ClientId
                       where a.ProductName.Contains(searchText)
                       select new OrderDetailIndexViewModel
                       {
                           CompanyName = c.CompanyName,
                           OrderDate = b.OrderDate,
                           QuoteNumber = b.QuoteNumber,
                           ProductName = a.ProductName,
                           Amount = a.Amount,
                           Price = a.Price
                       };

            return View(await data.ToListAsync());
        }

        // GET: OrderDetails/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderDetail = await _context.OrderDetails
                .FirstOrDefaultAsync(m => m.OrdeDetailId == id);
            if (orderDetail == null)
            {
                return NotFound();
            }

            return View(orderDetail);
        }

        // GET: OrderDetails/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: OrderDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OrdeDetailId,OrderId,ProductName,Amount,Price")] OrderDetail orderDetail)
        {
            if (ModelState.IsValid)
            {
                _context.Add(orderDetail);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(orderDetail);
        }

        // GET: OrderDetails/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderDetail = await _context.OrderDetails.FindAsync(id);
            if (orderDetail == null)
            {
                return NotFound();
            }
            return View(orderDetail);
        }

        // POST: OrderDetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OrdeDetailId,OrderId,ProductName,Amount,Price")] OrderDetail orderDetail)
        {
            if (id != orderDetail.OrdeDetailId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(orderDetail);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderDetailExists(orderDetail.OrdeDetailId))
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
            return View(orderDetail);
        }

        // GET: OrderDetails/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderDetail = await _context.OrderDetails
                .FirstOrDefaultAsync(m => m.OrdeDetailId == id);
            if (orderDetail == null)
            {
                return NotFound();
            }

            return View(orderDetail);
        }

        // POST: OrderDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var orderDetail = await _context.OrderDetails.FindAsync(id);
            if (orderDetail != null)
            {
                _context.OrderDetails.Remove(orderDetail);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderDetailExists(int id)
        {
            return _context.OrderDetails.Any(e => e.OrdeDetailId == id);
        }
    }
}
