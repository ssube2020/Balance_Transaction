using Balance_Transaction.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Text.RegularExpressions;

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



        public static object aa = new object();
            
        
        [HttpPost]
        public IActionResult SubtractMoney(int personid, double totransac, string receiverPN)
        {

            lock (aa)
            {
                //Todo Get balance
                //Check Balance
                //if balance > amount than make transaction
                var person = _db.Persons.Where(k => k.Id == personid).FirstOrDefault();
                if (person != null)
                {
                    if ((person.Balance >= totransac) && totransac > 0)
                    {
                        string trimmedrec = String.Concat(receiverPN.Where(c => !Char.IsWhiteSpace(c)));
                        if (Regex.IsMatch(trimmedrec, @"^\d+$"))
                        {
                            var perstofill = _db.Persons.Where(k => k.PrivateNo == trimmedrec).SingleOrDefault();
                            if (perstofill != null)
                            {
                                person.Balance -= totransac;
                                perstofill.Balance += totransac;
                                Transaction tran = new();
                                tran.Date = DateTime.Now;
                                tran.PersonId = personid;
                                tran.TransactionType = "გადარიცხვა";
                                tran.Amount = totransac;
                                _db.Transactions.Add(tran);
                                _db.SaveChanges();
                                TempData["success"] = " თანხა გადაირიცხა წარმატებით ";
                            }
                            else
                            {
                                TempData["nulloerror"] = " ამ პირადი ნომრით მომხმარებელი არ არსებობს! ";
                            }
                        }
                        else
                        {
                            TempData["nulloerror"] = " შეიყვანეთ პირადი ნომერი სწორ ფორმატში! ";
                        }
                        
                    }
                    else if ((totransac == 0) || String.IsNullOrEmpty(totransac.ToString()))
                    {
                        TempData["nulloerror"] = " 0 თეთრის გადარიცხვა შეუძლებელია ";
                    }
                    else
                    {
                        TempData["BalanceError"] = "ანგარიშზე არარის საკმარისი თანხა";
                    }
                }
            }
            Thread.Sleep(1500);
            
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
                    TempData["fill"] = "ჩაგერიცხათ " + tofill + " ლარი";
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
