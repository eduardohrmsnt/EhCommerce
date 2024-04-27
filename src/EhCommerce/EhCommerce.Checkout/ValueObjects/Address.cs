namespace EhCommerce.Checkout.ValueObjects
{
    public class Address
    {
        public Address(string country,
                       string state,
                       string city,
                       string street, 
                       string buildingNumber,
                       string description,
                       string zipCode)
        {
            Country = country;
            State = state;
            City = city;
            Street = street;
            BuildingNumber = buildingNumber;
            Description = description;
            ZipCode = zipCode;
        }

        public string Street { get; }
        public string State { get; }
        public string City { get; }
        public string Country { get;  }
        public string BuildingNumber { get; }
        public string ZipCode { get; }
        public string Description { get; }
    }
}
