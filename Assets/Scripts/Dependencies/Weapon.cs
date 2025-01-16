using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gameCore
{
    internal class Weapon : Item, IHoldable
    {

        public Weapon(int width, int height) : base(width, height) { }

    }
}
