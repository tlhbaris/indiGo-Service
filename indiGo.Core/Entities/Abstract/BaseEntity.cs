namespace indiGo.Core.Entities.Abstract;

public abstract class BaseEntity<T> where T : IEquatable<T>
{
    public T Id { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

}