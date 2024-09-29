using AutoMapper;
using CandidatesApp.Application.DTOs;
using CandidatesApp.Application.Handlers;
using CandidatesApp.Application.Queries;
using CandidatesApp.Infrastructure.Data;
using CandidatesApp.Models;
using Microsoft.EntityFrameworkCore;
using Moq;

public class GetAllCandidatesQueryHandlerTests
{
    private readonly Mock<DbSet<Candidate>> _mockSet;
    private readonly Mock<IMapper> _mockMapper;
    private readonly Mock<MyDbContext> _mockContext;
    private readonly GetAllCandidatesQueryHandler _handler;

    public GetAllCandidatesQueryHandlerTests()
    {
        _mockSet = new Mock<DbSet<Candidate>>();
        _mockMapper = new Mock<IMapper>();
        _mockContext = new Mock<MyDbContext>();

        _handler = new GetAllCandidatesQueryHandler(_mockContext.Object, _mockMapper.Object);
    }

    [Fact]
    public async Task Handle_Should_Return_List_Of_CandidateDTOs_With_All_Attributes()
    {
        var candidates = new List<Candidate>
    {
        new Candidate { Id = 1, Name = "Lionel", Surname = "Messi", Birthdate = new DateTime(1987, 6, 24), Email = "messi@fcb.com" },
        new Candidate { Id = 2, Name = "Xavi", Surname = "Hernandez", Birthdate = new DateTime(1980, 1, 25), Email = "xavi@fcb.com" }
    };

        _mockSet.As<IQueryable<Candidate>>().Setup(m => m.Provider).Returns(candidates.AsQueryable().Provider);
        _mockSet.As<IQueryable<Candidate>>().Setup(m => m.Expression).Returns(candidates.AsQueryable().Expression);
        _mockSet.As<IQueryable<Candidate>>().Setup(m => m.ElementType).Returns(candidates.AsQueryable().ElementType);
        _mockSet.As<IQueryable<Candidate>>().Setup(m => m.GetEnumerator()).Returns(candidates.GetEnumerator());

        _mockContext.Setup(c => c.Candidates).Returns(_mockSet.Object);

        var candidateDTOs = candidates.Select(c => new CandidateDTO
        {
            Id = c.Id,
            Name = c.Name,
            Surname = c.Surname,
            Birthdate = c.Birthdate,
            Email = c.Email
        }).ToList();

        _mockMapper.Setup(m => m.Map<IEnumerable<CandidateDTO>>(It.IsAny<IEnumerable<Candidate>>()))
                   .Returns(candidateDTOs);

        var query = new GetAllCandidatesQuery();
        var result = await _handler.Handle(query, CancellationToken.None);

        Assert.NotNull(result);
        Assert.Equal(2, result.Count());
        Assert.Equal("Lionel", result.First().Name);
        Assert.Equal("Messi", result.First().Surname);
        Assert.Equal("messi@fcb.com", result.First().Email);
        Assert.Equal(new DateTime(1987, 6, 24), result.First().Birthdate);
    }

    [Fact]
    public async Task Handle_Should_Return_Empty_List_When_No_Candidates_Found()
    {
        var players = new List<Candidate>();

        _mockSet.As<IQueryable<Candidate>>().Setup(m => m.Provider).Returns(players.AsQueryable().Provider);
        _mockSet.As<IQueryable<Candidate>>().Setup(m => m.Expression).Returns(players.AsQueryable().Expression);
        _mockSet.As<IQueryable<Candidate>>().Setup(m => m.ElementType).Returns(players.AsQueryable().ElementType);
        _mockSet.As<IQueryable<Candidate>>().Setup(m => m.GetEnumerator()).Returns(players.GetEnumerator());

        _mockContext.Setup(c => c.Candidates).Returns(_mockSet.Object);

        _mockMapper.Setup(m => m.Map<IEnumerable<CandidateDTO>>(It.IsAny<IEnumerable<Candidate>>()))
                   .Returns(new List<CandidateDTO>());

        var query = new GetAllCandidatesQuery();
        var result = await _handler.Handle(query, CancellationToken.None);

        Assert.NotNull(result);
        Assert.Empty(result);

        _mockContext.Verify(c => c.Candidates, Times.Once);
    }
}
