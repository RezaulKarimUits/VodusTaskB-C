using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using VodusTaskB.Models;
using VodusTaskB.Service;

namespace VodusTaskB.Controllers
{
    public class OrderController : Controller
    {
        private readonly ILogger<OrderController> _logger;
        private readonly IOrderService _orderService;
        public OrderController(ILogger<OrderController> logger, IOrderService service)
        {
            _logger = logger;
            _orderService = service;
        }

        public async Task< IActionResult> Index(string searchQuery, DateTime? fromDate, DateTime? toDate, bool loadFromJson = false)
        {

            if (loadFromJson)
            {
                var ordersFromJson = _orderService.GetOrdersFromJson(searchQuery, fromDate, toDate);
                return View(ordersFromJson);
            }
            else
            {
                var ordersFromDb = await _orderService.GetOrdersFromPostGreSql(searchQuery, fromDate, toDate);

                return View(ordersFromDb);
            }
        }

        public async Task<IActionResult> TaskC()
        {

                var ordersFromDb = await _orderService.GetOrdersFromPostGreSql();

                return View(ordersFromDb);
            
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
