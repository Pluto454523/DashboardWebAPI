namespace Domain;

public class BaseEntity: IBaseEntity
{
    public DateTime CreatedTime { get; set; }
    public DateTime UpdatedTime { get; set; }
}