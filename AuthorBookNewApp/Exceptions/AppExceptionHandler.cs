using AuthorBookNewApp.Exceptions.Author;
using AuthorBookNewApp.Exceptions.Book;
using AuthorBookNewApp.Models;
using Microsoft.AspNetCore.Diagnostics;

namespace AuthorBookNewApp.Exceptions
{
    public class AppExceptionHandler : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext httpcontext,
            Exception exception, CancellationToken cancellationToken)
        {
            var response = new ErrorResponse();
            if (exception is INotFoundException)
            {
                response.StatusCode = StatusCodes.Status404NotFound;
                response.Message = exception.Message;
                response.Title = "Wrong Input";
            }
            else
            {
                response.StatusCode = StatusCodes.Status500InternalServerError;
                response.Message = exception.Message;
                response.Title = "Something Went Wrong";
            }
            await httpcontext.Response.WriteAsJsonAsync(response, cancellationToken);
            return true;
        }
    }
}
