namespace All_Spice.Models
{
    public class Recipe : IdAndTime
    {
        public string Title { get; set; }
        public string Instructions { get; set; }
        public string Img { get; set; }
        public string Category { get; set; }
    }

}