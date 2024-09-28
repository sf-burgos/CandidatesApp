﻿using AutoMapper;
using CandidatesApp._2.Application.Exceptions;
using CandidatesApp._3.Infrastructure.Commands;
using CandidatesApp.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CandidatesApp._2.Aplication.Handlers
{
    public class DeleteExperienceHandler : IRequestHandler<DeleteExperienceCommand, string>
    {
        private readonly MyDbContext _context;
        private readonly IMapper _mapper;

        public DeleteExperienceHandler(MyDbContext dbContext, IMapper mapper)
        {
            _context = dbContext;
            _mapper = mapper;
        }

        public async Task<string> Handle(DeleteExperienceCommand request, CancellationToken cancellationToken)
        {
            var candidate = await _context.Candidates.FindAsync(request.CandidateId);

            if (candidate == null)
            {
                throw new CandidateNotFoundException(request.CandidateId);
            }

            var experience = await _context.Experience
                .FirstOrDefaultAsync(e => e.Id == request.ExperienceId && e.CandidateId == request.CandidateId, cancellationToken);

            if (experience == null)
            {
                return $"Experience with ID {request.ExperienceId} not found for candidate with ID {request.CandidateId}.";
            }

            _context.Experience.Remove(experience);
            await _context.SaveChangesAsync(cancellationToken);

            return $"Experience with ID {request.ExperienceId} has been successfully deleted.";
        }
    }

}
