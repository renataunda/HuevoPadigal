namespace PadigalAPI.Exceptions
{
    public class NotFoundException : Exception
    {
        public string ResourceName { get; }
        public int ResourceId { get; }

        public NotFoundException(string resourceName, int resourceId, string message) : base(message)
        {
            ResourceName = resourceName;
            ResourceId = resourceId;
        }
    }

}
