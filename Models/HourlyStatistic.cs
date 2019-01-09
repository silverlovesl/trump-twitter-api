using System.Collections.Generic;

namespace TrumpTwitter.Models
{

  public class HourlySQLData
  {
    public int Year { get; set; }
    public int Hour { get; set; }
    public int Count { get; set; }
  }
  public class HourlyStatistic
  {
    public int Year { get; set; }
    public List<HourlyDataStatistic> Data { get; set; }
  }

  public class HourlyDataStatistic
  {
    public int Hour { get; set; }
    public int Count { get; set; }
  }
}