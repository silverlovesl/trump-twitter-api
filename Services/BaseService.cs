using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System.Linq;
using System;
using Dapper.FastCrud;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace TrumpTwitter.Services
{
  public class BaseService<T> where T : new()
  {
    public IConfiguration Configuration { get; }

    public bool AutoCommit { get; }
    private MySqlConnection _connection;
    private MySqlTransaction _transaction;
    public MySqlTransaction Transaction { get; set; }
    public MySqlConnection Connection => _connection ?? (_connection = new MySqlConnection(Configuration.GetConnectionString("TrumpTwitter").ToString()));

    protected BaseService(IConfiguration config)
    {
      AutoCommit = true;
      Configuration = config;
    }

    protected BaseService(MySqlConnection connection)
    {
      AutoCommit = false;
      _connection = connection;
    }

    protected BaseService(MySqlConnection connection, MySqlTransaction transaction)
    {
      AutoCommit = false;
      _connection = connection;
      _transaction = transaction;
    }

    public virtual List<T> GetEntityList(T entity)
    {
      if (AutoCommit)
      {
        Connection.Open();
        var entityList = Connection.Find<T>().ToList();
        Connection.Close();

        return entityList;
      }
      else
      {
        var entityList = Connection.Find<T>(statement => statement.AttachToTransaction(_transaction)).ToList();
        return entityList;
      }
    }

    public void dispose()
    {
      if (_connection.State == ConnectionState.Open)
      {
        _connection.Close();
      }
    }
  }
}