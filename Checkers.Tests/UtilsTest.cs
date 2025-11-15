using NUnit.Framework;
using System;

namespace Checkers.Tests;

[TestFixture]
[TestOf(typeof(Utils))]
public class UtilsTest
{
    [Test]
    public void FindClosestToXTest()
    {
        Console.WriteLine(Utils.FindClosestToX(4, [1, 2, 5, 6]));
        Assert.That(Utils.FindClosestToX(4, [1, 2, 5, 6]) == 5);
        Assert.That(Utils.FindClosestToX(4, [1, 2, 4, 6]) == 4);
        Assert.That(Utils.FindClosestToX(4, [1, 3, 5, 6]) == 5);
        Assert.That(Utils.FindClosestToX(4, [1, 3, 5, 6], false) == 3);
    }
}
