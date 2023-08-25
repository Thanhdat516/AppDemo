namespace Data_Access_Layer.Entities
{
    public class Company
    {
        private int companyID;

        private string nameCompany;
        private string phoneCompany;
        private string addressCompany;

        private ICollection<Employee> employees;

        public int CompanyID { get => companyID; set => companyID = value; }
        public string NameCompany { get => nameCompany; set => nameCompany = value; }
        public string PhoneCompany { get => phoneCompany; set => phoneCompany = value; }
        public string AddressCompany { get => addressCompany; set => addressCompany = value; }

        public ICollection<Employee> Employees { get => employees; set => employees = value; }
    }
}
