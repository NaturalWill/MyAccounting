using Microsoft.AspNetCore.Mvc;
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
            record.RecordId = IdHelper.Next();
            _context.Records.Add(record);
            record.Details.ForEach(x =>
            {
                x.RecordId = record.RecordId;
                _context.RecordDetails.Add(x);
            });
            var line = _context.SaveChanges();
            return Ok(new { line });
        }
    }
}