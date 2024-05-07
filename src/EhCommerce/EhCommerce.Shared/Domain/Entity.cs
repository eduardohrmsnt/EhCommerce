namespace EhCommerce.Shared.Domain
{
    public abstract class Entity
    {
        public Guid Id { get; set; }

        protected void SetId(Guid id) => Id = id;

        protected abstract void Validate();
    }
}
