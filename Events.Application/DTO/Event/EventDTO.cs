namespace Events.Application.DTO.Event
{
    public class EventDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime EventDate { get; set; }
        public string Location { get; set; }
        public string CategoryName { get; set; }
        public int MaxParticipants { get; set; }
        public byte[]? Image { get; set; }
    }
}
