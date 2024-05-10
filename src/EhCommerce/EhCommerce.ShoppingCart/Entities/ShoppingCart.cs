using EhCommerce.Shared.Domain;
using EhCommerce.Shared.Validator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EhCommerce.ShoppingCart.Entities
{
    public sealed class ShoppingCart : Entity, IAggregateRoot
    {
        public ShoppingCart(Guid clientId)
        {
            ClientId = clientId;
            _products = new List<Product>();
            Validate();
        }

        public decimal TotalNetPrice => _products.Sum(p => p.NetPrice);

        public decimal TotalGrossPrice => _products.Sum(p => p.GrossPrice);

        public int TotalQuantity => _products.Sum(p => p.Quantity);

        public Guid ClientId { get; }

        public IReadOnlyCollection<Product> Products => _products.AsReadOnly();

        private List<Product> _products;

        public void AddProduct(Product product)
        {
            _products.Add(product);
        }

        protected override void Validate()
        {
            Validator.Contract.ShouldNotBeEmpty(nameof(ClientId), ClientId).Validate();
        }
    }
}
