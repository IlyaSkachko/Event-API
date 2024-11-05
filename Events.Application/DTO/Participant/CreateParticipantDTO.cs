namespace Events.Application.DTO.Participant
{
    public class CreateParticipantDTO
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime BirthDate { get; set; }
        public DateTime RegistrationDate { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
