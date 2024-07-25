namespace Core.Exceptions.File
{
    public class FileSizeException : Exception
    {
        public FileSizeException() : base("File size too large.")
        {
        }

        public FileSizeException(string message) : base(message)
        {
        }
    }
}
