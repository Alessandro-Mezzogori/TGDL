using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TGDLLib
{
    public delegate void GameActionDelegate(params object[] args);
    
    public class GameActionMetadata
    {

    }

    public class GameAction
    {
        // Require
        // Triggers
        // Results
        // parameters given to the function
        // delegate type 
        public GameActionDelegate Delegate { get; set; }

        public void CallAction(params object[] args)
        {
            Delegate(args);
        }
    }
}
