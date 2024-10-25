using Tracker_Api.Models;
using Tracker_Api.Models.Tracker;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Tracker_Api.Services
{
    public class TrackerService:ITrackerService
    {
        private readonly TrackerContext _context;
        public TrackerService(TrackerContext context)
        {
            _context = context;
        }
        public void Update(TrackingData data)
        {
            var foundData = _context.TrackingDatas.FirstOrDefault(x=>x.Date == data.Date&&x.User==data.User);
            if (foundData != null)
            {
                foundData.StressLevel = data.StressLevel;
                foundData.HapinessLevel = data.HapinessLevel;
                foundData.AnxietyLevel = data.AnxietyLevel;
                foundData.ExerciseTime = data.ExerciseTime;
                foundData.SleepTime = data.SleepTime;
                foundData.HappySentence = data.HappySentence;
                foundData.MeditationTime = data.MeditationTime;
                foundData.SleepQuality = data.SleepQuality;
                _context.TrackingDatas.Update(foundData);
            }
                
            else _context.TrackingDatas.Add(data);
            _context.SaveChanges();
        }
        public TrackingData Get(string user,DateOnly date)
        {
            var foundData = _context.TrackingDatas.FirstOrDefault(x => x.Date == date && x.User == user);
            return foundData ?? new TrackingData() { User=user,Date=date,SleepTime=0,SleepQuality=0,StressLevel=0,HappySentence="",AnxietyLevel=0,ExerciseTime=0,HapinessLevel=0,MeditationTime=0};
        }
        public IList<TrackingData> GetRange(string user, DateOnly startDate, DateOnly endDate)
        {
            IList<TrackingData> foundData = _context.TrackingDatas.Where(x => x.Date >= startDate && x.Date<endDate && x.User == user).ToList();
            return foundData;
        }
    }
}
