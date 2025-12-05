using System;
using System.Collections.Generic;

namespace Kata02_IEquatable_IComparable_Factory
{
    public class MemberList : IMemberList
    {
        List<IMember> _members = new List<IMember>();

        public IMember this[int idx] => _members[idx];
        public int Count() => _members.Count;
        public int Count(int year) =>_members.Count(item => item.Since.Year == year);
        public void Sort() => _members.Sort();
        public void Add(IMember member) => _members.Add(member);
        public override string ToString()
        {
            string sRet = "";
            for (int i = 0; i < _members.Count; i++)
            {
                sRet += $"{_members[i]}\n";
                if ((i + 1) % 10 == 0)
                {
                    sRet += "\n";
                }
            }
            return sRet;
        }


        public MemberList() { }

        //Copy constructorn has to take a parameter of type MemberList to be
        //able to access and copy _members which is private
        public MemberList(MemberList org)
        {
            //Reference Copy
            _members = org._members;
            
            //Shallow Copy
            _members = new List<IMember>(org._members);

            //Deep copy using Linq
            _members = org._members.Select(o => new Member(o)).ToList<IMember>();
        }
    }
}
