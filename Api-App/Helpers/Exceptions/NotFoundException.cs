namespace Api_App.Helpers.Exceptions
{
    public class NotFoundException:Exception
    {
        public NotFoundException(string message="Not found") : base(message) { }
    }
}
