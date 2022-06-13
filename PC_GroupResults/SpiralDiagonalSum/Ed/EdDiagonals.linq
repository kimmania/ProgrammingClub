<Query Kind="Statements" />

int n = 500;
double sum = 0;
  Stopwatch stopWatch = new Stopwatch();
  stopWatch.Start();
while (n>0){
//n is the number of rings, the center does not count as a ring. just add one when we are done.
	//2n+1 squared is the top right
	//top left is same minus 2n
	//bottom left is the same as above minus another 2n
	//bottom right is is the same as above minus another 2n
 sum += (4*((2*n+1) * (2*n+1) )) - 12*n;
 n--;
}
sum++;
 stopWatch.Stop();
sum.Dump();
stopWatch.ElapsedTicks.Dump();