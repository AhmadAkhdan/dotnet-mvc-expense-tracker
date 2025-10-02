// Kode ini mendefinisikan "cetakan" untuk data Kategori
namespace dotnet_mvc_expense_tracker.Models
{
    // Enum membatasi pilihan agar tidak salah ketik
    public enum TipeKategori { Income, Expense }
    public enum Status { Active, NonActive }

    public class Kategori
    {
        public int Id { get; set; } // Kunci unik untuk setiap kategori
        public TipeKategori Tipe { get; set; } // Tipe: Pemasukan atau Pengeluaran
        public string Nama { get; set; } // Nama kategori, misal: "Gaji", "Makanan"
        public string? Deskripsi { get; set; } // Deskripsi (boleh kosong)
        public Status Status { get; set; } // Status: Aktif atau Tidak Aktif
    }
}