namespace Myrtle.Abstractions;

using MongoDB.Driver;
using Exceptions;

/// <summary>
/// Represents a context for managing the lifecycle of a MongoDB transaction.
/// Ensures proper handling of MongoDB transactions, including starting, committing, and cleaning up resources.
/// Implements IAsyncDisposable for automatic cleanup of transaction resources.
/// </summary>
/// <remarks>
/// This context should be used within a using block to ensure proper resource cleanup.
/// All operations within the transaction should be performed between calls to StartAsync and CommitAsync.
/// If an exception occurs, the transaction should be aborted using AbortAsync.
/// </remarks>
public interface IMongoTransactionContext : IAsyncDisposable
{
    /// <summary>
    /// Gets a value indicating whether a transaction is currently active within this context.
    /// </summary>
    /// <value>
    /// True if a transaction has been started and not yet committed or aborted; otherwise, false.
    /// </value>
    bool IsActive { get; }

    /// <summary>
    /// Gets the current MongoDB session handle associated with this transaction context.
    /// </summary>
    /// <value>
    /// The MongoDB client session handle managing the current transaction.
    /// </value>
    /// <exception cref="MongoTransactionNotStartedException">
    /// Thrown when accessing the session before starting a transaction.
    /// </exception>
    IClientSessionHandle Session { get; }

    /// <summary>
    /// Starts a new MongoDB transaction within this context.
    /// </summary>
    /// <param name="cancellationToken">A token to cancel the async operation.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    /// <exception cref="MongoTransactionAlreadyStartedException">
    /// Thrown when attempting to start a transaction while another is already active.
    /// </exception>
    /// <exception cref="MongoException">
    /// Thrown when the MongoDB server encounters an error while starting the transaction.
    /// </exception>
    Task StartAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Commits the current MongoDB transaction.
    /// </summary>
    /// <param name="cancellationToken">A token to cancel the async operation.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    /// <exception cref="MongoTransactionNotStartedException">
    /// Thrown when attempting to commit a transaction that hasn't been started.
    /// </exception>
    /// <exception cref="MongoException">
    /// Thrown when the MongoDB server encounters an error while committing the transaction.
    /// </exception>
    Task CommitAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Aborts the current MongoDB transaction.
    /// </summary>
    /// <param name="cancellationToken">A token to cancel the async operation.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    /// <exception cref="MongoTransactionNotStartedException">
    /// Thrown when attempting to abort a transaction that hasn't been started.
    /// </exception>
    /// <exception cref="MongoException">
    /// Thrown when the MongoDB server encounters an error while aborting the transaction.
    /// </exception>
    Task AbortAsync(CancellationToken cancellationToken = default);
}