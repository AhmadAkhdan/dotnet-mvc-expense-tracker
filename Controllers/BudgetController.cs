using Microsoft.AspNetCore.Mvc;
using dotnet_mvc_expense_tracker.Models;
using Microsoft.AspNetCore.Mvc.Rendering; // <-- Penting untuk SelectList

namespace dotnet_mvc_expense_tracker.Controllers
{
    public class BudgetController : Controller
    {
        // DATA DUMMY UNTUK KATEGORI (Agar bisa dipilih di form Budget)
        private static List<Kategori> _kategori = new List<Kategori>
        {
            new Kategori { Id = 1, Nama = "Gaji", Tipe = TipeKategori.Income, Status = Status.Active },
            new Kategori { Id = 2, Nama = "Makanan", Tipe = TipeKategori.Expense, Status = Status.Active },
            new Kategori { Id = 3, Nama = "Transportasi", Tipe = TipeKategori.Expense, Status = Status.Active }
        };

        // DATA DUMMY UNTUK BUDGET
        private static List<Budget> _budgets = new List<Budget>
        {
            new Budget
            {
                Id = 1,
                Nama = "Budget Makanan Bulanan",
                TotalBudget = 1500000,
                StartDate = new DateTime(2025, 10, 1),
                EndDate = new DateTime(2025, 10, 31),
                KategoriId = 2,
                Kategori = _kategori.First(k => k.Id == 2), // Hubungkan ke Kategori Makanan
                Status = Status.Active,
                IsRepeat = true,
                Deskripsi = "Budget untuk makan sehari-hari"
            },
            new Budget
            {
                Id = 2,
                Nama = "Budget Bensin Oktober",
                TotalBudget = 500000,
                StartDate = new DateTime(2025, 10, 1),
                EndDate = new DateTime(2025, 10, 31),
                KategoriId = 3,
                Kategori = _kategori.First(k => k.Id == 3), // Hubungkan ke Kategori Transportasi
                Status = Status.Active,
                IsRepeat = false
            }
        };

        // GET: Budget
        public IActionResult Index()
        {
            return View(_budgets);
        }

        // GET: Budget/Create
        public IActionResult Create()
        {
            // Kirim daftar kategori ke View agar bisa ditampilkan di dropdown
            ViewData["KategoriList"] = new SelectList(_kategori.Where(k => k.Tipe == TipeKategori.Expense), "Id", "Nama");
            return View();
        }

        // GET: Budget/Edit/5
        public IActionResult Edit(int id)
        {
            var model = _budgets.FirstOrDefault(b => b.Id == id);
            if (model == null) return NotFound();

            // Kirim daftar kategori dan pilih kategori yang sesuai dengan data budget ini
            ViewData["KategoriList"] = new SelectList(_kategori.Where(k => k.Tipe == TipeKategori.Expense), "Id", "Nama", model.KategoriId);
            return View(model);
        }

        // GET: Budget/Details/5
        public IActionResult Details(int id)
        {
            var model = _budgets.FirstOrDefault(b => b.Id == id);
            if (model == null) return NotFound();
            return View(model);
        }

        // GET: Budget/Delete/5
        public IActionResult Delete(int id)
        {
            var model = _budgets.FirstOrDefault(b => b.Id == id);
            if (model == null) return NotFound();
            return View(model);
        }
    }
}