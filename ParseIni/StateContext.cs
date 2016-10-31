using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParseIni
{
    public class StateContext
    {
        public StateContext(IState state)
        {
            State = state;
        }

        public IState State { get; set; }

        public void StateRequest()
        {
            State.StateChange(this);
        }
    }
}
