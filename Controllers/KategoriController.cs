using Microsoft.AspNetCore.Mvc;
using dotnet_mvc_expense_tracker.Models;

namespace dotnet_mvc_expense_tracker.Controllers
{
    public class KategoriController : Controller
    {
        // DATA SEMENTARA (DUMMY) SEBELUM PAKAI DATABASE
        private static List<Kategori> _kategori = new List<Kategori>
        {
            new Kategori { Id = 1, Nama = "Gaji", Tipe = TipeKategori.Income, Status = Status.Active, Deskripsi = "Penerimaan gaji bulanan" },
            new Kategori { Id = 2, Nama = "Makanan", Tipe = TipeKategori.Expense, Status = Status.Active, Deskripsi = "Pengeluaran untuk makanan" },
            new Kategori { Id = 3, Nama = "Transportasi", Tipe = TipeKategori.Expense, Status = Status.Active, Deskripsi = "Bensin, parkir, tol" }
        };

        // === BAGIAN UNTUK MENAMPILKAN HALAMAN (GET) ===

        // GET: /Kategori
        public IActionResult Index()
        {
            return View(_kategori.OrderBy(k => k.Id).ToList());
        }

        // GET: /Kategori/Create
        public IActionResult Create()
        {
            return View();
        }

        // GET: /Kategori/Edit/5
        public IActionResult Edit(int id)
        {
            var model = _kategori.FirstOrDefault(k => k.Id == id);
            if (model == null) return NotFound();
            return View(model);
        }

        // GET: /Kategori/Details/5
        public IActionResult Details(int id)
        {
            var model = _kategori.FirstOrDefault(k => k.Id == id);
            if (model == null) return NotFound();
            return View(model);
        }

        // GET: /Kategori/Delete/5
        public IActionResult Delete(int id)
        {
            var model = _kategori.FirstOrDefault(k => k.Id == id);
            if (model == null) return NotFound();
            return View(model);
        }


        // === BAGIAN UNTUK MEMPROSES AKSI (POST) ===

        // POST: Kategori/Create
        // Method ini berjalan saat tombol 'Simpan' di form Create ditekan.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Tipe,Nama,Deskripsi,Status")] Kategori kategori)
        {
            if (ModelState.IsValid)
            {
                // Simulasi auto-increment ID
                kategori.Id = _kategori.Max(k => k.Id) + 1;
                _kategori.Add(kategori);
                return RedirectToAction(nameof(Index)); // Kembali ke halaman daftar
            }
            return View(kategori); // Jika data tidak valid, tampilkan form lagi
        }

        // POST: Kategori/Edit/5
        // Method ini berjalan saat tombol 'Update' di form Edit ditekan.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,Tipe,Nama,Deskripsi,Status")] Kategori kategori)
        {
            if (id != kategori.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var existingKategori = _kategori.FirstOrDefault(k => k.Id == id);
                if (existingKategori != null)
                {
                    // Update data yang ada di list
                    existingKategori.Nama = kategori.Nama;
                    existingKategori.Tipe = kategori.Tipe;
                    existingKategori.Deskripsi = kategori.Deskripsi;
                    existingKategori.Status = kategori.Status;
                }
                return RedirectToAction(nameof(Index)); // Kembali ke halaman daftar
            }
            return View(kategori); // Jika data tidak valid, tampilkan form lagi
        }

        // POST: Kategori/Delete/5
        // Method ini berjalan saat tombol 'Hapus' di halaman konfirmasi ditekan.
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var kategori = _kategori.FirstOrDefault(k => k.Id == id);
            if (kategori != null)
            {
                _kategori.Remove(kategori);
            }
            return RedirectToAction(nameof(Index)); // Kembali ke halaman daftar
        }
    }
}