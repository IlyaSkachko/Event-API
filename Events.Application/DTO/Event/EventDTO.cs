namespace Events.Application.DTO.Event
{
    public class EventDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime EventDate { get; set; }
        public string Location { get; set; }
        public int CategoryId { get; set; }
        public int MaxParticipants { get; set; }
        public string? Image { get; set; }
    }
}
