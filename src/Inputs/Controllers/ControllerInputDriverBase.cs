using BIGFOOT.RGBMatrix.Inputs.Enums;
using BIGFOOT.RGBMatrix.Inputs.Models;
using System;
using System.Threading;

namespace BIGFOOT.RGBMatrix.Visuals.Inputs
{
    //TODO can be a static class I believe
    public abstract class ControllerInputDriverBase
    {
        public event EventHandler E_CONNECTION_SUCCESS = delegate { };
        public event EventHandler E_CONNECTION_FAIL = delegate { };
        public event EventHandler E_CONTROLLER_INPUT_RECEIVED = delegate { ControllerInputEvent Event; };

        protected abstract string DriverName { get; }

        private protected void FIRE_E_CONTROLLER_INPUT_RECEIVED(ControllerInputEvent inputEvent)
        {
            E_CONTROLLER_INPUT_RECEIVED?.Invoke(inputEvent, EventArgs.Empty);
        }

        private protected void FIRE_E_CONNECITON_SUCCESS()
        {
            E_CONNECTION_SUCCESS?.Invoke(this, EventArgs.Empty);
        }

        private protected void FIRE_E_CONNECITON_FAIL()
        {
            E_CONNECTION_FAIL?.Invoke(this, EventArgs.Empty);
        }

        public void EstablishControllerConnection()
        {
            Connect();
            Listen();
        }

        public virtual void Listen()
        {
            new Thread(() =>
            {
                Thread.CurrentThread.Name = $"INPUT_LISTENER_THREAD__{DriverName}";

                Console.WriteLine($"{Thread.CurrentThread.Name} => Listening \n\n\n");

                while (true)
                {
                    Update();
                }

            }).Start();
        }
        private protected abstract void Update();
        private protected abstract void Connect();
    }
}
