using backend.DbContext;
using backend.Features.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Xunit;

namespace backend.Tests.Features.User;

public class GetUserControllerTest(TestFixture testFixture) : IClassFixture<TestFixture>
{
    private readonly MyDbContext dbContext = testFixture.DbContext;
    private readonly ILogger logger = testFixture.Logger;

    [Fact]
    public async Task Test001()
    {
        var controller = new GetUserController(dbContext, logger);
        var result = await controller.GetUser(new GetUserReqBody { Id = "1" });
        var typedResult = Assert.IsType<OkObjectResult>(result);
        var entity = Assert.IsType<List<UserEntity>>(typedResult.Value);
        Assert.Equal("Taro", entity.First().Name);
    }
}