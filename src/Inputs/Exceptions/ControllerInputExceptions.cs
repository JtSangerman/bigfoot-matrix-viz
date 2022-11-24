using System;
using System.Collections.Generic;
using System.Text;


namespace BIGFOOT.RGBMatrix.ControllerInput.Exceptions
{
    public class InputControllerNotConnectedException : Exception
    {
        public InputControllerNotConnectedException(string message, Exception innerException = null) : base(message, innerException) { }
    }

    public class ControllerInputNotRecognized : Exception
    {
        public ControllerInputNotRecognized(string message, Exception innerException = null) : base(message, innerException) { }
    }
}

