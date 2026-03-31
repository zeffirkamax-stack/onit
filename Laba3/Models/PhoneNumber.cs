using System.ComponentModel.DataAnnotations.Schema;

namespace Laba3.Models
{
    public class PhoneNumber
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public int EmployeeID { get; set; }
        [ForeignKey("EmployeeID")]
        public Employee Employee { get; set; }
    }
}
