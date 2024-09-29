using AutoMapper;
using CandidatesApp.Application.Commands;
using CandidatesApp.Application.DTOs;
using CandidatesApp.Application.Exceptions;
using CandidatesApp.Application.Handlers;
using CandidatesApp.Infrastructure.Data;
using CandidatesApp.Models;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace HandlerTest
{
    [TestClass]
    public class UpdateCandidateHandlerTests
    {
        private MyDbContext _dbContext;
        private Mock<IMapper> _mockMapper;
        private UpdateCandidateHandler _handler;

        [TestInitialize]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<MyDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _dbContext = new MyDbContext(options);
            _mockMapper = new Mock<IMapper>();
            _handler = new UpdateCandidateHandler(_dbContext, _mockMapper.Object);
        }

        [TestMethod]
        public async Task Handle_Should_Update_Candidate_When_Exists()
        {
            var candidateId = 1;
            var initialCandidate = new Candidate
            {
                Id = candidateId,
                Name = "John",
                Surname = "Doe",
                Birthday = DateTime.UtcNow.AddYears(-30),
                Email = "john.doe@example.com",
                InsertDate = DateTime.UtcNow
            };

            _dbContext.Candidates.Add(initialCandidate);
            await _dbContext.SaveChangesAsync();

            var updateCommand = new UpdateCandidateCommand(
                Id: candidateId,
                Name: "John Updated",
                Surname: "Doe Updated",
                Birthdate: DateTime.UtcNow.AddYears(-35),
                Email: "john.updated@example.com"
            );

            var updatedCandidateDto = new CandidateDTO
            {
                Id = candidateId,
                Name = "John Updated",
                Surname = "Doe Updated",
                Birthdate = DateTime.UtcNow.AddYears(-35),
                Email = "john.updated@example.com"
            };

            _mockMapper.Setup(m => m.Map(It.IsAny<UpdateCandidateCommand>(), It.IsAny<Candidate>()))
                       .Callback<UpdateCandidateCommand, Candidate>((cmd, cand) =>
                       {
                           cand.Name = cmd.Name;
                           cand.Surname = cmd.Surname;
                           cand.Birthday = cmd.Birthdate;
                           cand.Email = cmd.Email;
                           cand.ModifyDate = DateTime.UtcNow;
                       });

            _mockMapper.Setup(m => m.Map<CandidateDTO>(It.IsAny<Candidate>()))
                       .Returns(updatedCandidateDto);

            var result = await _handler.Handle(updateCommand, CancellationToken.None);

            Assert.IsNotNull(result);
            Assert.AreEqual(updatedCandidateDto.Name, result.Name);
            Assert.AreEqual(updatedCandidateDto.Surname, result.Surname);
            Assert.AreEqual(updatedCandidateDto.Email, result.Email);
        }

        [TestMethod]
        public async Task Handle_Should_Throw_CandidateNotFoundException_When_Candidate_Does_Not_Exist()
        {
            var candidateId = 1;

            var updateCommand = new UpdateCandidateCommand(
                Id: candidateId,
                Name: "Non-existent",
                Surname: "Candidate",
                Birthdate: DateTime.UtcNow.AddYears(-35),
                Email: "non.existent@example.com"
            );

            await Assert.ThrowsExceptionAsync<CandidateNotFoundException>(() => _handler.Handle(updateCommand, CancellationToken.None));
        }
    }
}