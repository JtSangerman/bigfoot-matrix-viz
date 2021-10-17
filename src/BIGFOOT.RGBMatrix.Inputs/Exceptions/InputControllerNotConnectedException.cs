﻿using System;
using System.Collections.Generic;
using System.Text;


namespace BIGFOOT.RGBMatrix.Inputs.Exceptions
{
    public class InputControllerNotConnectedException : Exception
    {
        public InputControllerNotConnectedException(string message, Exception? innerException = null) : base(message, innerException) { }
    }

}

