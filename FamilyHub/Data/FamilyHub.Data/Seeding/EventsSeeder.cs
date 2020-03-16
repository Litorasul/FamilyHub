namespace FamilyHub.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using FamilyHub.Data.Models.Planner;

    public class EventsSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Events.Any())
            {
                return;
            }

            var eventOne = new Event
            {
                Title = "Laundry",
                Description = "Wash all pillows, sheets, towels.",
                CreatorId = "5176a3b7-02f9-4ce7-aeca-f349e065eb60",
                StartTime = DateTime.Now.AddDays(3),
                AssignedUsers = new List<UserEvent>
                {
                    new UserEvent
                    {
                        UserId = "5176a3b7-02f9-4ce7-aeca-f349e065eb60",
                        EventId = 1,
                    },
                    new UserEvent
                    {
                        UserId = "94eda100-c39b-4afc-9e0b-32c9daf29e4d",
                        EventId = 1,
                    },
                },
            };

            var eventTwo = new Event
            {
                Title = "Parent meeting",
                Description = "Parent meeting at Ivan school.",
                CreatorId = "5176a3b7-02f9-4ce7-aeca-f349e065eb60",
                StartTime = DateTime.Now.AddDays(4),
                AssignedUsers = new List<UserEvent>
                {
                    new UserEvent
                    {
                        UserId = "5176a3b7-02f9-4ce7-aeca-f349e065eb60",
                        EventId = 2,
                    },
                    new UserEvent
                    {
                        UserId = "94eda100-c39b-4afc-9e0b-32c9daf29e4d",
                        EventId = 2,
                    },
                    new UserEvent
                    {
                        UserId = "68b6b4eb-53da-43f3-b61c-8e36ffe7e3d9",
                        EventId = 2,
                    },
                },
            };

            await dbContext.Events.AddAsync(eventOne);
            await dbContext.Events.AddAsync(eventTwo);

        }
    }
}