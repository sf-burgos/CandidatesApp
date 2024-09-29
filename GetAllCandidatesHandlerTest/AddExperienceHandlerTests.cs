using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    public class AddExperienceHandlerTests
    {
        private MyDbContext _dbContext;
        private Mock<IMapper> _mockMapper;
        private AddExperienceHandler _handler;

        [TestInitialize]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<MyDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _dbContext = new MyDbContext(options);
            _mockMapper = new Mock<IMapper>();
            _handler = new AddExperienceHandler(_dbContext, _mockMapper.Object);
        }

        [TestMethod]
        public async Task Handle_Should_Add_Experience_When_Candidate_Exists()
        {
            var candidateId = 1;
            var candidate = new Candidate
            {
                Id = candidateId,
                Name = "John",
                Surname = "Doe",
                Email = "john.doe@example.com",
                Birthday = DateTime.UtcNow.AddYears(-30)
            };

            _dbContext.Candidates.Add(candidate);
            await _dbContext.SaveChangesAsync();

            var experienceDto = new ExperienceDto
            {
                Company = "Company",
                Job = "Software Engineer",
                Description = "Developed software solutions",
                Salary = 60000,
                BeginDate = DateTime.UtcNow.AddYears(-2),
                EndDate = DateTime.UtcNow
            };

            var addExperienceCommand = new AddExperienceCommand(candidateId, experienceDto);

            var experience = new Experience
            {
                Company = experienceDto.Company,
                Job = experienceDto.Job,
                Description = experienceDto.Description,
                Salary = experienceDto.Salary,
                BeginDate = experienceDto.BeginDate,
                EndDate = experienceDto.EndDate,
                InsertDate = DateTime.UtcNow
            };

            _mockMapper.Setup(m => m.Map<Experience>(It.IsAny<ExperienceDto>()))
                       .Returns(experience);

            _mockMapper.Setup(m => m.Map<ExperienceDto>(It.IsAny<Experience>()))
                       .Returns(experienceDto);

            var result = await _handler.Handle(addExperienceCommand, CancellationToken.None);

            Assert.IsNotNull(result);
            Assert.AreEqual(experienceDto.Company, result.Company);
            Assert.AreEqual(experienceDto.Job, result.Job);
        }

        [TestMethod]
        public async Task Handle_Should_Return_Null_When_Candidate_Does_Not_Exist()
        {
            var experienceDto = new ExperienceDto
            {
                Company = "Company",
                Job = "Software Engineer",
                Description = "Developed software solutions",
                Salary = 60000,
                BeginDate = DateTime.UtcNow.AddYears(-2),
                EndDate = DateTime.UtcNow
            };

            var addExperienceCommand = new AddExperienceCommand(999, experienceDto);

            var result = await _handler.Handle(addExperienceCommand, CancellationToken.None);

            Assert.IsNull(result);
        }
    }
}