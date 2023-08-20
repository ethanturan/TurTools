namespace TurTools.Utilities.Tests;

public class EnumerableExtensionTests
{
    [Fact]
    public void WhereNotNull_FiltersNullStrings()
    {
        // Arrange + Act
        var result = new List<string?>
        {
            "a", "b", "c", null, "d", null
        }.WhereNotNull().ToList();

        var expectedList = new List<string>
        {
            "a", "b", "c", "d"
        };

        // Assert
        Assert.Equal(4, result.Count);
        Assert.Equal(4, result.Intersect(expectedList).Count());
    }
    
    [Fact]
    public void WhereNotNull_FiltersNullableString_ToEmpty()
    {
        // Arrange + Act
        var result = new List<string?>
        {
            null, null, null, null
        }.WhereNotNull().ToArray();


        // Assert
        Assert.NotNull(result);
        Assert.Empty(result);
    }

    [Fact]
    public void WhereNotNull_FiltersNullableInt()
    {
        // Arrange + Act
        var result = new List<int?>
        {
            1, null, 2, null, 3, null, null
        }.WhereNotNull().ToList();

        var expectedList = new List<int>
        {
            1, 2, 3
        };

        // Assert
        Assert.Equal(3, result.Count);
        Assert.Equal(3, result.Intersect(expectedList).Count());
    }
    
    [Fact]
    public void WhereNotNull_FiltersNullableInt_ToEmpty()
    {
        // Arrange + Act
        var result = new List<int?>
        {
            null, null, null, null
        }.WhereNotNull().ToArray();


        // Assert
        Assert.NotNull(result);
        Assert.Empty(result);
    }
    
    [Fact]
    public void WhereNotNull_FiltersNullableTuple()
    {
        // Arrange + Act
        var result = new List<Tuple<int, string>?>
        {
            new(1, "a"), 
            null,
            new(2, "b"), 
            new(3, "c"),
            null, 
            null
        }.WhereNotNull().ToList();

        var expectedList = new List<Tuple<int, string>>
        {
            new(1, "a"),
            new(2, "b"), 
            new(3, "c")
        };

        // Assert
        Assert.Equal(3, result.Count);
        Assert.Equal(3, result.Intersect(expectedList).Count());
    }
    
    [Fact]
    public void WhereNotNull_FiltersNullableTuple_ToEmpty()
    {
        // Arrange + Act
        var result = new List<Tuple<int, string>?>
        {
            null, null, null, null
        }.WhereNotNull().ToArray();


        // Assert
        Assert.NotNull(result);
        Assert.Empty(result);
    }
}