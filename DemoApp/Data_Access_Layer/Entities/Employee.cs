namespace Data_Access_Layer.Entities
{
    public class Employee
    {
        private int employeeId;
        private string nameEmployee = string.Empty;
        private string phoneEmployee = string.Empty;
        private string addressEmployee = string.Empty;
        private int? companyId;
        private Company? company;

        public int EmployeeID { get => employeeId; set => employeeId = value; }
        public string NameEmployee { get => nameEmployee; set => nameEmployee = value; }
        public string PhoneEmployee { get => phoneEmployee; set => phoneEmployee = value; }
        public string AddressEmployee { get => addressEmployee; set => addressEmployee = value; }
        public int? CompanyID { get => companyId; set => companyId = value; }
        public Company? Company { get => company; set => company = value; }
    }
}
