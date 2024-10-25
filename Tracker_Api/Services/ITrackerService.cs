using Tracker_Api.Models.Tracker;

namespace Tracker_Api.Services
{
    public interface ITrackerService
    {
        void Update(TrackingData data);
        TrackingData Get(string user, DateOnly date);
        IList<TrackingData> GetRange(string user, DateOnly startDate, DateOnly endDate);
    }
}
