using System.ComponentModel.DataAnnotations.Schema;

namespace Galaxy.Infrastructure
{
    public interface IObjectState
    {
        [NotMapped]
        ObjectState ObjectState { get; }

        void SyncObjectState(ObjectState objectState);
    }
}
