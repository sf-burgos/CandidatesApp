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
    public class UpdateExperienceHandlerTests
    {
        private MyDbContext _dbContext;
        private Mock<IMapper> _mockMapper;
        private UpdateExperienceHandler _handler;

        [TestInitialize]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<MyDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _dbContext = new MyDbContext(options);
            _mockMapper = new Mock<IMapper>();
            _handler = new UpdateExperienceHandler(_dbContext, _mockMapper.Object);
        }

        [TestMethod]
        public async Task Handle_Should_Update_Experience_When_Exists()
        {
            var candidateId = 1;
            var experienceId = 1;
            var initialExperience = new Experience
            {
                Id = experienceId,
                CandidateId = candidateId,
                Company = "Company A",
                Job = "Developer",
                Description = "Developing applications",
                Salary = 50000,
                BeginDate = DateTime.Now.AddYears(-2),
                InsertDate = DateTime.Now
            };

            _dbContext.Experience.Add(initialExperience);
            await _dbContext.SaveChangesAsync();

            var updateCommand = new UpdateExperienceCommand(
                CandidateId: candidateId,
                ExperienceId: experienceId,
                Company: "Company A Updated",
                Job: "Developer Updated",
                Description: "Updated description",
                Salary: 60000,
                BeginDate: DateTime.Now.AddYears(-1),
                EndDate: null
            );

            var updatedExperienceDto = new ExperienceDto
            {
                Id = experienceId,
                CandidateId = candidateId,
                Company = "Company A Updated",
                Job = "Developer Updated",
                Description = "Updated description",
                Salary = 60000,
                BeginDate = DateTime.Now.AddYears(-1),
                ModifyDate = DateTime.Now
            };

            _mockMapper.Setup(m => m.Map(It.IsAny<UpdateExperienceCommand>(), It.IsAny<Experience>()))
                       .Callback<UpdateExperienceCommand, Experience>((cmd, exp) =>
                       {
                           exp.Company = cmd.Company;
                           exp.Job = cmd.Job;
                           exp.Description = cmd.Description;
                           exp.Salary = cmd.Salary;
                           exp.BeginDate = cmd.BeginDate;
                           exp.EndDate = cmd.EndDate;
                           exp.ModifyDate = DateTime.Now;
                       });

            _mockMapper.Setup(m => m.Map<ExperienceDto>(It.IsAny<Experience>()))
                       .Returns(updatedExperienceDto);

            var result = await _handler.Handle(updateCommand, CancellationToken.None);

            Assert.IsNotNull(result);
            Assert.AreEqual(updatedExperienceDto.Company, result.Company);
            Assert.AreEqual(updatedExperienceDto.Job, result.Job);
            Assert.AreEqual(updatedExperienceDto.Description, result.Description);
            Assert.AreEqual(updatedExperienceDto.Salary, result.Salary);
        }

        [TestMethod]
        public async Task Handle_Should_Throw_ExperienceNotFoundException_When_Experience_Does_Not_Exist()
        {
            var candidateId = 1;
            var experienceId = 1;

            var updateCommand = new UpdateExperienceCommand(
                CandidateId: candidateId,
                ExperienceId: experienceId,
                Company: "Company A",
                Job: "Developer",
                Description: "Developing applications",
                Salary: 50000,
                BeginDate: DateTime.Now.AddYears(-2),
                EndDate: null
            );

            await Assert.ThrowsExceptionAsync<ExperienceNotFoundException>(() => _handler.Handle(updateCommand, CancellationToken.None));
        }
    }
}