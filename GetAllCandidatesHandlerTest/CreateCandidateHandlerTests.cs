using AutoMapper;
using CandidatesApp.Application.Commands;
using CandidatesApp.Application.DTOs;
using CandidatesApp.Application.Handlers;
using CandidatesApp.Infrastructure.Data;
using CandidatesApp.Models;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace HandlerTest
{
    [TestClass]
    public class CreateCandidateHandlerTests
    {
        private MyDbContext _dbContext;
        private Mock<IMapper> _mockMapper;
        private CreateCandidateHandler _handler;

        [TestInitialize]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<MyDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) 
                .Options;

            _dbContext = new MyDbContext(options);
            _mockMapper = new Mock<IMapper>();
            _handler = new CreateCandidateHandler(_dbContext, _mockMapper.Object);
        }

        [TestMethod]
        public async Task Handle_Should_Return_CandidateDTO_When_Candidate_Is_Created()
        {
            var command = new CreateCandidateCommand("Lionel", "Messi", new DateTime(1987, 6, 24), "messi@fcb.com");
            var candidateEntity = new Candidate
            {
                Name = "Lionel",
                Surname = "Messi",
                Birthday = new DateTime(1987, 6, 24),
                Email = "messi@fcb.com"
            };

            _mockMapper.Setup(m => m.Map<Candidate>(It.IsAny<CreateCandidateCommand>()))
                       .Returns(candidateEntity);
            _mockMapper.Setup(m => m.Map<CandidateDTO>(It.IsAny<Candidate>()))
                       .Returns(new CandidateDTO
                       {
                           Id = candidateEntity.Id, 
                           Name = "Lionel",
                           Surname = "Messi",
                           Birthdate = new DateTime(1987, 6, 24),
                           Email = "messi@fcb.com"
                       });

            var result = await _handler.Handle(command, CancellationToken.None);

            Assert.IsNotNull(result);
            Assert.AreEqual("Lionel", result.Name);
            Assert.AreEqual("Messi", result.Surname);
            Assert.AreEqual(new DateTime(1987, 6, 24), result.Birthdate);
            Assert.AreEqual("messi@fcb.com", result.Email);
        }

        [TestMethod]
        public async Task Handle_Should_Save_Candidate_To_DbContext()
        {
            var command = new CreateCandidateCommand("Lionel", "Messi", new DateTime(1987, 6, 24), "messi@fcb.com");

            _mockMapper.Setup(m => m.Map<Candidate>(It.IsAny<CreateCandidateCommand>()))
                       .Returns((CreateCandidateCommand cmd) => new Candidate
                       {
                           Name = cmd.Name,
                           Surname = cmd.Surname,
                           Birthday = cmd.Birthday,
                           Email = cmd.Email
                       });

            var result = await _handler.Handle(command, CancellationToken.None);

            var savedCandidate = await _dbContext.Candidates.FirstOrDefaultAsync(c => c.Email == "messi@fcb.com");
            Assert.IsNotNull(savedCandidate);
            Assert.AreEqual("Lionel", savedCandidate.Name);
            Assert.AreEqual("Messi", savedCandidate.Surname);
            Assert.AreEqual(new DateTime(1987, 6, 24), savedCandidate.Birthday);
            Assert.AreEqual("messi@fcb.com", savedCandidate.Email);
        }
    }
}
