using Absa.Domain.Entities;
using Absa.Domain.ValueObjects;
using Absa.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Absa.Infrastructure.Persistence
{
    public static class ApplicationDbContextSeed
    {
        public static async Task SeedDefaultUserAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            var administratorRole = new IdentityRole("Administrator");

            if (roleManager.Roles.All(r => r.Name != administratorRole.Name))
            {
                await roleManager.CreateAsync(administratorRole);
            }

            var administrator = new ApplicationUser { UserName = "administrator@localhost", Email = "administrator@localhost" };

            if (userManager.Users.All(u => u.UserName != administrator.UserName))
            {
                await userManager.CreateAsync(administrator, "Administrator1!");
                await userManager.AddToRolesAsync(administrator, new[] { administratorRole.Name });
            }
        }

        public static async Task SeedSampleDataAsync(ApplicationDbContext context)
        {
            if (!context.PhoneBooks.Any())
            {
                context.PhoneBooks.Add(new PhoneBook
                {
                    Name = "IT Department",
                    PhoneBookEntries = {
                        new PhoneBookEntry { Name = "Ayomide Fajobi", Number = "0730864922" },
                        new PhoneBookEntry { Name = "Elon Musk", Number = "0730864923" },
                        new PhoneBookEntry { Name = "Jeff Bezos", Number = "07308654393" },
                        new PhoneBookEntry { Name = "Riley Freeman", Number = "0730869832" },
                     }
                });

                context.PhoneBooks.Add(new PhoneBook
                {
                    Name = "HR Departmennt",
                    PhoneBookEntries = {
                        new PhoneBookEntry { Name = "Huey Freeman", Number = "0730860932" },
                        new PhoneBookEntry { Name = "Uncle Ruckus", Number = "07308645832" },
                        new PhoneBookEntry { Name = "Tom Dubois", Number = "0730860987" },
                        new PhoneBookEntry { Name = "Robert FreeMan", Number = "0730861093" },
                        new PhoneBookEntry { Name = "Gin Rummy", Number = "0730864538" },
                        new PhoneBookEntry { Name = "Sarah Dubois", Number = "0730869084" },
                     }
                });

                context.PhoneBooks.Add(new PhoneBook
                {
                    Name = "Maintenance Departmennt",
                    PhoneBookEntries = {
                        new PhoneBookEntry { Name = "Morthy Smith", Number = "0730866576" },
                        new PhoneBookEntry { Name = "Rick Sanchez", Number = "0730866393" },
                        new PhoneBookEntry { Name = "Summer Smith", Number = "0730869043" },
                        new PhoneBookEntry { Name = "Jerry Smith", Number = "0730864922" },
                        new PhoneBookEntry { Name = "Beth Smith", Number = "0730868854" },
                        new PhoneBookEntry { Name = "Bird Person", Number = "0730864444" },
                        new PhoneBookEntry { Name = "Scary Terry", Number = "0730860988" },
                        new PhoneBookEntry { Name = "Toby Matthews", Number = "0730864455" },
                        new PhoneBookEntry { Name = "Xenon Bloom", Number = "0730865654" },
                        new PhoneBookEntry { Name = "Frank Palicky", Number = "0730866565" },
                        new PhoneBookEntry { Name = "Glexo Slimslon", Number = "0730867676" },

                     }
                });
                await context.SaveChangesAsync();
            }
            // Seed, if necessary
            if (!context.TodoLists.Any())
            {
                context.TodoLists.Add(new TodoList
                {
                    Title = "Shopping",
                    Colour = Colour.Blue,
                    Items =
                    {
                        new TodoItem { Title = "Apples", Done = true },
                        new TodoItem { Title = "Milk", Done = true },
                        new TodoItem { Title = "Bread", Done = true },
                        new TodoItem { Title = "Toilet paper" },
                        new TodoItem { Title = "Pasta" },
                        new TodoItem { Title = "Tissues" },
                        new TodoItem { Title = "Tuna" },
                        new TodoItem { Title = "Water" }
                    }
                });

                await context.SaveChangesAsync();
            }
        }
    }
}
