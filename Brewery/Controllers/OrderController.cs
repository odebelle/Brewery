using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Models;

namespace Brewery.Controllers
{
    public class OrderController : ODataControllerBase<Order>
    {
        public OrderController(ILogger<Order> logger, BreweryContext ctx) : base(logger, ctx)
        {
        }

        public override async Task<ActionResult<Order>> Post(Order entity)
        {
            var capacity = _context.Wholesalers.SingleOrDefault(s => s.WholesalerId == entity.WholesalerId)?.Capacity;
            if (capacity == null) return BadRequest("Wholesaler not exist");
            
            var qry = entity.LineOrders.GroupBy(x => x.BierId).Select(s => s.First()).ToList();
            entity.LineOrders = qry;
            
            var ordered = qry.Sum(s => s.Quantity);
            if (ordered>capacity) return BadRequest("Out of capacity");
            
            return await base.Post(entity);
        }
    }
}