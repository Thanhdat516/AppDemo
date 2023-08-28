using Data_Access_Layer.Data;
using Data_Access_Layer.Entities;

namespace Data_Access_Layer.Repository
{
    public class CompanyRepository : GenericRepository<Company>, ICompanyRepository
    {
        public CompanyRepository(DataContext context) : base(context) { }

        /*private readonly DataContext _context;

        public CompanyRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<List<Company>> GetCompanies()
        {
            return await _context.Companies.ToListAsync();
        }

        public async Task<List<Company>> CreateCompany(Company company)
        {
            _context.Companies.Add(company);
            await _context.SaveChangesAsync();

            return await _context.Companies.ToListAsync();
        }

        public async Task<List<Company>> DeleteCompany(int companyID)
        {
            var dbCompany = await _context.Companies.FindAsync(companyID);
            _context.Companies.Remove(dbCompany);
            await _context.SaveChangesAsync();

            return await _context.Companies.ToListAsync();
        }


        public async Task<List<Company>> UpdateCompany(Company company)
        {
            var dbCompany = await _context.Companies.FindAsync(company.CompanyID);
            dbCompany.NameCompany = company.NameCompany;
            dbCompany.PhoneCompany = company.PhoneCompany;
            dbCompany.AddressCompany = company.AddressCompany;

            await _context.SaveChangesAsync();

            return await _context.Companies.ToListAsync();
        }

        public Task<IEnumerable<Company>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task Add(Company entity)
        {
            throw new NotImplementedException();
        }

        public Task Update(Company entity)
        {
            throw new NotImplementedException();
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }*/

        /*        public async Task<String> GetNameCompany(int companyID)
                {
                    var dbCompany = await _context.Companies.FindAsync(companyID);
                    return dbCompany.NameCompany;
                }*/
    }
}
