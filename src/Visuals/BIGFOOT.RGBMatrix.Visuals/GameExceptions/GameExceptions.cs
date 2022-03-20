using System;

namespace BIGFOOT.RGBMatrix.Visuals.GameExceptions
{
    public class PauseScreenConditionException : Exception
    {
        public PauseScreenConditionException(string message, Exception? innerException = null) : base(message, innerException) { }
    }
}
