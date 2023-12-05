using Entities;

namespace Services
{
    public interface IRatingService
    {
        Task createRatingAsync(Rating rating);
    }
}