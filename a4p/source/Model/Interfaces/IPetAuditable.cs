namespace Model.Interfaces
{
    public interface IPetAuditable : IAuditable
    {
        int PetId { get; }
    }
}