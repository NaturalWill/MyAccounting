using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebMyAccount.Models;

namespace WebMyAccount.Controllers
{

    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly MyAccountContext _context;

        public AccountController(MyAccountContext context)
        {
            _context = context;

            if (_context.Accounts.Count() == 0)
            {
                // Create a new TodoItem if collection is empty,
                // which means you can't delete all TodoItems.
                _context.Accounts.Add(new Account { Name = "Item1" });
                _context.SaveChanges();
            }


        }
        [HttpGet]
        public ActionResult<List<Account>> List()
        {
            var list = _context.Accounts.ToList();
            return Ok(list);
        }
    }
}