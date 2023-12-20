using Demo.Infrastructure.Ef;
using Demo.Infrastructure.Ef.Model;



namespace Demo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Hello!");

            FakeDataGenerator.Initialize();
            using (var dbContext = new DemoDbContext())
            {
                var users = dbContext.Users.ToList();
                var ticketBooks = dbContext.TicketBooks.ToList();
                var tickets = dbContext.Tickets.ToList();
                var controllers = dbContext.Controllers.ToList();
            }

            Console.ReadLine();

            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            //builder.Services.AddDbContext<DemoDbContext>(options => options.UseInMemoryDatabase(databaseName: "CityGuide"));
            //builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            //builder.Services.AddEndpointsApiExplorer();
            //builder.Services.AddSwaggerGen();
            

            //var app = builder.Build();

            //// Configure the HTTP request pipeline.
            //if (app.Environment.IsDevelopment())
            //{
            //    app.UseSwagger();
            //    app.UseSwaggerUI();
            //}

            //app.UseAuthorization();


            //app.MapControllers();

            //app.Run();
        }
    }

    public class FakeDataGenerator
    {
        public static void Initialize()
        {
            using (var context = new DemoDbContext())
            {
                TicketDb[] tickets = {
                    // Valids
                    new TicketDb() { Id = new Guid("f38dad76-173a-47ce-a15d-1de4473d1a28"), IssueDate=new DateTimeOffset(new DateTime(2023,11,20)) },
                    new TicketDb() { Id = new Guid("af2a45d4-2b2c-46fd-b615-293ab5ce510e"), IssueDate=new DateTimeOffset(new DateTime(2023,11,20)) },
                    new TicketDb() { Id = new Guid("b3ebbbd2-def6-4ac9-8916-8f3c9a97894b"), IssueDate=new DateTimeOffset(new DateTime(2023,11,20)) },
                    new TicketDb() { Id = new Guid("841495d9-9482-47ad-80f6-b61252306f1f"), IssueDate=new DateTimeOffset(new DateTime(2023,11,20)) },
                    new TicketDb() { Id = new Guid("03ab3ec4-07d2-4d0b-8120-83d6177240ea"), IssueDate=new DateTimeOffset(new DateTime(2023,11,20)) },
                    new TicketDb() { Id = new Guid("3041017d-748e-4f7b-811e-adece42d00e4"), IssueDate=new DateTimeOffset(new DateTime(2023,11,20)) },
                    new TicketDb() { Id = new Guid("4b5f1426-aa80-469e-a2dd-0ca4fe9b76e9"), IssueDate=new DateTimeOffset(new DateTime(2023,11,20)) },
                    new TicketDb() { Id = new Guid("a696b8c0-2ed5-4475-8f1a-757b5082e14e"), IssueDate=new DateTimeOffset(new DateTime(2023,11,20)) },
                    new TicketDb() { Id = new Guid("65135283-4017-4897-948f-3e5871e0f2e6"), IssueDate=new DateTimeOffset(new DateTime(2023,11,20)) },
                    new TicketDb() { Id = new Guid("c1969cee-fa75-48ae-a3f2-d6419a934324"), IssueDate=new DateTimeOffset(new DateTime(2023,11,20)) },
                    // Invalids
                    new TicketDb() { Id = new Guid("cc9f9a2c-1087-495a-baf5-1b22f7f8df39"), IssueDate=new DateTimeOffset(new DateTime(2023,12,20)), 
                        CompostDate = new DateTimeOffset(new DateTime(2023,12,20, 12,09, 43)), 
                        EndOfValidityDate = new DateTimeOffset(new DateTime(2023,12,20, 14,09, 43)),
                        ControlDate = new DateTimeOffset(new DateTime(2023,12,20, 13,09, 43))
                    }
                };

                TicketBookDb[] ticketBooks = {
                     new TicketBookDb() { Id = new Guid("6e142a3c-ba11-4ef8-ad4d-83205d4ca8f7"), IssueDate = new DateTimeOffset(new DateTime(2023, 11, 20)), Tickets = tickets[..10] },
                     new TicketBookDb() { Id = new Guid("1bb56fe2-b150-4823-98cb-f7bbc776a9a5"), IssueDate = new DateTimeOffset(new DateTime(2023, 12, 20)), Tickets = tickets[10..] }
                };

                UserDb[] users = {
                    new UserDb() { Id = new Guid("ff7a8f0f-ed22-49c8-94d9-f2053153d220"), TicketBook = null },
                    new UserDb() { Id = new Guid("1391d19b-3a98-45c1-8b8a-bc3ff1574c14"), TicketBook = ticketBooks.First() },
                    new UserDb() { Id = new Guid("7bf3618c-2a18-4faf-ae16-619355b4445c"), TicketBook = ticketBooks[1] },
                };

                ControllerDb[] controllers = { 
                    new ControllerDb() { Id = new Guid("e148df6b-9186-455a-bfa9-8f76e4b85bf1"), Fraudsters = new List<UserDb> { { users[0] } }, ControlledTickets = new List<TicketDb> {  tickets[^1] } }
                };

                context.Tickets.AddRange(tickets);
                context.TicketBooks.AddRange(ticketBooks);
                context.Users.AddRange(users);
                context.Controllers.AddRange(controllers);

                context.SaveChanges();
            }
        }
    }
}