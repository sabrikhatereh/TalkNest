using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TalkNest.Core.Shared.Result
{
    public class Result<TValue> : Result
    {
        private readonly TValue _value;

        [JsonConstructor]
        public Result(TValue value, bool isSuccess, Error error,
            IEnumerable<Error> errors) : base(isSuccess, error, errors)
        {
            _value = value;
        }


        protected internal Result(TValue value, bool isSuccess, Error error)
            : base(isSuccess, error)
        {
            _value = value;
        }

        public TValue Value => IsSuccess
            ? _value!
            : throw new InvalidOperationException("The value of a failure result can not be accessed.");

        public static implicit operator Result<TValue>(TValue value) => Create(value);
    }
}