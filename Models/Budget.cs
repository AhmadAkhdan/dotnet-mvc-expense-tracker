// Kode ini mendefinisikan "cetakan" untuk data Budget
using System.ComponentModel.DataAnnotations.Schema;

namespace dotnet_mvc_expense_tracker.Models
{
    public class Budget
    {
        public int Id { get; set; }
        public int KategoriId { get; set; } // Ini akan terhubung ke Id di tabel Kategori
        public virtual Kategori? Kategori { get; set; }
        public string Nama { get; set; }
        public string? Deskripsi { get; set; }
        public decimal TotalBudget { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsRepeat { get; set; }
        public Status Status { get; set; }
    }
}