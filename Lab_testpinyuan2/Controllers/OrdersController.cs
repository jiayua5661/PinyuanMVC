using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Lab_testpinyuan2.Models;
using LAB_testpinyuan.Dto;
using System.ComponentModel.Design;
using Lab_testpinyuan2.Dto;
using Lab_testpinyuan2.ViewModels;

namespace Lab_testpinyuan2.Controllers
{
    public class OrdersController : Controller
    {
        private readonly PinyuanContext _context;

        public OrdersController(PinyuanContext context)
        {
            _context = context;
        }

        // GET: Orders
        public async Task<IActionResult> Index()
        {
            var result = from a in _context.Orders
                         join b in _context.Clients on a.CompanyId equals b.ClientId
                         select new OrderIndexDto
                         {
                             OrderId = a.OrderId,
                             CompanyName = b.CompanyName,
                             OrderDate = a.OrderDate,
                             QuoteNumber = a.QuoteNumber
                         };
            return View(await result.ToListAsync());
        }

        // 搜尋估價單號碼
        [HttpPost]
        public async Task<IActionResult> Index(string searchText)
        {
            var data = from a in _context.Orders
                       join b in _context.Clients on a.CompanyId equals b.ClientId
                       where a.QuoteNumber.Contains(searchText)
                       select new OrderIndexDto
                       {
                           OrderId = a.OrderId,
                           CompanyName = b.CompanyName,
                           OrderDate = a.OrderDate,
                           QuoteNumber = a.QuoteNumber
                       };

            return View(await data.ToListAsync());
        }

        // GET: Orders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .FirstOrDefaultAsync(m => m.OrderId == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // 自己寫的
        //order 跟 多筆orderDetail 一起新增頁面
        public async Task<IActionResult> CreateOrderOrderDetail()
        {
            var OrderOrderDetailCreateViewModel = new OrderOrderDetailCreateViewModel();

            OrderOrderDetailCreateViewModel.EditOrderClientDtos = await(from a in _context.Clients
                                                                      select new EditOrderClientDto
                                                                      {
                                                                          ClientId = a.ClientId,
                                                                          CompanyName = a.CompanyName,
                                                                      }).ToListAsync();
            return View(OrderOrderDetailCreateViewModel);
        }

        // 自己寫的
        // 新增 沒有外鍵 order 跟 orderdetail insert
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateOrderOrderDetail(OrderDto orderDto)
        {
            if (ModelState.IsValid)
            {
                // order 是 資料庫 model
                Order insert = new Order();
                //                      dto 是 接收資料的模板 只開需要使用者輸入的 屬性
                //                          CurrentValues.SetValues 把dto接到的放到對應的order屬性裡面
                _context.Orders.Add(insert).CurrentValues.SetValues(orderDto);
                _context.SaveChanges();
                // 存進資料庫
                // 存完之後 才有orderid 在 insert身上
                foreach (var item in orderDto.OrderDetailDtos)
                {
                    OrderDetail insert2 = new OrderDetail()
                    {
                        // 帶入 orderID
                        OrderId = insert.OrderId
                    };
                    _context.OrderDetails.Add(insert2).CurrentValues.SetValues(item);// 對應資料
                }
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(orderDto);
        }

        #region EF 建的CREATE
        // GET: Orders/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OrderId,CompanyId,OrderDate,QuoteNumber")] Order order)
        {
            if (ModelState.IsValid)
            {
                _context.Add(order);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(order);
        }
        #endregion


        // 自己寫的 Edit
        //order 跟 多筆orderDetail 一起編輯頁面  顯示
        public async Task<IActionResult> EditOrderOrderDetail(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var OrderOrderDetailEditViewModel = new OrderOrderDetailEditViewModel();

            OrderOrderDetailEditViewModel.Order = await (from a in _context.Orders
                                                         where a.OrderId == id
                                                         select new EditOrderDto
                                                         {
                                                             OrderId = a.OrderId,
                                                             ClientId = a.CompanyId,
                                                             OrderDate = a.OrderDate,
                                                             QuoteNumber = a.QuoteNumber
                                                         }).SingleOrDefaultAsync();

            OrderOrderDetailEditViewModel.Order.EditOrderDetail = await (from a in _context.OrderDetails
                                                                         where a.OrderId == id
                                                                         select a).ToListAsync();

            OrderOrderDetailEditViewModel.EditOrderClientDtos = await (from a in _context.Clients
                                                                       select new EditOrderClientDto
                                                                       {
                                                                           ClientId = a.ClientId,
                                                                           CompanyName = a.CompanyName,
                                                                       }).ToListAsync();

            if (OrderOrderDetailEditViewModel.Order == null)
            {
                return NotFound();
            }
            return View(OrderOrderDetailEditViewModel);
        }

        // 自己寫的 Edit
        //order 跟 多筆orderDetail 一起更新 新增
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditOrderOrderDetail(int id, EditOrderDto Order)
        {
            if (id != Order.OrderId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var update = _context.Orders.Find(id);
                
                if (update != null)
                {
                    // 更新order欄位資料
                    update.CompanyId = Order.ClientId;
                    update.OrderDate = Order.OrderDate;
                    update.QuoteNumber = Order.QuoteNumber;

                    // 更新 或 新增 orderDetail
                    var updateOrderDetail = new OrderDetail();
                    foreach (var orderDetail in Order.EditOrderDetail)
                    {
                        updateOrderDetail = _context.OrderDetails.FirstOrDefault(x => x.OrdeDetailId == orderDetail.OrdeDetailId);
                        if(updateOrderDetail == null) // insert
                        {
                            orderDetail.OrderId = id;
                            _context.OrderDetails.Add(orderDetail);
                            int a = 0;
                        }
                        else // update
                        {
                            orderDetail.OrderId = updateOrderDetail.OrderId;
                            orderDetail.OrdeDetailId = updateOrderDetail.OrdeDetailId;
                            _context.Entry(updateOrderDetail).CurrentValues.SetValues(orderDetail);
                        }
                    }
                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(Index));
                }
            }
            return View(Order);
        }


        #region EF 建的 Edit
        // GET: Orders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OrderId,CompanyId,OrderDate,QuoteNumber")] Order order)
        {
            if (id != order.OrderId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(order);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderExists(order.OrderId))
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
            return View(order);
        }

        #endregion

        // GET: Orders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .FirstOrDefaultAsync(m => m.OrderId == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order != null)
            {
                _context.Orders.Remove(order);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderExists(int id)
        {
            return _context.Orders.Any(e => e.OrderId == id);
        }
    }
}
