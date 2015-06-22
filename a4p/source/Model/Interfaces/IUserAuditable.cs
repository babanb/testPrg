namespace Model.Interfaces
{
    public interface IUserAuditable : IAuditable
    {
        int UserId { get; }
    }
}