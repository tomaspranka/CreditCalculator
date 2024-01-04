using System.Collections.Generic;
using System.Linq;

namespace CreditCalculator.After;

public class CompanyRepository
{
    private readonly List<Company> _companies = new()
    {
        Company.RegularClient,
        Company.ImportantClient,
        Company.VeryImportantClient
    };

    public Company GetById(int companyId) 
    {
        return _companies.Single(c => c.Id == companyId);
    }
}
