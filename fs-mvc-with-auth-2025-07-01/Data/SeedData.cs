using fs_mvc_with_auth_2025_07_01.Data.Migrations;
using fs_mvc_with_auth_2025_07_01.Models;
using Microsoft.AspNetCore.Identity;

namespace fs_mvc_with_auth_2025_07_01.Data
{

    public static class HealthAppRoles
    {
        public const string ADMIN = "Admin";
        public const string DOCTOR = "Doctor";
        public const string PATIENT = "Patient";
    }


    public static class SeedData
    {
        public static async Task Initialize(IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var services = scope.ServiceProvider;
                var context = services.GetRequiredService<ApplicationDbContext>();

                var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
                var userManager = services.GetRequiredService<UserManager<IdentityUser>>();
                // use constants so you don't risk mispelling, and can reuse easily
                var roles = new string[] { HealthAppRoles.PATIENT, HealthAppRoles.DOCTOR, HealthAppRoles.ADMIN };

                foreach (var role in roles)
                {

                    if (!await roleManager.RoleExistsAsync(role))
                    {
                        await roleManager.CreateAsync(new IdentityRole(role));
                    }
                }


                string adminUserName = "admin@healthapp.com";
                string adminUserEmail = "admin@healthapp.com";
                string genericPassword = "Letmein01*";


                if (await userManager.FindByEmailAsync(adminUserEmail) == null)
                {

                    var user = new IdentityUser { UserName = adminUserName, Email = adminUserEmail, EmailConfirmed = true };
                    await userManager.CreateAsync(user, genericPassword);
                    await userManager.AddToRoleAsync(user, HealthAppRoles.ADMIN);
                }

                string doctor = "doctor01@healthapp.com";

                if (await userManager.FindByEmailAsync(doctor) == null)
                {
                    var userDoctor = new IdentityUser { UserName = doctor, Email = doctor, EmailConfirmed = true };
                    await userManager.CreateAsync(userDoctor, genericPassword);
                    await userManager.AddToRoleAsync(userDoctor, HealthAppRoles.DOCTOR);


                    string patient = "patient01@healthapp.com";
                    if (await userManager.FindByEmailAsync(patient) == null)
                    {
                        var userPatient = new IdentityUser { UserName = patient, Email = patient, EmailConfirmed = true };
                        await userManager.CreateAsync(userPatient, genericPassword);
                        await userManager.AddToRoleAsync(userPatient, HealthAppRoles.PATIENT);

                        var doctorPatient = new DoctorPatientModel
                        {
                            DoctorId = userDoctor.Id,
                            PatientId = userPatient.Id
                        };

                        context.DoctorPatient.Add(doctorPatient);

                        await context.SaveChangesAsync();


                    }



                } // end of creating doctor and creating and associating patient 

            }
        }
    }
}
