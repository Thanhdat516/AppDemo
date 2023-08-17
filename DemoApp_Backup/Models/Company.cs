namespace DemoApp.Models
{
    public class Company
    {
        private int companyid;

        private string name = string.Empty;

        private string phone = string.Empty;

        private string address = string.Empty;

        private ICollection<Employee>? employees;
        public int companyID { get => companyid; set => companyid = value; }
        public string Name { get => name; set => name = value; }
        public string Phone { get => phone; set => phone = value; }
        public string Address { get => address; set => address = value; }
        public ICollection<Employee>? Employees { get => employees; set => employees = value; }
    }
}
