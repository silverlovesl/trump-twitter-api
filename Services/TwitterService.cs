using System.Linq;
using MySql.Data.MySqlClient;
using TrumpTwitter.Entities;
using System.Collections.Generic;
using Dapper;
using Microsoft.Extensions.Configuration;
using TrumpTwitter.Models;

namespace TrumpTwitter.Services
{
  public interface ITwitterService
  {
    List<AnnualStatistic> GetAnnualStatistic();
    List<MonthlyStatistic> GetMonthlyStatistic();
    List<HourlyStatistic> GetHourlyStatistic();
    List<WordCloud> GetWordCloud(string category);
  }
  public class TwitterService : BaseService<Twitter>, ITwitterService
  {
    public TwitterService(IConfiguration config) : base(config) { }
    public TwitterService(MySqlConnection connection) : base(connection) { }
    public TwitterService(MySqlConnection connection, MySqlTransaction transaction) : base(connection, transaction) { }
    public List<AnnualStatistic> GetAnnualStatistic()
    {
      const string SQL = @"
        select 
            YEAR(created_at) as year
          ,count(id_str)    as count
        from twitter 
        where 1=1 and source = 'Twitter for iPhone'
        group by YEAR(created_at);
      ";
      using (var conn = Connection)
      {
        var result = conn.Query<AnnualStatistic>(SQL);
        return result.AsList();
      }
    }

    public List<MonthlyStatistic> GetMonthlyStatistic()
    {
      const string SQL = @"
        select 
             YEAR(created_at) as year
            ,MONTH(created_at) as month
            ,count(id_str)    as count
        from twitter 
        where 1=1 and source = 'Twitter for iPhone'
        group by YEAR(created_at),MONTH(created_at)
        order by year,month
      ";
      using (var conn = Connection)
      {
        var sqlResult = conn.Query<MonthlySQLData>(SQL).AsList();
        var query = sqlResult.GroupBy(v => v.Year);
        var result = new List<MonthlyStatistic>();
        foreach (var group in query)
        {
          var item = new MonthlyStatistic();
          item.Year = group.Key;
          item.Data = new List<MonthlyDataStatistic>();
          foreach (var d in group)
          {
            item.Data.Add(new MonthlyDataStatistic() { Month = d.Month, Count = d.Count });
          }
          result.Add(item);
        }
        return result;
      }
    }

    public List<HourlyStatistic> GetHourlyStatistic()
    {
      const string SQL = @"
        select
           YEAR(created_at) as year
          ,HOUR(created_at) as hour
          ,count(id_str)    as count
        from twitter 
        where 1=1 and source = 'Twitter for iPhone'
        group by
          YEAR(created_at)
          ,HOUR(created_at)
        order by
          year,hour
      ";
      using (var conn = Connection)
      {
        var sqlResult = conn.Query<HourlySQLData>(SQL).AsList();
        var query = sqlResult.GroupBy(v => v.Year);
        var result = new List<HourlyStatistic>();
        foreach (var group in query)
        {
          var item = new HourlyStatistic();
          item.Year = group.Key;
          item.Data = new List<HourlyDataStatistic>();
          foreach (var d in group)
          {
            item.Data.Add(new HourlyDataStatistic() { Hour = d.Hour, Count = d.Count });
          }
          result.Add(item);
        }
        return result;
      }
    }

    public List<WordCloud> GetWordCloud(string category)
    {
      string SQL = @"
        select
          word  as word,
          count as value
        from
          word_frequence
        where
          1 = 1
      ";
      using (var conn = Connection)
      {
        bool isNumber = int.TryParse(category, out int _c);
        if (isNumber) SQL += $" and category={_c}";
        // SQL += " limit 200";
        var result = conn.Query<WordCloud>(SQL).AsList();
        return result;
      }
    }
  }
}