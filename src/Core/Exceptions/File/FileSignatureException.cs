namespace Core.Exceptions.File
{
    public class FileSignatureException : Exception
    {
        public FileSignatureException() : base("Incorrect file signature.")
        {
        }

        public FileSignatureException(string message) : base(message)
        {
        }
    }
}
