using Microsoft.AspNetCore.Identity;

namespace fs_mvc_with_auth_2025_07_01.Data
{
    public static class SeedData
    {
        public static async Task Initialize(IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var services = scope.ServiceProvider;
                var context = services.GetRequiredService<ApplicationDbContext>();

                var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

                var roles = new string[] { "Patient", "Doctor", "Admin" };

                foreach (var role in roles)
                {

                    if(! await roleManager.RoleExistsAsync(role))
                    {
                        await roleManager.CreateAsync(new IdentityRole(role));
                    }
                }

            }
        }
    }
}
