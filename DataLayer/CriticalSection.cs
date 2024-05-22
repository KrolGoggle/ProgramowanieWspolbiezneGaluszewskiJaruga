using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class CriticalSection
    {
        private readonly object _lockObject = new object();

        public void Enter(Action criticalAction)
        {
            lock (_lockObject)
            {
                criticalAction();
            }
        }
    }
}
