using System.ComponentModel.DataAnnotations;

namespace Database.Models
{
    public class Experience
    {
        public Experience()
        {
        }
        [Key]
        public int ExpericenceId { get; set; }

        public int ApplicantId { get; set; }
        public virtual Applicant Applicant { get; set; }

        public String CompanyName { get; set; }
        public String Designation { get; set; }
        [Range(1, 25, ErrorMessage = "Year Must be between 1 and 25")]
        [Required]
        public String YearsWorked { get; set; }

     
    }
}
