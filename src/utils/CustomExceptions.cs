namespace SpaceShooter.utils
{
    internal class InvalidMoveException : Exception
    {
        public InvalidMoveException(string message = "InvalidMoveException Raised") : base(message) { }
    }
}
