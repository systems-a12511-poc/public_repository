using Microsoft.AspNetCore.Mvc;
using backend.DbContext;
using Microsoft.Extensions.Logging;
using Dapper;

namespace backend.Features.User;

[ApiController]
[Route("api/")]
public class GetUserController(MyDbContext dbContext, ILogger logger) : ControllerBase
{
    private readonly MyDbContext _dbContext = dbContext;
    private readonly ILogger logger = logger;

    /// <summary>
    /// 技術検証用API
    /// </summary>
    /// <param name="req"></param>
    /// <returns></returns>
    [HttpGet("user")]
    public async Task<IActionResult> GetUser(GetUserReqBody req)
    {
        var sql = @"Select id,name From public.users where id = @Id;";
        var parameters = new { req.Id };
        logger.LogDebug("${sql}", sql);
        var selectResult = await _dbContext.Connection.QueryAsync<UserEntity>(sql, parameters);
        return Ok(selectResult);
    }
}

