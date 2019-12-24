using System;
using System.Collections.Generic;
using System.Text;

namespace DemoApplication.Events
{
    public class AccountChangeEventArgs : EventArgs
    {
        public AccountChangeEventArgs(int index)
        {
            AccountIndex = index;
        }
        public int AccountIndex { get; }
    }
}
