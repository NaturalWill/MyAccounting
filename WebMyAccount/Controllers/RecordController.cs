using Microsoft.AspNetCore.Mvc;
using System.Linq;
using WebMyAccount.Helper;
using WebMyAccount.Models;

namespace WebMyAccount.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class RecordController : ControllerBase
    {
        private readonly MyAccountContext _context;
        public RecordController(MyAccountContext context) => _context = context;

        public ActionResult Add([FromBody]RecordDto record)
        {
            //record.RecordId = IdHelper.Next();
            _context.Records.Add(record);
            var line = _context.SaveChanges();
            record.Details.ForEach(x =>
            {
                x.RecordId = record.RecordId;
                _context.RecordDetails.Add(x);
            });
            line = _context.SaveChanges();
            return Ok(new { line });
        }
        [HttpGet]
        public ActionResult List()
        {
            //record.RecordId = IdHelper.Next();
            var list = _context.Records.ToList();

            return Ok(list);
        }

        [HttpGet]
        public ActionResult Detail(long recordId)
        {
            //record.RecordId = IdHelper.Next();
            var list = _context.RecordDetails.Where(x => x.RecordId == recordId).ToList();

            return Ok(list);
        }
    }
}