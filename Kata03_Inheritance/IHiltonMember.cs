using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kata03_Inheritance
{
    public interface IHiltonMember : IMember  //Im showing the inheritance in the interface
    {
        public string HiltonOnly { get; set; }
    }
}
