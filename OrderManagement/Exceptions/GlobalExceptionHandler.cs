using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace OrderManagement.Exceptions;

public class GlobalExceptionHandler : IExceptionHandler
{

    private readonly ILogger<GlobalExceptionHandler> _logger;

        public GlobalExceptionHandler(
            ILogger<GlobalExceptionHandler> logger)
        {
            _logger = logger;
        }

    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken) //CancellationToken je .NET mehanizam koji omogućava da se asinhronoj operaciji pošalje signal da treba da prekine izvršavanje, na primer kada klijent otkaže HTTP zahtev. Slicno kao interrupt u Springu. Cancellation je kooperativan i ne prekida nasilno operaciju.
    {
        if (exception is DbUpdateException
            {
                InnerException: PostgresException
                {
                    SqlState: PostgresErrorCodes.UniqueViolation,
                    ConstraintName: "IX_Products_Name"
                }
            })
        {
            httpContext.Response.StatusCode =
                StatusCodes.Status409Conflict;

            var conflict = new ProblemDetails
            {
                Status = StatusCodes.Status409Conflict,
                Title = "Conflict",
                Detail = "A product with this name already exists."
            };

            await httpContext.Response.WriteAsJsonAsync(
                conflict,
                cancellationToken);

            return true;
        }

        // Log the exception and return a generic error response
        _logger.LogError(
            exception,
            "An unexpected error occurred while processing {Path}",
            httpContext.Request.Path);  // Structured logging u .net nam omogucava da logujemo dodatne informacije o kontekstu u kojem se desila greška, kao što je putanja HTTP zahteva. Ovo je slično kao MDC (Mapped Diagnostic Context) u Logback-u ili Log4j-u u Javi, gde možemo dodati kontekstualne informacije koje će biti uključene u log poruke.

        httpContext.Response.StatusCode =
            StatusCodes.Status500InternalServerError;

        var problem = new ProblemDetails
        {
            Status = StatusCodes.Status500InternalServerError,
            Title = "Internal Server Error",
            Detail = "An unexpected error occurred."
        };

        await httpContext.Response.WriteAsJsonAsync(
            problem,
            cancellationToken);

        return true; // vraca true zato što smo izuzetak obradili i poslali HTTP odgovor

    }
}