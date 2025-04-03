namespace VesselManagement.Api.Exceptions;

/// <summary>
/// Custom exception for not found entities.
/// </summary>
public class EntityNotFoundException(string message) : Exception(message)
{ }