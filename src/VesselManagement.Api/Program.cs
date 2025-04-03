using Microsoft.AspNetCore.Mvc;
using MediatR;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using VesselManagement.Api.Models.Commands;
using VesselManagement.Api.Models.Queries;
using SharpGrip.FluentValidation.AutoValidation.Endpoints.Extensions;
using System.Text.Json.Serialization;
using VesselManagement.Api.ExceptionHandlers;
using VesselManagement.Api.Data;
using VesselManagement.Api.Models.Requests;

var builder = WebApplication.CreateBuilder(args);

// Configure JSON options to use string enum converter
builder.Services.ConfigureHttpJsonOptions(options =>
{
	options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

builder.Services.Configure<JsonOptions>(options =>
{
	options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

// Register EF Core with an in-memory database.
builder.Services.AddDbContext<VesselDbContext>(options => options.UseInMemoryDatabase("VesselsDb"));

// Register MediatR (scans the current assembly for handlers).
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<Program>());

// Register FluentValidation validators.
builder.Services.AddValidatorsFromAssemblyContaining<Program>();
builder.Services.AddFluentValidationAutoValidation();

// Enable minimal API OpenAPI support (using built-in endpoints explorer).
builder.Services.AddEndpointsApiExplorer();

// Note: While the instructions mention .NET 9 Open API, here we use the minimal API support.
builder.Services.AddSwaggerGen();

// Add problem details & exception handler
builder.Services.AddProblemDetails();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

var app = builder.Build();

// Global exception handling middleware
app.UseExceptionHandler();

// Enable Open API / Swagger UI (for development purposes).
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var vessels = app.MapGroup("/api/vessels").AddFluentValidationAutoValidation();

// POST /api/vessels: Register a new vessel.
vessels.MapPost("/", async (RegisterVesselCommand command, IMediator mediator, IValidator<RegisterVesselCommand> validator) =>
{
	var vessel = await mediator.Send(command);
	return Results.Created($"/api/vessels/{vessel.Id}", vessel);
});

// PUT /api/vessels/{id}: Update an existing vessel.
vessels.MapPut("/{id:guid}", async (Guid id, UpdateVesselRequest request, IMediator mediator, IValidator<UpdateVesselRequest> validator) =>
{
	await mediator.Send(new UpdateVesselCommand(id, request.Name, request.IMO, request.Type, request.Capacity));
	return Results.NoContent();
});

// GET /api/vessels: Retrieve all vessels.
vessels.MapGet("/", async (IMediator mediator) =>
{
	var vessels = await mediator.Send(new GetAllVesselsQuery());
	return Results.Ok(vessels);
});

// GET /api/vessels/{id}: Retrieve a specific vessel by ID.
vessels.MapGet("/{id:guid}", async (Guid id, IMediator mediator) =>
{
	var vessel = await mediator.Send(new GetVesselByIdQuery(id));
	return Results.Ok(vessel);
});

app.Run();