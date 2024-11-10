using Events.Domain.Enums;
using System.Text.Json.Serialization;

namespace Events.Application.DTO.Participant
{
    public class ParticipantDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime BirthDate { get; set; }
        public DateTime RegistrationDate { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public byte[] PasswordSalt { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public Role Role { get; set; }
    }
}
