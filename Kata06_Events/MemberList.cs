using System;
using System.Collections.Generic;

namespace Kata06_Events
{
    class MemberList : IMemberList
    {
        List<IMember> _members = new List<IMember>();

        public IMember this[int idx]
        {
            get { return _members[idx]; }
        }
        public int Count() => _members.Count;
        public int Count(int year)
        {
            int c = 0;
            foreach (var item in _members)
            {
                if (item.Since.Year == year)
                    c++;
            }
            return c;
        }

        //Event handler and On... Method that fires the event
        public EventHandler<int> ListSortedEvent;
        public void OnListSorted(int NrOfItems) => ListSortedEvent?.Invoke(this, NrOfItems);

        //Modified Sort that invokes the ListSorted event
        public void Sort()
        {
            _members.Sort();
            OnListSorted(_members.Count);
        }

        public int NrOfMembers(Func<IMember, bool> match)
        {
            int c = 0;
            foreach (var item in _members)
            {
                if (match(item))
                {
                    c++;
                }
            }
            return c;
        }

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
            public static MemberList CreateRandom(int NrOfItems, Action<IMember> NewMemberAction)
            {
                var memberlist = new MemberList();
                for (int i = 0; i < NrOfItems; i++)
                {
                    var newMember = Member.Factory.CreateRandom();
                    memberlist._members.Add(newMember);

                    NewMemberAction(newMember);
                }
                return memberlist;
            }
        }
        #endregion

        public MemberList() { }
    }
}
