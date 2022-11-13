namespace MoviesWebApi.Shared
{
    public class Error: IEquatable<Error>
    {
        public string Code { get; }
        public string Message { get; }

        public Error(string code, string message)
        {
            Code = code;
            Message = message;
        }

        public static readonly Error None = new Error(String.Empty, String.Empty);
        public static readonly Error NullValue = new Error("Error.NullValue", "Specificirana je null vrednost");

        public static implicit operator string(Error error) => error.Code;

        public static bool operator== (Error? err1, Error? err2)
        {
            if (err1 is null && err2 is null)
                return true;
            if (err1 is null || err2 is null)
                return false;
            return err1.Code == err2.Code;

        }

        public static bool operator !=(Error? err1, Error? err2)
        {
            return !(err1 == err2);
        }

        public bool Equals(Error? other)
        {
            return (this == other);
        }
    }
}
