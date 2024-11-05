namespace Events.Application.DTO.Participant
{
    public class UpdateParticipantDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime BirthDate { get; set; }
        public string Email { get; set; }
    }
}