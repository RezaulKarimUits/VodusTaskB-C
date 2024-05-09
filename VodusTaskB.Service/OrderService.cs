using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Text.Json.Serialization;
using VodusTask.Data;
using VodusTask.Data.Models;
using VodusTaskB.Models;

namespace VodusTaskB.Service
{
    public class OrderService : IOrderService
    {
        private readonly VodusDbContext _dbContext;
        public OrderService(VodusDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<OrderViewModel> GetOrdersFromJson(string searchQuery, DateTime? fromDate, DateTime? toDate)
        {
            if (toDate is null)
            {
                toDate = DateTime.Now.Date;
            }
            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string fileName = "Order.json";
            string filePath = Path.Combine(desktopPath, fileName);
            string jsonContent = File.ReadAllText(filePath);
            string cleanedJsonContent = jsonContent.Trim('\"').Replace("\\u0022", "\"");
            var orders = JsonConvert.DeserializeObject<List<OrderViewModel>>(cleanedJsonContent) ?? new List<OrderViewModel>();
            
            if (!string.IsNullOrEmpty(searchQuery))
            {
                orders = orders.Where(x => x.Name.Contains(searchQuery)).ToList();
            }
            if (fromDate != null)
            {
                orders = orders.Where(x => x.OrderDate.Date >= fromDate.GetValueOrDefault().Date && x.OrderDate.Date <= toDate.GetValueOrDefault().Date).ToList();
            }
            return orders;
        }

        public async Task<IList<OrderViewModel>> GetOrdersFromPostGreSql(string searchQuery, DateTime? fromDate, DateTime? toDate)
        {
            if(toDate is null)
            {
                toDate = DateTime.UtcNow.Date;

            }
            var list = _dbContext.Order.AsNoTracking().Select(x=> new OrderViewModel
            {
                Id = x.Id,
                Name = x.Name,
                Image = x.Image,
                Price = x.Price,
                DiscountedPrice = x.DiscountedPrice,
                OrderDate = DateTime.SpecifyKind(x.OrderDate, DateTimeKind.Utc)
            }).AsQueryable();

            if(!string.IsNullOrEmpty(searchQuery))
            {
                list = list.Where(x=> x.Name.Contains(searchQuery));
            }
            if(fromDate != null)
            {
                list = list.Where(x=> x.OrderDate.Date >= fromDate.GetValueOrDefault().Date.AddDays(-1) && x.OrderDate.Date <= toDate.GetValueOrDefault().Date);
            }

            return await list.ToListAsync();
        }
        public async Task<IList<OrderViewModel>> GetOrdersFromPostGreSql()
        {
            var list = _dbContext.Order.AsNoTracking().Select(x => new OrderViewModel
            {
                Id = x.Id,
                Name = x.Name,
                Image = x.Image,
                Price = x.Price,
                DiscountedPrice = x.DiscountedPrice,
                OrderDate = DateTime.SpecifyKind(x.OrderDate, DateTimeKind.Utc)
            }).AsQueryable();


            return await list.ToListAsync();
        }
        //for show hide column based on user customization
        public async Task ShowHideColumnAsync(List<ColumnConfiguratorViewModel> viewModel)
        {

            var list = await _dbContext.ColumnConfigurator.ToListAsync();

            foreach (var column in viewModel)
            {
                var entity = list.FirstOrDefault(x => x.Name == column.Name);


                if (entity is not  null)
                {
                     entity.Isvisible = column.Isvisible;
                    _dbContext.Update(entity);
                }
            }
            await _dbContext.SaveChangesAsync();
        }
    }
}
