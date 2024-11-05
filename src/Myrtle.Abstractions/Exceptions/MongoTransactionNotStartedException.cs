namespace Myrtle.Abstractions.Exceptions;

/// <summary>
/// The exception thrown when attempting to perform operations on a MongoDB transaction that hasn't been started.
/// </summary>
public sealed class MongoTransactionNotStartedException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="MongoTransactionNotStartedException"/> class.
    /// </summary>
    public MongoTransactionNotStartedException()
        : base("No MongoDB transaction has been started. Call StartAsync before performing transaction operations.")
    {
    }
}