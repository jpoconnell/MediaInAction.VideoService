namespace VideoService2.Domain.Entities;

public class SeriesAlias : BaseAuditableEntity
{
    public int SeriesId { get; set; }

    public string? IdType { get; set; }

    public string? IdValue { get; set; }
    

    private bool _done;
    public bool Done
    {
        get => _done;
        set
        {
            if (value && !_done)
            {
                AddDomainEvent(new TodoItemCompletedEvent(this));
            }

            _done = value;
        }
    }

    public TodoList List { get; set; } = null!;
}
