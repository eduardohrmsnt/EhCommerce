using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EhCommerce.Shared.Domain
{
    public abstract class Entity
    {
        protected Guid Id { get; set; }

        protected void SetId(Guid id) => Id = id;

        protected abstract void Validate();
    }
}
