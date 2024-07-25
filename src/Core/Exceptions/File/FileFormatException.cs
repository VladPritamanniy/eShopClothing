namespace Core.Exceptions.File
{
    public class FileFormatException : Exception
    {
        public FileFormatException() : base("Incorrect file format.")
        {
        }

        public FileFormatException(string message) : base(message)
        {
        }
    }
}
