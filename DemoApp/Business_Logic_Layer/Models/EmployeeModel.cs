namespace Business_Logic_Layer.Models
{
    public class EmployeeModel
    {
        private int employeeid;
        private string nameEmployee;
        private string phoneEmployee;
        private string addressEmployee;
        private int companyId;
        /*private Company company;*/

        public int EmployeeID { get => employeeid; set => employeeid = value; }
        public string NameEmployee { get => nameEmployee; set => nameEmployee = value; }
        public string PhoneEmployee { get => phoneEmployee; set => phoneEmployee = value; }
        public string AddressEmployee { get => addressEmployee; set => addressEmployee = value; }
        public int CompanyID { get => companyId; set => companyId = value; }
        /*public Company Company { get => company; set => company = value; }*/
    }
}
