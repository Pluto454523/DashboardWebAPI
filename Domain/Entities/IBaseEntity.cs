namespace Domain;

public interface IBaseEntity
{
    DateTime CreatedTime { get; set; }
    DateTime UpdatedTime { get; set; }
}