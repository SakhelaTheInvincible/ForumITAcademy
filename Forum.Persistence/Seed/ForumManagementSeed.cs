using Forum.Domain.topic;
using Forum.Domain.user;
using Forum.Persistence.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace Forum.Persistence.Seed
{
    public class ForumManagementSeed
    {
        public static async Task SeedUsersAndRolesAsync(IServiceProvider serviceProvider)
        {
            using (var serviceScope = serviceProvider.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<ForumManagementIdentityContext>()!;

                //Roles
                var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole<int>>>();

                

                if (!await roleManager.RoleExistsAsync("Administrator"))
                {
                    var AdminRole = new IdentityRole<int>("Administrator");
                    AdminRole.NormalizedName = "ADMINISTRATOR";
                    await roleManager.CreateAsync(AdminRole);
                }

                if (!await roleManager.RoleExistsAsync("User"))
                {
                    var UserRole = new IdentityRole<int>("User");
                    UserRole.NormalizedName = "USER";
                    await roleManager.CreateAsync(UserRole);
                }

                var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<User>>();
                string adminUserEmail = "admin@gmail.com";

                var adminUser = await userManager.FindByEmailAsync(adminUserEmail);
                if (adminUser == null)
                {
                    var newAdminUser = new User()
                    {
                        UserName = "admin",
                        NormalizedUserName = "ADMNIN",
                        Email = adminUserEmail,
                        NormalizedEmail = adminUserEmail.ToUpper(),
                        CreatedAt = DateTime.Now,
                        ModifiedAt = DateTime.Now
                    };
                    await userManager.CreateAsync(newAdminUser, "Password1!");
                    await userManager.AddToRoleAsync(newAdminUser, "Administrator");
                    await userManager.AddToRoleAsync(newAdminUser, "User");

                    var newUser1 = new User()
                    {
                        UserName = "gio",
                        NormalizedUserName = "GIO",
                        Email = "gio@gmail.com",
                        NormalizedEmail = "GIO@GMAIL.COM",
                        CreatedAt = DateTime.Now,
                        ModifiedAt = DateTime.Now
                    };
                    await userManager.CreateAsync(newUser1, "Password1!");
                    await userManager.AddToRoleAsync(newUser1, "User");


                    var newUser2 = new User()
                    {
                        UserName = "username",
                        NormalizedUserName = "USERNAME",
                        Email = "user@gmail.com",
                        NormalizedEmail = "USER@GMAIL.COM",
                        CreatedAt = DateTime.Now,
                        ModifiedAt = DateTime.Now
                    };
                    await userManager.CreateAsync(newUser2, "Password1!");
                    await userManager.AddToRoleAsync(newUser2, "User");


                    var topic = new Topic
                    {
                        AuthorId = newAdminUser.Id,
                        Title = "admin topic",
                        Content = "some content",
                        CreatedAt = DateTime.Now,
                        ModifiedAt = DateTime.Now

                    };

                    var topic2 = new Topic
                    {
                        AuthorId = newAdminUser.Id,
                        Title = "gio's content",
                        Content = "Content here2",
                        CreatedAt = DateTime.Now,
                        ModifiedAt = DateTime.Now

                    };

                    await context.Topic.AddAsync(topic);
                    await context.Topic.AddAsync(topic2);
                }

                await context.SaveChangesAsync();


            }
        }
    }
}
