namespace SampleApp.Domain.Common;

public interface IModifiableEntity
{
    public DateTime ModifiedUtc { get; set; }
}
