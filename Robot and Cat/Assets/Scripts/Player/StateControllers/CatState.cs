using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotCat.Player
{
    public class CatState
    {
        public enum States
        {
            IsGrounded, IsActive
        }

        private Dictionary<States, object> stateDict = new Dictionary<States, object>();

        public bool CheckBool(States state)
        {
            if (stateDict.ContainsKey(state))
            {
                var val = stateDict[state];
                if (val.GetType().Equals(typeof(bool)))
                {
                    return (bool)stateDict[state];
                }
            }
            return false;
        }
    }
}
