using MongoDB.Driver;

namespace Myrtle.Extensions;

/// <summary>
/// Provides extension methods for IAsyncCursor{T} to enhance MongoDB cursor operations.
/// </summary>
/// <remarks>
/// This static class contains extension methods that add functionality to IAsyncCursor{T},
/// making it easier to work with MongoDB cursors in an asynchronous manner.
/// These methods are designed to simplify common patterns when dealing with 
/// asynchronous cursors in MongoDB operations.
/// </remarks>
public static class AsyncCursorExtensions
{
    /// <summary>
    /// Converts an IAsyncCursor{T} to an asynchronous enumerable.
    /// </summary>
    /// <typeparam name="T">The type of elements in the cursor.</typeparam>
    /// <param name="asyncCursor">The IAsyncCursor{T} to convert.</param>
    /// <returns>An IAsyncEnumerable{T} that can be used to asynchronously enumerate the cursor results.</returns>
    /// <remarks>
    /// This method provides a convenient way to asynchronously enumerate the results of a MongoDB cursor.
    /// It's particularly useful when working with large result sets, as it allows for efficient
    /// streaming of data without loading everything into memory at once.
    /// 
    /// Usage example:
    /// <code>
    /// var cursor = await collection.FindAsync(filter);
    /// await foreach (var item in cursor.ToAsyncEnumerable())
    /// {
    ///     // Process each item
    /// }
    /// </code>
    /// 
    /// Note: This method does not execute any additional queries. It simply provides a more
    /// convenient way to iterate over the cursor results asynchronously.
    /// </remarks>
    public static async IAsyncEnumerable<T> ToAsyncEnumerable<T>(this IAsyncCursor<T> asyncCursor)
    {
        while (await asyncCursor.MoveNextAsync())
        {
            foreach (var current in asyncCursor.Current)
            {
                yield return current;
            }
        }
    }
}