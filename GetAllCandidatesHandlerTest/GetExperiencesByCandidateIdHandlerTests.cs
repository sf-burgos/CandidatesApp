
using AutoMapper;
using CandidatesApp.Application.DTOs;
using CandidatesApp.Application.Handlers;
using CandidatesApp.Application.Queries;
using CandidatesApp.Infrastructure.Data;
using CandidatesApp.Models;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace HandlerTest
{
    [TestClass]
    public class GetExperiencesByCandidateIdHandlerTests
    {
        private MyDbContext _dbContext;
        private Mock<IMapper> _mockMapper;
        private GetExperiencesByCandidateIdHandler _handler;

        [TestInitialize]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<MyDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _dbContext = new MyDbContext(options);
            _mockMapper = new Mock<IMapper>();
            _handler = new GetExperiencesByCandidateIdHandler(_dbContext, _mockMapper.Object);
        }

        [TestMethod]
        public async Task Handle_Should_Return_ExperienceDtos_When_Experiences_Exist()
        {
            var candidateId = 1;
            var experiences = new List<Experience>
            {
                new Experience { Id = 1, CandidateId = candidateId, Company = "Company A", Job = "Developer", Description = "Developing applications", Salary = 50000, BeginDate = DateTime.Now.AddYears(-2) },
                new Experience { Id = 2, CandidateId = candidateId, Company = "Company B", Job = "Senior Developer", Description = "Leading projects", Salary = 70000, BeginDate = DateTime.Now.AddYears(-1) }
            };

            _dbContext.Experience.AddRange(experiences);
            await _dbContext.SaveChangesAsync();

            var experienceDtos = new List<ExperienceDto>
            {
                new ExperienceDto { Id = 1, CandidateId = candidateId, Company = "Company A", Job = "Developer", Description = "Developing applications", Salary = 50000, BeginDate = DateTime.Now.AddYears(-2) },
                new ExperienceDto { Id = 2, CandidateId = candidateId, Company = "Company B", Job = "Senior Developer", Description = "Leading projects", Salary = 70000, BeginDate = DateTime.Now.AddYears(-1) }
            };

            _mockMapper.Setup(m => m.Map<List<ExperienceDto>>(It.IsAny<List<Experience>>()))
                       .Returns(experienceDtos);

            var query = new GetExperiencesByCandidateIdQuery(candidateId);
            var result = await _handler.Handle(query, CancellationToken.None);

            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count);
            Assert.AreEqual("Company A", result[0].Company);
            Assert.AreEqual("Company B", result[1].Company);
        }
    }
}
