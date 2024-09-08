namespace Core.Exceptions.Permission
{
    public class PermissionException : Exception
    {
        public PermissionException() : base("You do not have permission to perform this action")
        {
        }

        public PermissionException(string message) : base(message)
        {
        }
    }
}
