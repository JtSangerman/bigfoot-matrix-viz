using System;
using System.Collections.Generic;
using System.Text;

namespace BIGFOOT.RGBMatrix.Visuals.Inputs
{
    public abstract class ControllerInput
    {
        public bool Connected { get; protected set; }

        public abstract void Connect();

        public virtual void Up()
        {
            Console.WriteLine("UP from base class");
        }

        public virtual void Down()
        {

        }

        //public void Left();
        //public void Right();
        //public void Ext1();
        //public void Ext2();
        //public bool IsConnected();
    }
}
