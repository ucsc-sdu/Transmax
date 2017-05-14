using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Transmax_Programming_Test
{
    public class Record : IComparer
    {
        public string firstname;
        public string lastname;
        public int grade;

        public Record(): this("", "", 0)
        {

        }

        public Record(string firstname, string lastname, int grade)
        {
            this.firstname = firstname;
            this.lastname = lastname;
            this.grade = grade;
        }

        int IComparer.Compare(object x, object y)
        {
            Record a = (Record)x;
            Record b = (Record)y;

            if (a.grade > b.grade)
            {
                return -1;
            }
            else if (a.grade < b.grade)
            {
                return 1;
            }
            else
            {
                int lastnameCompare = (new CaseInsensitiveComparer()).Compare(a.lastname, b.lastname);

                if ((lastnameCompare < 0) || (lastnameCompare > 0))
                {
                    return lastnameCompare;
                }
                else
                {
                    return (new CaseInsensitiveComparer()).Compare(a.firstname, b.firstname);
                }
            }
        }
    }
}
