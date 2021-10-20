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

        public override async Task<ActionResult<Order>> Post([FromBody]Order entity)
        {
            var capacity = _context.Wholesalers.SingleOrDefault(s => s.WholesalerId == entity.WholesalerId)?.Capacity;
            if (capacity == null) return BadRequest("Wholesaler not exist");

            var stock = _context.Stocks.Where(w => w.WholesalerID == entity.WholesalerId).Select(s => s.BierId);
            foreach (var bier in entity.LineOrders)
            {
                if (stock.Contains(bier.BierId)) continue;
                
                var notFound = await _context.Biers.FindAsync(bier.BierId);
                return BadRequest($"{notFound.Name} not for this wholesaler.");
            }
            
            var qry = entity.LineOrders.GroupBy(x => x.BierId).Select(s => s.First()).ToList();
            entity.LineOrders = qry;

            var ordered = qry.Sum(s => s.Quantity);
            if (ordered > capacity) return BadRequest("Out of capacity");

            return await base.Post(entity);
        }
    }
}