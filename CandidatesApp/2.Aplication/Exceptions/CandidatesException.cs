using System;

namespace CandidatesApp._2.Application.Exceptions
{
    public class CandidateNotFoundException : Exception
    {
        public CandidateNotFoundException(int candidateId)
            : base($"Candidate with ID {candidateId} does not exist. Please verify the candidate ID.")
        {
        }
    }
}
