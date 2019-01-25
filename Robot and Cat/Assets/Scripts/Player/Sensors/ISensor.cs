using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotCat.Player
{
    public interface ISensor
    {
        void Tick(float deltaTime);
    }
}
