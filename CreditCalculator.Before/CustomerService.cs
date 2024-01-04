using System;
using System.Globalization;
using System.Linq.Expressions;
using System.Runtime.ConstrainedExecution;

namespace CreditCalculator.Before;
public class CustomerService
{
    public bool AddCustomer(
        string firstName,
        string lastName,
        string email,
        DateTime dateOdBirth,
        int companyId)
    {
        if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName)) 
        {
            return false;
        }
        
        if (!email.Contains('@') && !email.Contains('.'))
        {
            return false;
        }
        
        var now = DateTime.Now;
        var age = now.Year - dateOdBirth.Year;
        
        if (now.Month < dateOdBirth.Month || 
            now.Month == dateOdBirth.Month && 
            now.Day < dateOdBirth.Day) 
        {
            age--;
        }
        
        if (age <21)
        {
            return false;
        }
        var companyRepository = new CompanyRepository();
        var company = companyRepository.GetById(companyId);

        var customer = new Customer
        {
            Company = company,
            DateOfBirth = dateOdBirth,
            EmailAddress = email,
            FirstName = firstName,
            LastName = lastName
        };

        if (company.Type == "VeryImportantClient")
        {
            customer.HasCreditLimit = false;
        }
        else if (company.Type == "ImportantClient")
        {
            customer.HasCreditLimit = true;
            var creditService = new CustomerCreditServiceClient();
            
            var credirLimit = creditService.GetCreditLimit(
                customer.FirstName,
                customer.LastName,
                customer.DateOfBirth);

            credirLimit *= 2;
            customer.CreditLimit = credirLimit;

        }
        else
        {
            customer.HasCreditLimit = true;
            var creditService = new CustomerCreditServiceClient();

            var creditLimit = creditService.GetCreditLimit(
                customer.FirstName,
                customer.LastName,
                customer.DateOfBirth);
            
            customer.CreditLimit = creditLimit;
        }

        if (customer.HasCreditLimit && customer.CreditLimit < 500) 
        {
            return false;
        }

        var customerRepository = new CustomerRepository();
        customerRepository.AddCustomer(customer);

        return true;
    }
}
