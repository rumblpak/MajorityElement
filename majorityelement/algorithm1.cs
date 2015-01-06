using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace majorityelement
{
    class algorithm1
    {
        
        private int NIL = -1;
        private short UNIL = -1;

        int count(short[] A, int p, int r, int val)
        {
            int c = 0;
            for (long i = p; i < r; i++)
            {
                if(A[i] == val) c++;
            }
            return c;
        }

        public int ceil(double d)
        {
            int i;
            i = (int)(Math.Ceiling(d));
            return i;
        }

        public int floor(double d)
        {
            int i;
            i = (int)(Math.Floor(d));
            return i;
        }

        public short find_majority(short[] A, int p, int r)
        {
            int cnt1 = NIL, cnt2 = NIL, q = NIL;
            short x1 = UNIL, x2 = UNIL;
            if ((r-p+1) <= 3) { //base case
                cnt1 = count(A, p, r, A[p]);
                if (cnt1 > ceil((r-p)/2)) return A[p];
                cnt2 = count(A, p, r, A[p + 1]);
                if (cnt2 > ceil((r - p) / 2)) return A[p + 1];
                return UNIL;
            }
            else
            {
                q = floor((p + r) / 2);
                x1 = find_majority(A, p, q);
                x2 = find_majority(A, q + 1, r);
                if (x1 != NIL)
                {
                    cnt1 = count(A, p, r, x1);
                    if (cnt1 > ceil((r - p) / 2)) return x1;
                }
                if (x2 != NIL)
                {
                    cnt2 = count(A, p, r, x2);
                    if (cnt2 > ceil((r - p) / 2)) return x2;
                }
                return UNIL;
            }
            
        }
        /*
        public void find_majority_help()
        {
            

            int id = majorityelement.Program.threadCount;
            Interlocked.Increment(ref majorityelement.Program.threadCount);

            int temp = find_majority(majorityelement.Program.A[id], 0, majorityelement.Program.splitArrayLength);
            Interlocked.Exchange(ref majorityelement.Program.test1[id], temp);
            
        }*/


    }
}
