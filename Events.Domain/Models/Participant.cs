namespace Events.Domain.Models
{
    public class Participant
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname {  get; set; }
        public DateTime BirthDate { get; set; }
        public DateTime RegistrationDate {  get; set; }
        public string Email {  get; set; }
        public string? Password {  get; set; }
        public byte[]? PasswordSalt { get; set; }
        public List<EventParticipant> EventParticipants { get; set; }
        public string RefreshToken { get; set; } = string.Empty;
    }
}
