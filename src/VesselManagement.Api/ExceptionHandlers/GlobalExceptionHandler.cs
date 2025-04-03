using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using VesselManagement.Api.Exceptions;

namespace VesselManagement.Api.ExceptionHandlers;

public class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger) : IExceptionHandler
{
	public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
	{
		var statusCode = exception switch
		{
			EntityNotFoundException => (int)HttpStatusCode.NotFound,
			EntityExistsException => (int)HttpStatusCode.Conflict,
			ValidationException => (int)HttpStatusCode.BadRequest,
			_ => (int)HttpStatusCode.InternalServerError
		};

		const string ErrorMessage = "An error occurred while processing your request.";

		var problemDetails = new ProblemDetails
		{
			Title = ErrorMessage,
			Detail = exception?.Message,
			Status = statusCode
		};

		if (exception is ValidationException fluentException)
		{
			var validationErrors = fluentException.Errors.Select(error => error.ErrorMessage).ToList();
			if (validationErrors.Count != 0)
			{
				problemDetails.Extensions.Add("errors", validationErrors);
			}
		}

		logger.LogError(exception, ErrorMessage);

		httpContext.Response.StatusCode = statusCode;
		httpContext.Response.ContentType = "application/problem+json";

		await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

		return true;
	}
}