namespace EventManager.App.Api.Basic.Models;

using EventManager.App.Api.Basic.Constants;
using System.Net;

/// <summary>
/// The <see cref="OpResult{T}"/> class represents the operation result.
/// </summary>
/// <typeparam name="T">The type of the result data.</typeparam>
public class OpResult<T>
{
    /// <summary>
    /// Gets or sets the HTTP status code of the operation result.
    /// </summary>
    public HttpStatusCode Status { get; set; }

    /// <summary>
    /// Gets or sets the error code associated with the operation result.
    /// </summary>
    public ErrorCode ErrorCode { get; set; }

    /// <summary>
    /// Gets or sets the result data of the operation.
    /// </summary>
    public T Result { get; set; }
}
