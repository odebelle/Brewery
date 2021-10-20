using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Attributes;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Logging;
using Models;

namespace Brewery.Controllers
{
    [ODataRouteComponent("odata")]
    public abstract class ODataControllerBase<T> : ODataController where T : class
    {
        protected readonly BreweryContext _context;
        private readonly ILogger<T> _logger;

        // GET
        public ODataControllerBase(
            ILogger<T> logger,
            BreweryContext ctx)
        {
            _context = ctx;
            _logger = logger;
        }

        [EnableQuery]
        [HttpGet]
        public ActionResult<IQueryable<T>> Get()
        {
            var chose = _context.Set<T>();
            return Ok(chose);
        }

        [HttpPost]
        public virtual async Task<ActionResult<T>> Post([FromBody] T entity)
        {
            var result = _context.Set<T>().Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
    }

    public class BierController : ODataControllerBase<Bier>
    {
        public BierController(ILogger<Bier> logger, BreweryContext ctx) : base(logger, ctx)
        {
        }
    }

    public class BreweryController : ODataControllerBase<Models.Brewery>
    {
        public BreweryController(ILogger<Models.Brewery> logger, BreweryContext ctx) : base(logger, ctx)
        {
        }
    }

    public class WholesalerController : ODataControllerBase<Wholesaler>
    {
        public WholesalerController(ILogger<Wholesaler> logger, BreweryContext ctx) : base(logger, ctx)
        {
        }
    }

    public class LineOrderController : ODataControllerBase<LineOrder>
    {
        public LineOrderController(ILogger<LineOrder> logger, BreweryContext ctx) : base(logger, ctx)
        {
        }
    }

    public class StockController : ODataControllerBase<Stock>
    {
        public StockController(ILogger<Stock> logger, BreweryContext ctx) : base(logger, ctx)
        {
        }

        public override async Task<ActionResult<Stock>> Post(Stock entity)
        {
            var stock = _context.Stocks.SingleOrDefault(s =>
                s.WholesalerID == entity.WholesalerID && s.BierId == entity.BierId);
            if (stock!=null)
            {
                try
                {
                    var wholesaler = await _context.Wholesalers.FindAsync(entity.WholesalerID);
                    if (wholesaler== null)
                    {
                        return BadRequest("Wholesaler not found");
                    }

                    var capacity = wholesaler.Capacity - stock.Count + entity.Count;
                    var test = capacity <= wholesaler.Capacity;
                    if (test)
                    {
                        wholesaler.Capacity = capacity;
                        stock.Count = entity.Count;
                        await _context.SaveChangesAsync();
                    }
                    else
                    {
                        _context.Stocks.Add(entity);
                        await _context.SaveChangesAsync();
                    }
                }
                catch (Exception e)
                {
                    return BadRequest(e);
                }
            }
            return await base.Post(entity);
        }

        [HttpPut]
        public async Task<ActionResult<Stock>> Put([FromRoute]int key, [FromBody] Stock entity)
        {
            var result = await _context.Stocks.FindAsync(key);
            if (result==null)
            {
                return BadRequest();
            }
            else
            {
                result.Count = entity.Count;
            }
            await _context.SaveChangesAsync();
            return Ok(result);
        }
    }
}