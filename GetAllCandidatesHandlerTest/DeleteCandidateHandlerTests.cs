using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CandidatesApp.Application.Commands;
using CandidatesApp.Application.Exceptions;
using CandidatesApp.Application.Handlers;
using CandidatesApp.Infrastructure.Data;
using CandidatesApp.Models;
using Microsoft.EntityFrameworkCore;

namespace HandlerTest
{
    [TestClass]
    public class DeleteCandidateHandlerTests
    {
        private MyDbContext _dbContext;
        private DeleteCandidateHandler _handler;

        [TestInitialize]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<MyDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _dbContext = new MyDbContext(options);
            _handler = new DeleteCandidateHandler(_dbContext);
        }

        [TestMethod]
        public async Task Handle_Should_Delete_Candidate_When_Candidate_Exists()
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

            var command = new DeleteCandidateCommand(1);

            var result = await _handler.Handle(command, CancellationToken.None);

            Assert.AreEqual("Candidate with ID 1 has been successfully deleted.", result);

            var deletedCandidate = await _dbContext.Candidates.FindAsync(1);
            Assert.IsNull(deletedCandidate);
        }

        [TestMethod]
        public async Task Handle_Should_Throw_CandidateNotFoundException_When_Candidate_Does_Not_Exist()
        {
            var command = new DeleteCandidateCommand(999);

            await Assert.ThrowsExceptionAsync<CandidateNotFoundException>(
                async () => await _handler.Handle(command, CancellationToken.None));
        }
    }
}
