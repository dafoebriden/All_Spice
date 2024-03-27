namespace All_Spice.Models
{
    public class Ingredient : IdAndTime
    {
        public string Name { get; set; }
        public string Quantity { get; set; }
        public int RecipieId { get; set; }
    }
}