using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Recurly;

namespace RecurlyCleanUpUtility.Entities
{
    public class User
    {
        public string FirstName;
        public string LastName;
        public string Adress;
        public string City;
        public string State;
        public string Zip;
        public string Mobile;
        public string BirthDate;
        public string LastDigitsOFSocial;

        public static bool IsEqual(User self, User to, params string[] ignore)
        {
            if (self != null && to != null)
            {
                Type type = typeof(User);
                List<string> ignoreList = new List<string>(ignore);
                foreach (System.Reflection.FieldInfo fi in type.GetFields(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance))
                {
                    if (!ignoreList.Contains(fi.Name))
                    {
                        object selfValue = type.GetField(fi.Name).GetValue(self);
                        object toValue = type.GetField(fi.Name).GetValue(to);

                        if (selfValue != toValue && (selfValue == null || !selfValue.Equals(toValue)))
                        {
                            return false;
                        }
                    }
                }
                return true;
            }
            return self == to;
        }
    }
}
