using Microsoft.AspNetCore.Mvc;
using dotnet_mvc_expense_tracker.Models;
using Microsoft.AspNetCore.Mvc.Rendering; // <-- Penting untuk SelectList

namespace dotnet_mvc_expense_tracker.Controllers
{
    public class BudgetController : Controller
    {
        // DATA DUMMY UNTUK KATEGORI (Agar bisa dipilih di form Budget)
        // Catatan: Idealnya, data ini diambil dari satu sumber yang sama dengan KategoriController
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
                StartDate = new DateTime(2025, 1, 1),
                EndDate = new DateTime(2025, 1, 31),
                KategoriId = 2,
                Kategori = _kategori.First(k => k.Id == 2), // Hubungkan ke Kategori Makanan
                Status = Status.Active,
                IsRepeat = true,
                Deskripsi = "Budget untuk makan sehari-hari"
            }
        };


        // === BAGIAN UNTUK MENAMPILKAN HALAMAN (GET) ===

        // GET: Budget
        public IActionResult Index()
        {
            return View(_budgets.OrderBy(b => b.Id).ToList());
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

        // === BAGIAN UNTUK MEMPROSES AKSI (POST) ===

        // POST: Budget/Create
        // Method ini berjalan saat tombol 'Simpan' ditekan
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("KategoriId,Nama,Deskripsi,TotalBudget,StartDate,EndDate,IsRepeat,Status")] Budget budget)
        {
            // Cek apakah data yang dikirim valid (misal: field yang wajib sudah diisi)
            if (ModelState.IsValid)
            {
                // Tambahkan relasi ke Kategori berdasarkan KategoriId yang dipilih
                budget.Kategori = _kategori.FirstOrDefault(k => k.Id == budget.KategoriId);
                // Simulasi ID baru
                budget.Id = _budgets.Any() ? _budgets.Max(b => b.Id) + 1 : 1;
                _budgets.Add(budget);
                return RedirectToAction(nameof(Index)); // Kembali ke halaman daftar budget
            }
            // Jika data tidak valid, kirim kembali daftar Kategori ke View dan tampilkan form lagi
            ViewData["KategoriList"] = new SelectList(_kategori.Where(k => k.Tipe == TipeKategori.Expense), "Id", "Nama", budget.KategoriId);
            return View(budget);
        }

        // POST: Budget/Edit/5
        // Method ini berjalan saat tombol 'Update' ditekan
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,KategoriId,Nama,Deskripsi,TotalBudget,StartDate,EndDate,IsRepeat,Status")] Budget budget)
        {
            if (id != budget.Id) return NotFound();

            if (ModelState.IsValid)
            {
                var existingBudget = _budgets.FirstOrDefault(b => b.Id == id);
                if (existingBudget != null)
                {
                    // Update semua field dari data yang ada dengan data baru dari form
                    existingBudget.KategoriId = budget.KategoriId;
                    existingBudget.Kategori = _kategori.FirstOrDefault(k => k.Id == budget.KategoriId);
                    existingBudget.Nama = budget.Nama;
                    existingBudget.Deskripsi = budget.Deskripsi;
                    existingBudget.TotalBudget = budget.TotalBudget;
                    existingBudget.StartDate = budget.StartDate;
                    existingBudget.EndDate = budget.EndDate;
                    existingBudget.IsRepeat = budget.IsRepeat;
                    existingBudget.Status = budget.Status;
                }
                return RedirectToAction(nameof(Index)); // Kembali ke halaman daftar
            }
            ViewData["KategoriList"] = new SelectList(_kategori.Where(k => k.Tipe == TipeKategori.Expense), "Id", "Nama", budget.KategoriId);
            return View(budget);
        }

        // POST: Budget/Delete/5
        // Method ini berjalan saat tombol 'Hapus' di halaman konfirmasi ditekan
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var budget = _budgets.FirstOrDefault(b => b.Id == id);
            if (budget != null)
            {
                _budgets.Remove(budget);
            }
            return RedirectToAction(nameof(Index)); // Kembali ke halaman daftar
        }
    }
}