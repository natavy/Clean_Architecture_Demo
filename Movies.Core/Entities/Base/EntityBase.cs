

namespace Movies.Core.Entities.Base
{
    public abstract class EntityBase<Tid> : IEntityBase<Tid>
    {
        public virtual Tid Id { get; protected set; }
    }
}
