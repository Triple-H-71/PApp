namespace PApp.Models
{
    public class Sale
    {
        public int Id { get; set; }
        public int ProductId { get; set; }  
        public int SalesPersonId { get; set; }  
        public int CustomerId { get; set; } 
        public string SalesDate { get; set; }
    }
}
