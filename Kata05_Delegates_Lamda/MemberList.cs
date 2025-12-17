using System;
using System.Collections.Generic;

namespace Kata05_Delegates_Lamda
{
    public class MemberList : IMemberList
    {
        List<IMember> _members = new List<IMember>();

        public IMember this[int idx] => _members[idx];
        public int Count() => _members.Count;
        public int Count(int year) =>_members.Count(item => item.Since.Year == year);
        public void Sort() => _members.Sort();
        public void Add(IMember member) => _members.Add(member);

        public IEnumerable<IMember> Filter(Func<IMember, bool> predicate) => _members.FindAll(m => predicate(m));

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

        #region Class Factory for creating an instance filled with Random data
        public static class Factory
        {
            public static MemberList CreateRandom(int NrOfItems, Func<IMember, IMember> NewMemberAction = null)
            {
                var memberlist = new MemberList();
                for (int i = 0; i < NrOfItems; i++)
                {
                    var newMember = Member.Factory.CreateRandom();
                    if (NewMemberAction != null)
                    {
                        newMember = NewMemberAction(newMember);
                        if (newMember == null)
                            throw new Exception("NewMemberAction returned null");
                    }
                    memberlist._members.Add(newMember);
                }
                return memberlist;
            }
        }
        #endregion

        public MemberList() { }
    }
}
