using Microsoft.AspNetCore.Identity;

namespace fs_mvc_with_auth_2025_07_01.Models
{
    public class DoctorPatientModel
    {
        public string DoctorId { get; set; }
        public IdentityUser Doctor { get; set; }

        public string PatientId { get; set; }
        public IdentityUser Patient { get; set; }

    }


}
