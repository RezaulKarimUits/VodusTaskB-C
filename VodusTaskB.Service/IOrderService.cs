using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VodusTask.Data;
using VodusTask.Data.Models;
using VodusTaskB.Models;

namespace VodusTaskB.Service
{
    public interface IOrderService
    {
        Task<IList<OrderViewModel>> GetOrdersFromPostGreSql(string searchQuery, DateTime? fromDate, DateTime? toDate);
        List<OrderViewModel> GetOrdersFromJson(string searchQuery, DateTime? fromDate, DateTime? toDate);
        Task<IList<OrderViewModel>> GetOrdersFromPostGreSql();
        Task ShowHideColumnAsync(List<ColumnConfiguratorViewModel> viewModel);
    }
}
