Application Settings:

static public bool DEBUG = false; //prints the randomly generated array
static public bool MODE = false; //if true user mode, else automated loop


Description of Problem: 

Majority Element Problem – An array is determined to have a majority element if more than half of its entries are the same. The elements of the prior mentioned array are not necessarily sorted so no comparison array A[i] > A[j] can be made. We can, however, answer A[i] = A[j] in constant time. The goal is to answer whether an array has a majority element, and if so return that element. 


Algorithms:

Divide-and-Conquer: RT – O(nlogn)
This algorithm works by splitting the array into two smaller arrays recursively to determine the majority of the smaller arrays. If an element is the majority of the smaller array check that element to be a majority of the larger array. This algorithm will be titled Algorithm 1 by the application.

Majority Element – Rengakrishnan Subramanian: RT – O(n)
This algorithm works by keeping a running tally of the element with the largest majority. At each element, if the previous element is equal to the current element a counter is increased by 1, otherwise it is decreased by one. If the counter is equal to zero, the current element is set to the majority element and the counter is set to one. This algorithm will be titled Algorithm 2 by the application.


Application Description:

The application runs in two modes, a user mode or an automated mode. In the user mode, a user will enter the length of the array to generate randoms. In the automated mode, the array will be created in 100 to 1000 elements in increments of 100. In the user mode, the program will output the random array, the majority element, the run times in ticks and milliseconds, and the theoretical big O maximum in tick units. A tick is a method of measurement used by the Stopwatch method in C# to determine the step size for each step of frequency set by the Stopwatch. In this case a tick will occur with every 0.0001 mS. In addition, the automated mode will output a comma separated values (CSV) which can be opened in excel for easy plot generation. 
The code is broken into three areas, the main class called program which handles generating the random numbers, setting the application mode, timing the algorithms, and outputting the data. The main class is shown in Appendix A. The algorithm1 class handles the divide-and-conquer algorithm shown above. The code for the algorithm 1 class is shown in Appendix B. The algorithm2 class handles the Rengakrishnan Subramanian method shown above. The code for the algorithm 2 class is shown in Appendix C. Shown in Figure 1 is the output of the program in user mode with a majority element with an array length of n=50. Shown in Figure 2 is the output of the program in user mode with no majority element with an array length of n=50. The theoretical run time shown by the program is just the value of n*log n with ceiling placed on it for algorithm 1 and n for algorithm 2. 

