namespace Business_Logic_Layer.Models
{
    public class CompanyModel
    {
        private int companyId;

        private string nameCompany;

        private string phoneCompany;

        private string addressCompany;

        /*        private ICollection<EmployeeModel> employees;*/
        public int CompanyID { get => companyId; set => companyId = value; }

        public string NameCompany { get => nameCompany; set => nameCompany = value; }

        public string PhoneCompany { get => phoneCompany; set => phoneCompany = value; }

        public string AddressCompany { get => addressCompany; set => addressCompany = value; }
        /*
                public ICollection<EmployeeModel> Employees { get => employees; set => employees = value; }*/
    }
}
