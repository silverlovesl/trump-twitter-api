using System.Collections.Generic;

namespace TrumpTwitter.Models
{

  public class MonthlySQLData
  {
    public int Year { get; set; }
    public int Month { get; set; }
    public int Count { get; set; }
  }
  public class MonthlyStatistic
  {
    public int Year { get; set; }
    public List<MonthlyDataStatistic> Data { get; set; }
  }

  public class MonthlyDataStatistic
  {
    public int Month { get; set; }
    public int Count { get; set; }
  }
}