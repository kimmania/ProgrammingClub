<Query Kind="Statements" />

/*
111 112 113 114 115 116 117 118 119 120 121
110  73  74  75  76  77  78  79  80  81  82
109  72  43  44  45  46  47  48  49  50  83 
108  71  42  21  22  23  24  25  26  51  84
107  70  41  20   7   8   9  10  27  52  85
106  69  40  19   6   1   2  11  28  53  86
105  68  39  18   5   4   3  12  29  54  87
104  67  38  17  16  15  14  13  30  55  88
103  66  37  36  35  34  33  32  31  56  89
102  65  64  63  62  61  60  59  58  57  90 
101 100  99  98  97  96  95  94  93  92  91

essentially, I am looking at four points in a square for each level in the diaganol
if we average the four points in a specific loop and multiply by 4 to get the total
for that level in the spiral.  The average is easy and happens to be the value in 
the row starting with 1 and moving out to the left (1, 6, 19, 40, 69, 106).
Now to figure out what these numbers are...
With each round (except for the very first value where we haven't had the opportunity 
to add a full iteration, we add 8 digits to the previous loop's value.  We then use this
value to multiply by 4 to get the four value totaled and add to our running total.
*/


Stopwatch numberOfTicks = Stopwatch.StartNew();
int size = 1001;  // size of the spiral
int increment = 5; // starting out with 5, all future will be increasing by 8
int positionValue = 6; //first value to left of 1
int total = 25;//1 + (4 * 6);
int runs = size / 2; // we add two numbers in with each spiral size, so we only need to iterate half the size
for (int i = 1; i < runs; i++)
{
	increment += 8; //with each iteration, we are adding 8 values to a spiral level
	positionValue += increment; //add the previous position with the increment to get the new value (19, 40, 69, 106)
	total += (4 * positionValue);  //add 4 * the position to get the total for the spiral
}

numberOfTicks.Stop();
numberOfTicks.ElapsedTicks.Dump("Ticks");
total.Dump("Total");
//expected: 669171001