using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.EntityFrameworkCore.Query;
using System.Runtime.InteropServices;

namespace MoviesWebApi.Shared
{
    public class Result<TValue>: Result
    {
        private readonly TValue _value; 

        protected internal Result(TValue value, bool isSuccess, Error error):
            base(isSuccess, error)
        {
            _value = value;
        }    
        
        public TValue Value => IsSucess ?
            _value :
            throw new InvalidOperationException("Ne može se pristupiti vrednosti rezultata kada operacija nije uspešna");

        public static Result<TValue> Sucess(TValue value) => new(value, true, Error.None);

        public static new Result<TValue> Faliure(Error error) => new(default(TValue), false, error);

        public static implicit operator Result<TValue>(TValue? value) => Create(value);

        private static Result<TValue> Create(TValue? value)
        {
            return Result<TValue>.Sucess(value);
        }
    }
}
