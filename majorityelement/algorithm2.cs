using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace majorityelement
{
    class algorithm2
    {
        public short find_majority(short[] A)
        {
            int i = 0, j = A.Length, count = 0;
            short elm = find_element(A);

            for (long x = i; x < j; x++)
            {
                if (A[x] == elm)
                {
                    count++;
                }
            }

            if (count > (A.Length / 2))
                return elm;
            else return -1;
        }

        private short find_element(short[] A)
        {
            int m = 0;
            short ele = 0;
            for (int x = 0; x < A.Length; x++)
            {
                if (m == 0)
                {
                    m = 1;
                    ele = A[x];
                }
                else if (A[x] == ele)
                {
                    m++;
                }
                else m--;
            }
            return ele;
        }
    }
}
