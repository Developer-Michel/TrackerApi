using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tracker_Api.Helper;
using Tracker_Api.Models.Tracker;
using Tracker_Api.Services;

namespace Tracker_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrackerController : ControllerBase
    {
        private readonly ITrackerService _trackerService;
        public TrackerController(ITrackerService trackerService)
        {
            _trackerService= trackerService;
        }
        [HttpGet]
        public IActionResult Get(string user,DateOnly date)
        {
            try
            {
                return HttpWebResponseHelper.CreateGoodHttpResponse(_trackerService.Get(user,date));
            }
            catch (Exception ex)
            {
                return HttpWebResponseHelper.CreateBadHttpResponse(ex);
            }
        }
        [HttpGet]
        [Route("GetDatas")]
        public IActionResult GetDatas(string user, DateOnly startDate, DateOnly endDate)
        {
            try
            {
                return HttpWebResponseHelper.CreateGoodHttpResponse(_trackerService.GetRange(user, startDate,endDate));
            }
            catch (Exception ex)
            {
                return HttpWebResponseHelper.CreateBadHttpResponse(ex);
            }
        }
        [HttpPut]
        public IActionResult Update([FromBody] TrackingData data)
        {
            try
            {
                _trackerService.Update(data);
                return HttpWebResponseHelper.CreateGoodHttpResponse();
            }
            catch (Exception ex)
            {
                return HttpWebResponseHelper.CreateBadHttpResponse(ex);
            }
        }
    }
}
