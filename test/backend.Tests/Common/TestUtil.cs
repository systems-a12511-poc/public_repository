using System.Reflection;
using Xunit;

namespace backend.Tests.Common;

public class TestUtil
{
    /// <summary>
    /// 引数のリストにセットされている値を比較します。
    /// <param name="list"></param>
    /// <param name="compDatas"></param>
    /// </summary>
    public static void AssertEqual<T>(
        IEnumerable<T> expected,
        IEnumerable<T> actual
    )
    {
        var expectedList = expected.ToList();
        var actualList = actual.ToList();
        AssertEqual(expectedList, actualList);
    }

    /// <summary>
    /// 引数のリストにセットされている値を比較します。
    /// <param name="list"></param>
    /// <param name="compDatas"></param>
    /// </summary>
    public static void AssertEqual<T>(
        List<T> expected,
        List<T> actual
    )
    {
        Assert.Equal(expected.Count, actual.Count);
        var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
        for (int i = 0; i < expected.Count; i++)
        {
            foreach (var prop in properties)
            {
                var expectedValue = prop.GetValue(expected[i]);
                var actualValue = prop.GetValue(actual[i]);

                if (prop.PropertyType == typeof(string) && expectedValue != null && actualValue != null)
                {
                    Assert.Equal((string)expectedValue, (string)actualValue);
                }
                else
                {
                    Assert.Equal(expectedValue, actualValue);
                }
            }
        }
    }
}