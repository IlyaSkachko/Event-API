using Events.Domain.Enums;
using System.Text.Json.Serialization;

namespace Events.Application.DTO.Participant
{
    public class UpdateParticipantDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime BirthDate { get; set; }
        public string Email { get; set; }
        public string RefreshToken {  get; set; } = string.Empty;
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public Role Role { get; set; }
    }
}
