namespace DTO.Member
{
    public class MemberDTO
    {
        public int Id { get; set; }
        public string NickName { get; set; } = string.Empty;
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
    }
}
