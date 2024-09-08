namespace Core.Exceptions.AV
{
    public class InfectedFileException : Exception
    {
        public InfectedFileException() : base("AV found infected file")
        {
        }

        public InfectedFileException(string message) : base(message)
        {
        }
    }
}
