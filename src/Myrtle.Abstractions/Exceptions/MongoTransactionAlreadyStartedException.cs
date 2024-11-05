namespace Myrtle.Abstractions.Exceptions;

/// <summary>
/// The exception thrown when attempting to start a MongoDB transaction that is already active.
/// </summary>
public sealed class MongoTransactionAlreadyStartedException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="MongoTransactionAlreadyStartedException"/> class.
    /// </summary>
    public MongoTransactionAlreadyStartedException()
        : base("A MongoDB transaction is already in progress.")
    {
    }
}