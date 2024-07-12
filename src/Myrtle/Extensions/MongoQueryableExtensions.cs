using MongoDB.Driver.Linq;

namespace Myrtle.Extensions;

/// <summary>
/// Provides extension methods for IMongoQueryable to enhance MongoDB query capabilities.
/// </summary>
/// <remarks>
/// This static class contains extension methods that add functionality to IMongoQueryable,
/// making it easier to work with MongoDB queries in a LINQ-like manner.
/// These methods are designed to bridge gaps between standard LINQ operations and 
/// MongoDB-specific querying needs.
/// </remarks>
public static class MongoQueryableExtensions
{
    /// <summary>
    /// Converts an IMongoQueryable to an asynchronous enumerable.
    /// </summary>
    /// <typeparam name="T">The type of elements in the queryable.</typeparam>
    /// <param name="queryable">The IMongoQueryable to convert.</param>
    /// <returns>An IAsyncEnumerable{T} that can be used to asynchronously enumerate the results.</returns>
    /// <remarks>
    /// This method allows for efficient, asynchronous enumeration of MongoDB query results.
    /// It's particularly useful in scenarios where you want to process large result sets
    /// without loading all data into memory at once.
    /// 
    /// Usage example:
    /// <code>
    /// await foreach (var item in collection.AsQueryable().Where(x => x.IsActive).ToAsyncEnumerable())
    /// {
    ///     // Process each item
    /// }
    /// </code>
    /// 
    /// Note: This method executes the query against the database. Ensure that your query
    /// is optimized to avoid performance issues with large datasets.
    /// </remarks>
    public static async IAsyncEnumerable<T> ToAsyncEnumerable<T>(this IMongoQueryable<T> queryable)
    {
        var asyncCursor = await queryable.ToCursorAsync();
        while (await asyncCursor.MoveNextAsync())
        {
            foreach (var current in asyncCursor.Current)
            {
                yield return current;
            }
        }
    }
}