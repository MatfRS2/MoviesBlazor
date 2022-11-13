using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.EntityFrameworkCore.Query;
using System.Runtime.InteropServices;

namespace MoviesWebApi.Shared
{
    public class ResultT<TValue>: Result
    {
        private readonly TValue _value; 

        protected internal ResultT(TValue value, bool isSuccess, Error error):
            base(isSuccess, error)
        {
            _value = value;
        }    
        
        public TValue Value => IsSucess ?
            _value :
            throw new InvalidOperationException("Ne može se pristupiti vrednosti rezultata kada operacija nije uspešna");

        public static ResultT<TValue> Sucess(TValue value) => new(value, true, Error.None);

        public static ResultT<TValue> Faliure(TValue? value, Error error) => new(value, false, error);


    }
}
