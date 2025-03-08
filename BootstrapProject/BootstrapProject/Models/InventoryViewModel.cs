namespace BootstrapProject.Models
{
    public class InventoryViewModel
    {
        public int TotalCategories { get; set; }
        public int TotalProducts { get; set; }
        public int LowStockCount { get; set; }
        public List<Product> InventoryItems { get; set; }
    }
}