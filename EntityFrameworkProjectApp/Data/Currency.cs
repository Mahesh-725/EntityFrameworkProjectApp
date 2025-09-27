namespace EntityFrameworkProjectApp.Data
{
    public class Currency
    {
        public int Id { get; set; }
        public String? Title { get; set; }
        public String? Description { get; set; }

        public ICollection<BookPrice>? BookPrices { get; set; }
    }
}
