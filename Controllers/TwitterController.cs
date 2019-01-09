using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TrumpTwitter.Entities;
using TrumpTwitter.Models;
using TrumpTwitter.Services;

namespace TrumpTwitter.Controllers
{
  [Route("api/v1/twitter")]
  [ApiController]
  public class TwitterController : ControllerBase
  {
    public ITwitterService _tservice { get; set; }

    public TwitterController(ITwitterService tservice)
    {
      _tservice = tservice;
    }

    [HttpGet]
    public ActionResult<IEnumerable<string>> Get()
    {
      return new string[] { "value1", "value2" };
    }

    [HttpGet("{id}")]
    public ActionResult<string> Get(int id)
    {
      return "value";
    }

    [HttpPost]
    public void Post([FromBody] string value)
    {
    }

    // PUT api/values/5
    [HttpPut("{id}")]
    public void Put(int id, [FromBody] string value)
    {
    }

    // DELETE api/values/5
    [HttpDelete("{id}")]
    public void Delete(int id)
    {
    }


    [HttpGet]
    [Route("annual-statistic")]
    public ActionResult<ListWrap<AnnualStatistic>> GetAnnualStatistic()
    {
      var result = new ListWrap<AnnualStatistic>();
      result.Data = _tservice.GetAnnualStatistic();
      return result;
    }

    [HttpGet]
    [Route("monthly-statistic")]
    public ActionResult<ListWrap<MonthlyStatistic>> GetMonthlyStatistic()
    {
      var result = new ListWrap<MonthlyStatistic>();
      result.Data = _tservice.GetMonthlyStatistic();
      return result;
    }

    [HttpGet]
    [Route("hourly-statistic")]
    public ActionResult<ListWrap<HourlyStatistic>> GetHourlyStatistic()
    {
      var result = new ListWrap<HourlyStatistic>();
      result.Data = _tservice.GetHourlyStatistic();
      return result;
    }

    [HttpGet]
    [Route("word-cloud")]
    public ActionResult<ListWrap<WordCloud>> GetWordCloud([FromQuery] string category)
    {
      var result = new ListWrap<WordCloud>();
      result.Data = _tservice.GetWordCloud(category);
      return result;
    }
  }
}
