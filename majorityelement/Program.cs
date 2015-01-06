using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.IO;
using majorityelement;
using System.Threading;
using System.Threading.Tasks;

namespace majorityelement
{
    class Program
    {
        static public bool DEBUG = false; //prints the randomly generated array
        static public bool MODE = false; //if true user mode, else automated loop
        static public bool runtime = true; //fixes the compiler bug present in Stopwatch method
        static public bool end = false;

        static public Random random = new Random();
        static public Random randOneIn = new Random();
        static public short randomMajority;

        static public int RAND_MAX = 32767;

        static public int ARRAY_SIZE = 1048576;

        static public int loopCount = 0, prevLoopCount;
        static public short[][] A, B;
        static public int threadCount = 0;
        static public int arrays, splitArrayLength;
        static public short[] test1, test2;
        static public bool lockTaken = false;

        public struct ElmData
        {
            public int arrayLength;
            public long alg1Ticks;
            public double alg1Time;
            public ulong alg1Theor;
            public long alg2Ticks;
            public double alg2Time;
            public long alg2Theor;
        }

        public struct ElmAvgData
        {
            public int arrayLength;
            public long alg1TicksAvg;
            public double alg1TimeAvg;
            public ulong alg1Theor;
            public long alg2TicksAvg;
            public double alg2TimeAvg;
            public long alg2Theor;
        }

        static public long ticks1avg = 0, ticks2avg = 0;
        static public double time1avg = 0, time2avg = 0;

        static public ElmData[] elmArray = new ElmData[100];
        static public ElmAvgData[] elmAvg = new ElmAvgData[10];

        static public short[] RandomArray(short i)
        {
            short[] array = new short[i];
            randomMajority = Convert.ToInt16(random.Next(0, RAND_MAX));
            for (long j = 0; j < i; j++)
            {
                bool test;
                if (randOneIn.Next(0, 2) == 1) test = true;
                else test = false;
                if (test == true) array[j] = randomMajority;
                else
                    array[j] = Convert.ToInt16(random.Next(0, RAND_MAX));
            }
            return array;
        }

        static public short[][] RandomArray(int x, int y)
        {
            //x = arraylength / arrays
            //y = arrays
            short[][] array = new short[y][];
            for (int i = 0; i < y; i++)
            {
                array[i] = new short[x];
                
            }
            randomMajority = Convert.ToInt16(random.Next(0, RAND_MAX));
            for (int i = 0; i < y; i++)
            {
                for (int j = 0; j < x; j++)
                {
                    bool test;
                    if (randOneIn.Next(0, 2) == 1) test = true;
                    else test = false;
                    if (test == true) array[i][j] = randomMajority;
                    else
                        array[i][j] = Convert.ToInt16(random.Next(0, RAND_MAX));
                }
            }
            return array;
        }

        static double arrayLength = -1;

        static bool fixarray = true;

        static void compute()
        {
            if (loopCount == 0)
            {
                arrayLength = 100;
            }
            else if (loopCount % 10 == 0 && fixarray)
            {
                fixarray = false;
                arrayLength += 100;
            }
            else if (loopCount == 99) end = true;
            if (prevLoopCount % 10 == 0) fixarray = true;
        }

        static void read()
        {
            Console.WriteLine("How long is the array? \t");
            String s = Console.ReadLine();
            int i = 1;
            int j;

            try
            {
                if (IntPtr.Size == 8)
                {
                    arrayLength = Convert.ToInt64(s);
                }
                else
                {
                    arrayLength = Convert.ToInt32(s);
                }
                j = (int)(i / arrayLength);
            }
            catch (FormatException e)
            {
                Console.WriteLine("Not a number");
                read();
            }
            catch (OverflowException e)
            {
                Console.WriteLine("Number is too long");
                read();
            }
            catch (DivideByZeroException e)
            {
                Console.WriteLine("Number must have positive length");
                read();
            }
            
        }

        private static string fileName = "./output.csv";

        static void averages()
        {
            foreach (ElmData ed in elmArray)
            {
                switch (ed.arrayLength)
                {
                    case 100:
                        elmAvg[0].alg1TicksAvg += ed.alg1Ticks;
                        elmAvg[0].alg1TimeAvg += ed.alg1Time;
                        elmAvg[0].alg1Theor = ed.alg1Theor;
                        elmAvg[0].alg2TicksAvg += ed.alg2Ticks;
                        elmAvg[0].alg2TimeAvg += ed.alg2Time;
                        elmAvg[0].alg2Theor = ed.alg2Theor;
                        elmAvg[0].arrayLength = ed.arrayLength;
                        break;
                    case 200:
                        elmAvg[1].alg1TicksAvg += ed.alg1Ticks;
                        elmAvg[1].alg1TimeAvg += ed.alg1Time;
                        elmAvg[1].alg1Theor = ed.alg1Theor;
                        elmAvg[1].alg2TicksAvg += ed.alg2Ticks;
                        elmAvg[1].alg2TimeAvg += ed.alg2Time;
                        elmAvg[1].alg2Theor = ed.alg2Theor;
                        elmAvg[1].arrayLength = ed.arrayLength;
                        break;
                    case 300:
                        elmAvg[2].alg1TicksAvg += ed.alg1Ticks;
                        elmAvg[2].alg1TimeAvg += ed.alg1Time;
                        elmAvg[2].alg1Theor = ed.alg1Theor;
                        elmAvg[2].alg2TicksAvg += ed.alg2Ticks;
                        elmAvg[2].alg2TimeAvg += ed.alg2Time;
                        elmAvg[2].alg2Theor = ed.alg2Theor;
                        elmAvg[2].arrayLength = ed.arrayLength;
                        break;
                    case 400:
                        elmAvg[3].alg1TicksAvg += ed.alg1Ticks;
                        elmAvg[3].alg1TimeAvg += ed.alg1Time;
                        elmAvg[3].alg1Theor = ed.alg1Theor;
                        elmAvg[3].alg2TicksAvg += ed.alg2Ticks;
                        elmAvg[3].alg2TimeAvg += ed.alg2Time;
                        elmAvg[3].alg2Theor = ed.alg2Theor;
                        elmAvg[3].arrayLength = ed.arrayLength;
                        break;
                    case 500:
                        elmAvg[4].alg1TicksAvg += ed.alg1Ticks;
                        elmAvg[4].alg1TimeAvg += ed.alg1Time;
                        elmAvg[4].alg1Theor = ed.alg1Theor;
                        elmAvg[4].alg2TicksAvg += ed.alg2Ticks;
                        elmAvg[4].alg2TimeAvg += ed.alg2Time;
                        elmAvg[4].alg2Theor = ed.alg2Theor;
                        elmAvg[4].arrayLength = ed.arrayLength;
                        break;
                    case 600:
                        elmAvg[5].alg1TicksAvg += ed.alg1Ticks;
                        elmAvg[5].alg1TimeAvg += ed.alg1Time;
                        elmAvg[5].alg1Theor = ed.alg1Theor;
                        elmAvg[5].alg2TicksAvg += ed.alg2Ticks;
                        elmAvg[5].alg2TimeAvg += ed.alg2Time;
                        elmAvg[5].alg2Theor = ed.alg2Theor;
                        elmAvg[5].arrayLength = ed.arrayLength;
                        break;
                    case 700:
                        elmAvg[6].alg1TicksAvg += ed.alg1Ticks;
                        elmAvg[6].alg1TimeAvg += ed.alg1Time;
                        elmAvg[6].alg1Theor = ed.alg1Theor;
                        elmAvg[6].alg2TicksAvg += ed.alg2Ticks;
                        elmAvg[6].alg2TimeAvg += ed.alg2Time;
                        elmAvg[6].alg2Theor = ed.alg2Theor;
                        elmAvg[6].arrayLength = ed.arrayLength;
                        break;
                    case 800:
                        elmAvg[7].alg1TicksAvg += ed.alg1Ticks;
                        elmAvg[7].alg1TimeAvg += ed.alg1Time;
                        elmAvg[7].alg1Theor = ed.alg1Theor;
                        elmAvg[7].alg2TicksAvg += ed.alg2Ticks;
                        elmAvg[7].alg2TimeAvg += ed.alg2Time;
                        elmAvg[7].alg2Theor = ed.alg2Theor;
                        elmAvg[7].arrayLength = ed.arrayLength;
                        break;
                    case 900:
                        elmAvg[8].alg1TicksAvg += ed.alg1Ticks;
                        elmAvg[8].alg1TimeAvg += ed.alg1Time;
                        elmAvg[8].alg1Theor = ed.alg1Theor;
                        elmAvg[8].alg2TicksAvg += ed.alg2Ticks;
                        elmAvg[8].alg2TimeAvg += ed.alg2Time;
                        elmAvg[8].alg2Theor = ed.alg2Theor;
                        elmAvg[8].arrayLength = ed.arrayLength;
                        break;
                    case 1000:
                        elmAvg[9].alg1TicksAvg += ed.alg1Ticks;
                        elmAvg[9].alg1TimeAvg += ed.alg1Time;
                        elmAvg[9].alg1Theor = ed.alg1Theor;
                        elmAvg[9].alg2TicksAvg += ed.alg2Ticks;
                        elmAvg[9].alg2TimeAvg += ed.alg2Time;
                        elmAvg[9].alg2Theor = ed.alg2Theor;
                        elmAvg[9].arrayLength = ed.arrayLength;
                        break;
                }

            }
            for (int i = 0; i < 10; i++)
            {
                elmAvg[i].alg1TicksAvg = elmAvg[i].alg1TicksAvg / 10;
                elmAvg[i].alg2TicksAvg = elmAvg[i].alg2TicksAvg / 10;
                elmAvg[i].alg1TimeAvg = elmAvg[i].alg1TimeAvg / 10;
                elmAvg[i].alg2TimeAvg = elmAvg[i].alg2TimeAvg / 10;

            }
        }

        static void printAndEnd()
        {
            averages();
            Console.Clear();
            
            foreach (ElmAvgData ea in elmAvg)
            {
                Console.WriteLine();
                Console.Write("Array Length is:\t");
                Console.Write(ea.arrayLength);
                Console.WriteLine();
                Console.Write("Algorithm 1 Avg Run Time (ticks):\t");
                Console.Write(ea.alg1TicksAvg);
                Console.WriteLine();
                Console.Write("Algorithm 1 Avg Run Time (mS):\t");
                Console.Write(ea.alg1TimeAvg);
                Console.WriteLine();
                Console.Write("Algorithm 1 Avg Theoretical Run Time:\t");
                Console.Write(ea.alg1Theor);
                Console.WriteLine();
                Console.WriteLine();
                Console.Write("Algorithm 2 Avg Run Time (ticks):\t");
                Console.Write(ea.alg2TicksAvg);
                Console.WriteLine();
                Console.Write("Algorithm 2 Avg Run Time (mS):\t");
                Console.Write(ea.alg2TimeAvg);
                Console.WriteLine();
                Console.Write("Algorithm 2 Avg Theoretical Run Time:\t");
                Console.Write(ea.alg2Theor);
                Console.WriteLine();
            }

            using (StreamWriter writer = File.CreateText(fileName))
            {
                foreach (ElmAvgData ea in elmAvg)
                {
                    writer.Write("Array Length is:, {0},", ea.arrayLength);
                    writer.Write("Algorithm 1 Avg Run Time (ticks):, {0}, Algorithm 1 Avg Run Time (mS):, {1}, Algorithm 1 Theoretical Run Time:, {2}, \n",
                        ea.alg1TicksAvg, ea.alg1TimeAvg, ea.alg1Theor);
                    writer.Write("Array Length is:, {0},", ea.arrayLength);
                    writer.Write("Algorithm 2 Avg Run Time (ticks):, {0}, Algorithm 2 Avg Run Time (mS):, {1}, Algorithm 2 Theoretical Run Time:, {2}, \n",
                        ea.alg2TicksAvg, ea.alg2TimeAvg, ea.alg2Theor);
                }
            }

        }

        static void Main(string[] args)
        {
            
            while (true)
            {
                if (MODE)
                {
                    DEBUG = false; //prints the randomly generated array
                    while (arrayLength == -1) read();
                }
                else
                {
                    
                    compute();
                    
                }
                
                
                if (arrayLength < ARRAY_SIZE)
                {
                    A = new short[1][];
                    A[0] = new short[(int)arrayLength];
                    A = RandomArray((int)arrayLength, 1);
                    B = new short[1][];
                    B[0] = new short[(int)arrayLength];
                    A[0].CopyTo(B[0], 0);
                    arrays = 1;
                    splitArrayLength = (int)arrayLength;
                }
                else
                {
                    arrays = (int)(Math.Ceiling(arrayLength / ARRAY_SIZE));
                    splitArrayLength = (int)(arrayLength / arrays);
                    A = new short[arrays][];
                    B = new short[arrays][];
                    for (int i = 0; i < arrays; i++)
                    {
                        A[i] = new short[splitArrayLength];
                        B[i] = new short[splitArrayLength];
                    }
                    A = RandomArray(splitArrayLength, arrays);
                    for (int i = 0; i < arrays; i++)
                    {
                        A[i].CopyTo(B[i], 0);
                    }
                }
                if (DEBUG)
                {
                    if (A.Equals(B))
                    {
                        Console.WriteLine("Random Array: ");
                        for (int i = 0; i < A.Length; i++)
                        {
                            Console.Write(A[i]);
                            Console.Write(", ");
                        }
                        Console.WriteLine();
                    }
                    else
                    {
                        for (int j = 0; j < arrays; j++)
                        {
                            for (int i = 0; i < splitArrayLength; i++)
                            {
                                B[j][i] = A[j][i];
                            }
                        }
                        Console.WriteLine("Random Array: ");
                        for (int j = 0; j < arrays; j++)
                        {
                            for (int i = 0; i < splitArrayLength; i++)
                            {
                                Console.Write(A[j][i]);
                                Console.Write(", ");
                            }
                        }
                        Console.WriteLine();
                    }
                }

                Console.Write("Array Length := ");
                Console.Write(arrayLength);
                Console.WriteLine();

                algorithm1 alg1 = new algorithm1();
                algorithm2 alg2 = new algorithm2();

                //Thread[] threads = new Thread[arrays];
                //ThreadStart ts;

                Stopwatch watch = new Stopwatch();
                watch.Start();
                watch.Stop();
                watch.Restart();
                watch.Stop();

                test1 = new short[arrays];
                int ii = 0, ret1, ret2;
                ulong alg1Theor = 0;

                //ts = new ThreadStart(alg1.find_majority_help);
                //threadCount = 0;

                /*for (ii = 0; ii < arrays; ii++)
                {
                    threads[ii] = new Thread(ts);
                }
                watch.Restart();
                for (ii = 0; ii < arrays; ii++ )
                {
                    threads[ii].Start();
                }
                for (ii = 0; ii < arrays; ii++)
                {
                    threads[ii].Join();
                   
                }*/
                watch.Restart();
                for (ii = 0; ii < arrays; ii++ )
                {
                    test1[ii] = alg1.find_majority(A[ii], 0, splitArrayLength);
                }
                ret1 = alg1.find_majority(test1, 0, test1.Length);
                watch.Stop();
                alg1Theor = (ulong)(splitArrayLength * arrays);
                
                double alg1TimeMillis = watch.Elapsed.TotalMilliseconds;
                long alg1Ticks = watch.Elapsed.Ticks;
                alg1Theor = (ulong)alg1.ceil(arrays * splitArrayLength * Math.Log(arrays * splitArrayLength));

                long alg2Theor = 0;
                ii = 0;
                threadCount = 0;
                test2 = new short[arrays];
                /*ts = new ThreadStart(alg2.find_majority_help);
                for (ii = 0; ii < arrays; ii++)
                {
                    threads[ii] = new Thread(ts);
                }
                watch.Restart();
                for (ii = 0; ii < arrays; ii++)
                {
                    threads[ii].Start();
                }
                for (ii = 0; ii < arrays; ii++)
                {
                    threads[ii].Join();
                }*/
                watch.Restart();
                for (ii = 0; ii < arrays; ii++)
                {
                    test2[ii] = alg2.find_majority(B[ii]);
                }
                ret2 = alg2.find_majority(test2);
                //long ret2 = alg2.find_majority(test2);
                watch.Stop();
                double alg2TimeMillis = watch.Elapsed.TotalMilliseconds;
                long alg2Ticks = watch.Elapsed.Ticks;
                alg2Theor = arrays * splitArrayLength;

                if (MODE)
                {
                    if (ret1 >= 0)
                    {
                        Console.WriteLine();
                        Console.Write("Majority Element is:\t");
                        Console.Write(ret1);
                        Console.WriteLine();
                        Console.Write("Algorithm 1 Run Time (ticks):\t");
                        Console.Write(alg1Ticks);
                        Console.WriteLine();
                        Console.Write("Algorithm 1 Run Time (mS):\t");
                        Console.Write(alg1TimeMillis);
                        Console.WriteLine();
                        Console.Write("Algorithm 1 Theoretical Run Time:\t");
                        Console.Write(alg1Theor);
                        Console.WriteLine();
                    }
                    else
                    {
                        Console.WriteLine();
                        Console.Write("There is no majority element:\t");
                        Console.WriteLine();
                        Console.Write("Algorithm 1 Run Time (ticks):\t");
                        Console.Write(alg1Ticks);
                        Console.WriteLine();
                        Console.Write("Algorithm 1 Run Time (mS):\t");
                        Console.Write(alg1TimeMillis);
                        Console.WriteLine();
                        Console.Write("Algorithm 1 Theoretical Run Time:\t");
                        Console.Write(alg1Theor);
                        Console.WriteLine();
                    }
                    
                    if (ret2 >= 0)
                    {
                        Console.WriteLine();
                        Console.Write("Majority Element is:\t");
                        Console.Write(ret2);
                        Console.WriteLine();
                        Console.Write("Algorithm 2 Run Time (ticks):\t");
                        Console.Write(alg2Ticks);
                        Console.WriteLine();
                        Console.Write("Algorithm 2 Run Time (mS):\t");
                        Console.Write(alg2TimeMillis);
                        Console.WriteLine();
                        Console.Write("Algorithm 2 Theoretical Run Time:\t");
                        Console.Write(alg2Theor);
                        Console.WriteLine();
                    }
                    else
                    {
                        Console.WriteLine();
                        Console.Write("There is no majority element:\t");
                        Console.WriteLine();
                        Console.Write("Algorithm 2 Run Time (ticks):\t");
                        Console.Write(alg2Ticks);
                        Console.WriteLine();
                        Console.Write("Algorithm 2 Run Time (mS):\t");
                        Console.Write(alg2TimeMillis);
                        Console.WriteLine();
                        Console.Write("Algorithm 2 Theoretical Run Time:\t");
                        Console.Write(alg2Theor);
                        Console.WriteLine();
                    }
                    

                    if (!runtime)
                    {
                        Console.WriteLine("Run again (y/n)?");
                        ConsoleKeyInfo key = new ConsoleKeyInfo();
                        key = Console.ReadKey(true);
                        Console.Clear();
                        if (key.KeyChar == 'n') break;
                    }
                }
                else
                {
                    if (!runtime)
                    {
                        if ((ret1 >= 0) && (ret2 >= 0))
                        {
                            elmArray[loopCount].arrayLength = splitArrayLength*arrays;
                            elmArray[loopCount].alg1Ticks = alg1Ticks;
                            elmArray[loopCount].alg1Time = alg1TimeMillis;
                            elmArray[loopCount].alg1Theor = alg1Theor;
                            elmArray[loopCount].alg2Ticks = alg2Ticks;
                            elmArray[loopCount].alg2Time = alg2TimeMillis;
                            elmArray[loopCount].alg2Theor = alg2Theor;
                            prevLoopCount = loopCount;
                            loopCount++;
                        }
                        if ((ret1 >= 0) && (ret2 >= 0) && end == true)
                        {
                            printAndEnd();
                            ConsoleKeyInfo key = new ConsoleKeyInfo();
                            key = Console.ReadKey(true);
                            break;
                        }
                    }
                }
                Console.Clear();
                runtime = false;
            }
        }
    }
}
