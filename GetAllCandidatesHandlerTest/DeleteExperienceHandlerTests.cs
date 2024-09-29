using AutoMapper;
using CandidatesApp.Application.Commands;
using CandidatesApp.Application.Exceptions;
using CandidatesApp.Application.Handlers;
using CandidatesApp.Infrastructure.Data;
using CandidatesApp.Models;
using Microsoft.EntityFrameworkCore;


namespace HandlerTest
{
    [TestClass]
    public class DeleteExperienceHandlerTests
    {
        private MyDbContext _dbContext;
        private IMapper _mapper;
        private DeleteExperienceHandler _handler;

        [TestInitialize]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<MyDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _dbContext = new MyDbContext(options);
            _mapper = new Mapper(new MapperConfiguration(cfg => { }));
            _handler = new DeleteExperienceHandler(_dbContext, _mapper);
        }

        [TestMethod]
        public async Task Handle_Should_Delete_Experience_When_Experience_Exists()
        {
            var candidate = new Candidate
            {
                Id = 1,
                Name = "Lionel",
                Surname = "Messi",
                Birthday = new DateTime(1987, 6, 24),
                Email = "messi@fcb.com",
                Experiences = new List<Experience>
                {
                    new Experience { Id = 1, Company = "FC Barcelona", Job = "Forward", Description = "Top scorer", Salary = 1000000, BeginDate = new DateTime(2004, 8, 1) }
                }
            };

            _dbContext.Candidates.Add(candidate);
            await _dbContext.SaveChangesAsync();

            var command = new DeleteExperienceCommand(1, 1);

            var result = await _handler.Handle(command, CancellationToken.None);

            Assert.AreEqual("Experience with ID 1 has been successfully deleted.", result);

            var deletedExperience = await _dbContext.Experience.FindAsync(1);
            Assert.IsNull(deletedExperience);
        }

        [TestMethod]
        public async Task Handle_Should_Throw_CandidateNotFoundException_When_Candidate_Does_Not_Exist()
        {
            var command = new DeleteExperienceCommand(1, 999);

            await Assert.ThrowsExceptionAsync<CandidateNotFoundException>(
                async () => await _handler.Handle(command, CancellationToken.None));
        }

        [TestMethod]
        public async Task Handle_Should_Return_NotFoundMessage_When_Experience_Does_Not_Exist()
        {
            var candidate = new Candidate
            {
                Id = 1,
                Name = "Lionel",
                Surname = "Messi",
                Birthday = new DateTime(1987, 6, 24),
                Email = "messi@fcb.com"
            };

            _dbContext.Candidates.Add(candidate);
            await _dbContext.SaveChangesAsync();

            var command = new DeleteExperienceCommand(1, 999);

            var result = await _handler.Handle(command, CancellationToken.None);

            Assert.AreEqual("Experience with ID 999 not found for candidate with ID 1.", result);
        }
    }

}

