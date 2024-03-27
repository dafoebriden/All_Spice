namespace All_Spice.Models
{
    public class Recipe : IdAndTime
    {
        public string Title { get; set; }
        public int Instructions { get; set; }
        public string Image { get; set; }
        public string Category { get; set; }
    }

}