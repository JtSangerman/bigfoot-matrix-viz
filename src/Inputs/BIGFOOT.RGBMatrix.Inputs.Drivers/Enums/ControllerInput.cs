using System;
using System.Collections.Generic;
using System.Text;

namespace BIGFOOT.RGBMatrix.Inputs.Drivers.Enums
{
    public enum ControllerInput
    {
        UP,
        DOWN,
        LEFT,
        RIGHT,


        // ex: Pause, Start
        EXT1,
        // ex: Select
        EXT2,

        // default, usually unhandled
        NONE
    }
}
