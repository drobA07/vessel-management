namespace VesselManagement.Api.Exceptions;

/// <summary>
/// Custom exception for existing entities.
/// </summary>
public class EntityExistsException(string message) : Exception(message)
{ }
