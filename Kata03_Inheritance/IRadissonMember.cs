using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kata03_Inheritance
{
    public interface IRadissonMember : IMember  //Im showing the inheritance in the interface
    {
        public string RadissonOnly { get; set; }
    }
}
