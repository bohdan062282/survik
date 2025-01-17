using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gameCore
{
    internal class Weapon : Item, IHoldable
    {

        public Weapon(int height, int width, bool isPrimary) : base(height, width) { IsPrimary = isPrimary; }

        public bool IsPrimary { get; private set; }

    }
}
