using backend.DbContext;
using Xunit;

namespace backend.Tests.Common;

/// <summary>
/// 各テストクラスの基底処理
/// テストクラスで共通の変数や処理を実装する
/// </summary>
public class TestClassBase(TestFixture testFixture) : IClassFixture<TestFixture>
{
    protected readonly MyDbContext _dbContext = testFixture.DbContext;

}