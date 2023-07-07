namespace SpaceShooter.utils
{
    public class CustomExceptions
    {
        public class EntryNotFoundException : Exception
        {
            public EntryNotFoundException() { }

            public EntryNotFoundException(string message) : base(message) { }
        }
    }
}
