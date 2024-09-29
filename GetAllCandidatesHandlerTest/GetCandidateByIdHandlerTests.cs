using AutoMapper;
using CandidatesApp.Application.DTOs;
using CandidatesApp.Application.Exceptions;
using CandidatesApp.Application.Handlers;
using CandidatesApp.Application.Queries;
using CandidatesApp.Infrastructure.Data;
using CandidatesApp.Models;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace HandlerTest
{
    [TestClass]
    public class GetCandidateByIdHandlerTests
    {
        private MyDbContext _dbContext;
        private Mock<IMapper> _mockMapper;
        private GetCandidateByIdHandler _handler;

        [TestInitialize]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<MyDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _dbContext = new MyDbContext(options);
            _mockMapper = new Mock<IMapper>();
            _handler = new GetCandidateByIdHandler(_dbContext, _mockMapper.Object);
        }

        [TestMethod]
        public async Task Handle_Should_Return_CandidateDTO_When_Candidate_Exists()
        {
            var candidate = new Candidate
            {
                Id = 1,
                Name = "Lionel",
                Surname = "Messi",
                Birthday = new DateTime(1987, 6, 24),
                Email = "messi@fcb.com",
                InsertDate = new DateTime(2023, 9, 1),
                ModifyDate = null
            };

            _dbContext.Candidates.Add(candidate);
            await _dbContext.SaveChangesAsync();

            var candidateDTO = new CandidateDTO
            {
                Id = 1,
                Name = "Lionel",
                Surname = "Messi",
                Birthdate = new DateTime(1987, 6, 24),
                Email = "messi@fcb.com",
                InsertDate = new DateTime(2023, 9, 1),
                ModifyDate = null
            };

            _mockMapper.Setup(m => m.Map<CandidateDTO>(It.Is<Candidate>(c => c.Id == candidate.Id)))
                       .Returns(candidateDTO);

            var query = new GetCandidateByIdQuery(1);

            var result = await _handler.Handle(query, CancellationToken.None);

            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Id);
            Assert.AreEqual("Lionel", result.Name);
            Assert.AreEqual("Messi", result.Surname);
            Assert.AreEqual(new DateTime(1987, 6, 24), result.Birthdate);
            Assert.AreEqual("messi@fcb.com", result.Email);
            Assert.AreEqual(new DateTime(2023, 9, 1), result.InsertDate);
            Assert.IsNull(result.ModifyDate);
        }

        [TestMethod]
        public async Task Handle_Should_Throw_CandidateNotFoundException_When_Candidate_Does_Not_Exist()
        {
            var query = new GetCandidateByIdQuery(999);

            await Assert.ThrowsExceptionAsync<CandidateNotFoundException>(
                async () => await _handler.Handle(query, CancellationToken.None));
        }
    }
}