using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kata06_Events
{
    public interface IMemberList
    {
        public IMember this[int idx] { get; }  
        
        public int Count();
        public int Count(int year);
        public void Sort();
        public void Add(IMember member);

        public EventHandler<IMember> PlatinumMemberEvent { get; set; }
        public EventHandler<IMember> GoldMemberEvent { get; set; }
    }
}
