using System.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Npgsql;

namespace backend.DbContext;
public class MyDbContext : IDisposable
{
    private readonly ILogger<MyDbContext> _logger;
    private readonly IDbConnection _connection;
    private IDbTransaction? _transaction;
    private bool _transactionStarted;

    public MyDbContext(IConfiguration configuration, ILogger<MyDbContext> logger)
    {
        _logger = logger;
        var connectionString = Environment.GetEnvironmentVariable("connectionstring");
        // var connectionString = "Host=10.0.184.8;Port=5432;Database=postgres;Username=postgres;Password=systems123;Client Encoding=UTF8;";
        if (string.IsNullOrEmpty(connectionString))
        {
            throw new InvalidOperationException("The ConnectionString property has not been initialized.");
        }

        _connection = new NpgsqlConnection(connectionString);
        _transactionStarted = false;
    }

    public IDbConnection Connection => _connection;
    public IDbTransaction? Transaction => _transaction;

    public void BeginTran()
    {
        if (!_transactionStarted)
        {
            _transaction = _connection.BeginTransaction();
            _transactionStarted = true;
        }
    }

    public void Dispose()
    {
        if (_connection.State == ConnectionState.Open)
        {
            _transaction?.Dispose();
            _connection?.Dispose();
        }
    }

    public void Commit()
    {
        if (_transactionStarted && _transaction != null)
        {
            _transaction.Commit();
        }
    }

    public void TransactionStoped()
    {
        if (_transactionStarted && _transaction != null && Connection.State == ConnectionState.Open)
        {
            _transactionStarted = false;
        }
    }
}

