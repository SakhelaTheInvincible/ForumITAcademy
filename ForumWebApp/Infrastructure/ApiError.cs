using Microsoft.AspNetCore.Mvc;
using System.Net;
using Forum.Application.Exceptions;
using ForumWebApp.Infrastructure.Localization;

namespace ForumWebApp.Infrastructure
{
    public class ApiError : ProblemDetails
    {
        public const string UnhandlerErrorCode = "UnhandledError";
        private HttpContext _httpContext;
        private Exception _exception;

        public LogLevel LogLevel { get; set; }
        public string Code { get; set; }

        public string? TraceId
        {
            get
            {
                if (Extensions.TryGetValue("TraceId", out var traceId))
                {
                    return (string?)traceId;
                }

                return null;
            }

            set => Extensions["TraceId"] = value;
        }

        public ApiError(HttpContext httpContext, Exception exception)
        {
            _httpContext = httpContext;
            _exception = exception;

            TraceId = httpContext.TraceIdentifier;

            //default
            Code = UnhandlerErrorCode;
            Status = (int)HttpStatusCode.InternalServerError;
            Title = ErrorMessages.Error;
            LogLevel = LogLevel.Error;
            Instance = httpContext.Request.Path;

            HandleException((dynamic)exception);
        }


        private void HandleException(UserNotFoundException exception)
        {
            Code = exception.Code;
            Status = (int)HttpStatusCode.NotFound;
            Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.4";
            Title = ErrorMessages.UserNotFound;
            LogLevel = LogLevel.Information;
        }

        private void HandleException(TopicNotFoundException exception)
        {
            Code = exception.Code;
            Status = (int)HttpStatusCode.NotFound;
            Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.4";
            Title = ErrorMessages.TopicNotFound;
            LogLevel = LogLevel.Information;
        }

        private void HandleException(CommentNotFoundException exception)
        {
            Code = exception.Code;
            Status = (int)HttpStatusCode.NotFound;
            Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.4";
            Title = ErrorMessages.CommentNotFound;
            LogLevel = LogLevel.Information;
        }

        private void HandleException(LoginException exception)
        {
            Code = exception.Code;
            Status = (int)HttpStatusCode.BadRequest;
            Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.1";
            Title = ErrorMessages.LoginError;
            LogLevel = LogLevel.Information;
        }

        private void HandleException(InactiveTopicException exception)
        {
            Code = exception.Code;
            Status = (int)HttpStatusCode.BadRequest;
            Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.1";
            Title = ErrorMessages.InactiveTopicException;
            LogLevel = LogLevel.Information;
        }

        private void HandleException(UserBannedException exception)
        {
            Code = exception.Code;
            Status = (int)HttpStatusCode.Unauthorized;
            Type = "https://datatracker.ietf.org/doc/html/rfc7235#section-3.1";
            Title = ErrorMessages.UserIsBanned;
            LogLevel = LogLevel.Information;
        }

        private void HandleException(OtherCommentException exception)
        {
            Code = exception.Code;
            Status = (int)HttpStatusCode.Unauthorized;
            Type = "https://datatracker.ietf.org/doc/html/rfc7235#section-3.1";
            Title = ErrorMessages.ModificationError;
            LogLevel = LogLevel.Information;
        }

        private void HandleException(CommentCountException exception)
        {
            Code = exception.Code;
            Status = (int)HttpStatusCode.Unauthorized;
            Type = "https://datatracker.ietf.org/doc/html/rfc7235#section-3.1";
            Title = ErrorMessages.CommentCountException;
            LogLevel = LogLevel.Information;
        }

        private void HandleException(OtherTopicException exception)
        {
            Code = exception.Code;
            Status = (int)HttpStatusCode.Unauthorized;
            Type = "https://datatracker.ietf.org/doc/html/rfc7235#section-3.1";
            Title = ErrorMessages.ModificationError;
            LogLevel = LogLevel.Information;
        }

        private void HandleException(OtherUserException exception)
        {
            Code = exception.Code;
            Status = (int)HttpStatusCode.Unauthorized;
            Type = "https://datatracker.ietf.org/doc/html/rfc7235#section-3.1";
            Title = ErrorMessages.ModificationError;
            LogLevel = LogLevel.Information;
        }

        private void HandleException(EmailAlreadyExistsException exception)
        {
            Code = exception.Code;
            Status = (int)HttpStatusCode.Conflict;
            Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.8";
            Title = ErrorMessages.UserEmailUnique;
            LogLevel = LogLevel.Information;
        }

        private void HandleException(UserNameAlreadyExistsException exception)
        {
            Code = exception.Code;
            Status = (int)HttpStatusCode.Conflict;
            Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.8";
            Title = ErrorMessages.UserNameUnique;
            LogLevel = LogLevel.Information;
        }

        private void HandleException(Exception exception)
        {
            Code = exception.Message;
            Status = (int)HttpStatusCode.BadRequest;
            Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.1";
            Title = ErrorMessages.Error;
            LogLevel = LogLevel.Information;
        }
    }
}