namespace All_Spice.Models
{
    public class IdAndTime
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string CreatorId { get; set; }
        public Account Creator { get; set; }
    }
}