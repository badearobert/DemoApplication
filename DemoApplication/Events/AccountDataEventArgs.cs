using System;
using System.Collections.Generic;
using System.Text;

namespace DemoApplication.Events
{
    public class AccountDataEventArgs : EventArgs
    {
        public AccountDataEventArgs(int index)
        {
            AccountIndex = index;
        }
        public int AccountIndex { get; }
    }
}
