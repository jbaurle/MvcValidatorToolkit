using System.Collections.Generic;

namespace MvcValidatorToolkit.SampleSite
{
    public class MembershipData
    {
        public MembershipData(string c, string n)
        {
            Code = c;
            Name = n;
        }

        public string Code
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }
    }

    public class Sample3ViewData
    {
        public List<MembershipData> MembershipList
        {
            get;
            set;
        }

       /* public Sample3ValidationSet Sample3ValidationSet
        {
            get;
            set;
        }*/
    }
}
