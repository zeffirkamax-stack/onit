using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography;

namespace Laba3.Models
{
    public class Emp_Prof
    {
        public int Id { get; set; }
        public int EmployeeID { get; set; }
        [ForeignKey("EmployeeID")]
        public Employee Employee { get; set; }
        public int ProfessionID { get; set; }
        [ForeignKey("ProfessionID")]
        public Profession Profession { get; set; }
    }
}
