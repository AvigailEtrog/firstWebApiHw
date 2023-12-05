using Entities;

namespace Repositories
{
    public interface IRatingRepository
    {
        Task createRatingAsync(Rating rating);
    }
}