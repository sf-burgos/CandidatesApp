namespace CandidatesApp.Application.Exceptions
{
    public class ExperienceNotFoundException : Exception
    {
        public ExperienceNotFoundException(int candidateId, int experienceId)
            : base($"Experience with ID {experienceId} for candidate with ID {candidateId} does not exist. Please verify the experience ID.")
        {
        }
    }
}
