using System.Collections.Generic;
using System.Linq;

namespace CreditCalculator.After;

public class CustomerRepository
{
    private readonly List<Customer> _customer = [];
    public List<Customer> GetCustomers => _customer.ToList();
    public void AddCustomer(Customer customer) 
    {
        _customer.Add(customer);
    }
}
