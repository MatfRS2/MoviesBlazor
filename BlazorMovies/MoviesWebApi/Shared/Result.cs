using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace MoviesWebApi.Shared
{
    public class Result
    {
        protected internal Result(bool isSuccess, Error error)
        {
            if (isSuccess && error != Error.None)
                throw new InvalidOperationException();
            if (!isSuccess && error == Error.None)
                throw new InvalidOperationException();
            IsSucess = isSuccess;
            Error = error;
        }
        public bool IsSucess { get; set; }

        public bool IsFaliure => !IsSucess;

        public Error Error { get; }

        public static Result Sucess() => new(true, Error.None);

        public static Result Faliure(Error error) => new(false, error);
    }
}
