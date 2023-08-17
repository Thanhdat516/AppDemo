using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace DemoApp.Models
{
    public class Employee
    {
        private int employeeid;
        private string name = string.Empty;
        private string phone = string.Empty;
        private string address = string.Empty;
        private int? companyid;
        private Company? company;

        public int employeeID { get => employeeid; set => employeeid = value; }
        public string Name { get => name; set => name = value; }
        public string Phone { get => phone; set => phone = value; }
        public string Address { get => address; set => address = value; }
        public int? companyID { get => companyid; set => companyid = value; }
        public Company? Company { get => company; set => company = value; }
    }
}
