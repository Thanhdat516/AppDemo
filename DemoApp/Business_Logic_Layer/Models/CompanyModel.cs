namespace Business_Logic_Layer.Models
{
    public class CompanyModel
    {
        private int companyid;

        private string nameCompany;

        private string phoneCompany;

        private string addressCompany;

        /*        private ICollection<EmployeeModel> employees;*/
        public int CompanyID { get => companyid; set => companyid = value; }

        public string NameCompany { get => nameCompany; set => nameCompany = value; }

        public string PhoneCompany { get => phoneCompany; set => phoneCompany = value; }

        public string AddressCompany { get => addressCompany; set => addressCompany = value; }
        /*
                public ICollection<EmployeeModel> Employees { get => employees; set => employees = value; }*/
    }
}
