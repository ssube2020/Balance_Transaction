using Balance_Transaction.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace Balance_Transaction.Controllers
{
    public class TransactionController : Controller
    {
        private readonly AppDbContext _db;

        public TransactionController(AppDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            return View(_db.Persons.ToList());
        }

        [HttpPost]
        public IActionResult SubtractMoney(int personid, double totransac)
        {
            //Todo Get balance
            //Check Balance
            //if balance > amount than make transaction
            var person = _db.Persons.Where(k=>k.Id == personid).FirstOrDefault();
            if (person != null)
            {
                if ((person.Balance >= totransac) && totransac > 0)
                {
                    person.Balance -= totransac;
                    Transaction tran = new();
                    tran.Date = DateTime.Now;
                    tran.PersonId = personid;
                    tran.TransactionType = "გადარიცხვა";
                    tran.Amount = totransac;
                    _db.Transactions.Add(tran);
                    _db.SaveChanges();
                } else if((totransac == 0) || String.IsNullOrEmpty(totransac.ToString()))
                {
                    TempData["nulloerror"] = " 0 თეთრის გადარიცხვა შეუძლებელია ";
                }
                else
                {
                    TempData["BalanceError"] = "ანგარიშზე არარის საკმარისი თანხა";
                }
            }
            return RedirectToAction("Index", "Transaction");
        }

        [HttpPost]
        public IActionResult AddMoney(int persid, double tofill)
        {
            //Todo Get balance
            //Check Balance
            //if balance > amount than make transaction
            var person = _db.Persons.Where(k => k.Id == persid).FirstOrDefault();
            if (person != null)
            {
                if (tofill > 0)
                {
                    person.Balance += tofill;
                    Transaction tran = new();
                    tran.Date = DateTime.Now;
                    tran.PersonId = persid;
                    tran.TransactionType = "ჩარიცხვა";
                    tran.Amount = tofill;
                    _db.Transactions.Add(tran);
                    _db.SaveChanges();
                }
            }
            return RedirectToAction("Index", "Transaction");
        }

        [HttpPost]
        public ActionResult TransHistory(int persid)
        {

            ViewBag.data = _db.Transactions.Where(k => k.PersonId == persid).ToList();
            ViewBag.person = _db.Persons.Find(persid);
            return View();

        }
    }
}
