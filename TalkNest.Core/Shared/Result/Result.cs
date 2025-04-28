using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

namespace TalkNest.Core.Shared.Result
{
    public class Result
    {

        public bool IsSuccess { get; }

        public bool IsFailure => !IsSuccess;

        public Error Error { get; }

        public IEnumerable<Error> Errors { get; } = Enumerable.Empty<Error>();

        [JsonConstructor]
        public Result(bool isSuccess, Error error, IEnumerable<Error> errors)
        {
            IsSuccess = isSuccess;
            Error = error;
            Errors = errors ?? Enumerable.Empty<Error>();
        }
        protected internal Result(bool isSuccess, Error error)
        {
            switch (isSuccess)
            {
                case true when error != Error.None:
                    throw new InvalidOperationException();
                case false when error == Error.None:
                    throw new InvalidOperationException();
                default:
                    IsSuccess = isSuccess;
                    Error = error;
                    break;
            }
        }

        protected internal Result(bool isSuccess, IEnumerable<Error> errors)
        {
            switch (isSuccess)
            {
                case true when errors.Any():
                    throw new InvalidOperationException();
                case false when !errors.Any():
                    throw new InvalidOperationException();
                default:
                    IsSuccess = isSuccess;
                    Errors = errors;
                    break;
            }
        }


        public static Result Success()
        {
            Result result = new Result(true, Error.None);
            return result;
        }

        public static Result<TValue> Success<TValue>(TValue value) =>
            new Result<TValue>(value, true, Error.None);

        public static Result Failure(Error error) =>
            new Result(false, error);

        public static Result Failure(IEnumerable<Error> errors) =>
            new Result(false, errors);

        public static Result<TValue> Failure<TValue>(Error error) =>
            new Result<TValue>(default, false, error);

        public static Result<TValue> Create<TValue>(TValue value)
        {
            return value != null ? Success(value) : Failure<TValue>(Error.NullValue);
        }

        public static Result<TValue> Create<TValue>(TValue value, Error error) where TValue : class =>
            value is null ? Failure<TValue>(error) : Success(value);

        public static Result FirstFailureOrSuccess(params Result[] results)
        {
            foreach (Result result in results)
            {
                if (result.IsFailure)
                {
                    return result;
                }
            }

            return Success();
        }
    }
}