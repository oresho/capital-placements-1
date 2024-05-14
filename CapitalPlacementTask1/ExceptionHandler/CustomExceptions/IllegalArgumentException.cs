namespace CapitalPlacementTask1.ExceptionHandler.CustomExceptions
{
    [Serializable]
    public class IllegalArgumentException : Exception
    {
        public IllegalArgumentException()
        {

        }
        public IllegalArgumentException(string? message)
            : base(message)
        {
        }
    }
}
