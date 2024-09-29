using AutoMapper;
using CandidatesApp.Application.DTOs;
using CandidatesApp.Application.Handlers;
using CandidatesApp.Application.Queries;
using CandidatesApp.Infrastructure.Data;
using CandidatesApp.Models;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace GetAllCandidatesHandlerTest
{
    [TestClass]
    public class GetAllCandidatesQueryHandlerTests
    {
        private MyDbContext _dbContext;
        private Mock<IMapper> _mockMapper;
        private GetAllCandidatesQueryHandler _handler;

        [TestInitialize]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<MyDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _dbContext = new MyDbContext(options);
            _mockMapper = new Mock<IMapper>();
            _handler = new GetAllCandidatesQueryHandler(_dbContext, _mockMapper.Object);
        }

        [TestMethod]
        public async Task Handle_Should_Return_List_Of_CandidateDTOs_Without_Experiences()
        {
            var candidates = new List<Candidate>
            {
                new Candidate {
                    Id = 1,
                    Name = "Lionel",
                    Surname = "Messi",
                    Birthday = new DateTime(1987, 6, 24),
                    Email = "messi@fcb.com",
                    InsertDate = new DateTime(2023, 9, 1),
                    ModifyDate = new DateTime(2024, 1, 1)
                },
                new Candidate {
                    Id = 2,
                    Name = "Xavi",
                    Surname = "Hernandez",
                    Birthday = new DateTime(1980, 1, 25),
                    Email = "xavi@fcb.com",
                    InsertDate = new DateTime(2023, 9, 1)
                }
            };

            _dbContext.Candidates.AddRange(candidates);
            await _dbContext.SaveChangesAsync();

            var candidateDTOs = new List<CandidateDTO>
            {
                new CandidateDTO
                {
                    Id = 1,
                    Name = "Lionel",
                    Surname = "Messi",
                    Birthdate = new DateTime(1987, 6, 24),
                    Email = "messi@fcb.com",
                    InsertDate = new DateTime(2023, 9, 1),
                    ModifyDate = new DateTime(2024, 1, 1),
                    Experiences = new List<ExperienceDto>()
                },
                new CandidateDTO
                {
                    Id = 2,
                    Name = "Xavi",
                    Surname = "Hernandez",
                    Birthdate = new DateTime(1980, 1, 25),
                    Email = "xavi@fcb.com",
                    InsertDate = new DateTime(2023, 9, 1),
                    Experiences = new List<ExperienceDto>()
                }
            };

            _mockMapper.Setup(m => m.Map<IEnumerable<CandidateDTO>>(It.IsAny<IEnumerable<Candidate>>()))
                       .Returns(candidateDTOs);

            var query = new GetAllCandidatesQuery();

            var result = await _handler.Handle(query, CancellationToken.None);

            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count());

            var firstCandidate = result.First();
            Assert.AreEqual("Lionel", firstCandidate.Name);
            Assert.AreEqual("Messi", firstCandidate.Surname);
            Assert.AreEqual("messi@fcb.com", firstCandidate.Email);
            Assert.AreEqual(new DateTime(1987, 6, 24), firstCandidate.Birthdate);
            Assert.AreEqual(new DateTime(2023, 9, 1), firstCandidate.InsertDate);
            Assert.AreEqual(new DateTime(2024, 1, 1), firstCandidate.ModifyDate);
            Assert.IsNotNull(firstCandidate.Experiences);
            Assert.AreEqual(0, firstCandidate.Experiences.Count());

            var secondCandidate = result.Last();
            Assert.AreEqual("Xavi", secondCandidate.Name);
            Assert.AreEqual("Hernandez", secondCandidate.Surname);
            Assert.AreEqual("xavi@fcb.com", secondCandidate.Email);
            Assert.AreEqual(new DateTime(1980, 1, 25), secondCandidate.Birthdate);
            Assert.AreEqual(new DateTime(2023, 9, 1), secondCandidate.InsertDate);
            Assert.IsNull(secondCandidate.ModifyDate);
            Assert.IsNotNull(secondCandidate.Experiences);
            Assert.AreEqual(0, secondCandidate.Experiences.Count());
        }
    }
}
