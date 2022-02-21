using Balance_Transaction.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace Balance_Transaction.Controllers
{
    public class PeopleController : Controller
    {
        private readonly AppDbContext _db;

        public PeopleController(AppDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            return View(_db.Persons.ToList());
        }

        //[HttpPost]
        //public IActionResult Transact(int id)
        //{
        //    var person = _db.Persons.SingleOrDefault(x => x.Id == id);
        //    return View(person);  
        //}

        [HttpPost]
        public IActionResult SubtractMoney(int personid, double totransac)
        {
            //Todo Get balance
            //Check Balance
            //if balance > amount than make transaction
            var person = _db.Persons.Where(k=>k.Id == personid).FirstOrDefault();
            if (person != null)
            {
                if ((person.Balance >= Convert.ToDouble(totransac)) && (Convert.ToDouble(totransac) > 0))
                {
                    person.Balance -= Convert.ToInt32(totransac);
                    Transaction tran = new();
                    tran.Date = DateTime.Now;
                    tran.PersonId = personid;
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
            return RedirectToAction("Index","People");
        }

        [HttpPost]
        public IActionResult AddMoney(int persid, string tofill)
        {
            //Todo Get balance
            //Check Balance
            //if balance > amount than make transaction
            var person = _db.Persons.Where(k => k.Id == persid).FirstOrDefault();
            if (person != null)
            {
                if (Convert.ToInt32(tofill) > 0)
                {
                    person.Balance += Convert.ToInt32(tofill);
                    Transaction tran = new();
                    tran.Date = DateTime.Now;
                    tran.PersonId = persid;
                    _db.Transactions.Add(tran);
                    _db.SaveChanges();
                }
            }
            return RedirectToAction("Index", "People");
        }

        [HttpPost]
        public ActionResult TransHistory(int persid)
        {

            ViewBag.data = _db.Transactions.Where(k => k.PersonId == persid).ToList();
            return View(ViewBag.data);
            

        }
    }
}
