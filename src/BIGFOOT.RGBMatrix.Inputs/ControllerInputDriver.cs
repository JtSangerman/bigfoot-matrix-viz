using System;
using System.Collections.Generic;
using System.Text;

namespace BIGFOOT.RGBMatrix.Visuals.Inputs
{
    //TODO can be a static class I believe
    public abstract class ControllerInputDriver
    {
        public event EventHandler E_CONNECTION_SUCCESS = delegate { };
        public event EventHandler E_CONNECTION_FAIL = delegate { };

        public event EventHandler E_INPUT_UP = delegate { string type; };
        public event EventHandler E_INPUT_DOWN = delegate { };
        public event EventHandler E_INPUT_LEFT = delegate { };
        public event EventHandler E_INPUT_RIGHT = delegate { };
        public event EventHandler E_INPUT_NONE = delegate { };

        private protected event EventHandler E_INPUT_START = delegate { };
        public event EventHandler E_INPUT_EXT1 = delegate { };
        public event EventHandler E_INPUT_EXT2 = delegate { };

        private protected void FIRE_E_CONNECITON_SUCCESS()
        {
            E_CONNECTION_SUCCESS?.Invoke(this, EventArgs.Empty);
        }

        private protected void FIRE_E_CONNECITON_FAIL()
        {
            E_CONNECTION_FAIL?.Invoke(this, EventArgs.Empty);
        }

        private protected void FIRE_E_INPUT_LEFT()
        {
            E_INPUT_LEFT?.Invoke(this, EventArgs.Empty);
        }

        private protected void FIRE_E_INPUT_RIGHT()
        {
            E_INPUT_RIGHT?.Invoke(this, EventArgs.Empty);
        }

        private protected void FIRE_E_INPUT_DOWN()
        {
            E_INPUT_DOWN?.Invoke(this, EventArgs.Empty);
        }

        private protected void FIRE_E_INPUT_UP()
        {
            E_INPUT_UP?.Invoke(this, EventArgs.Empty);
        }

        private protected void FIRE_E_INPUT_NONE()
        {
            E_INPUT_NONE?.Invoke(this, EventArgs.Empty);
        }

        public void EstablishControllerConnection()
        {
            Connect();
            Listen();
        }

        private protected abstract void Connect();
        public abstract void Listen();
    }
}
