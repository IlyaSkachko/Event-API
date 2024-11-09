namespace Events.Application.DTO.Token
{
    public class TokenDTO
    {
        public string Access {  get; set; }
        public DateTime AccessExpires { get; set; }
        public string Refresh {  get; set; }
        public DateTime RefreshExpires { get; set; }
    }
}
