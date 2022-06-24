using CommonPermutation;

foreach (var pair in StaticUtilities.LoadContentFromFile("./InputFile.txt"))
    Console.WriteLine(pair.Evaluate());


/*
Problem: 
 Given two strings a and b, we have to find the largest string x such that there is a permutation of x that is a (not necessarily continuous) 
 subsequence of a and another permutation of x (likely distinct) which is a subsequence of b. 
 If there is more than one such string x, output the lexicographically smallest one.


The last sentence caused confusion because I am not sure how you would end up with more than one solution.
I went looking for some clarification on the intention with the problem and found
https://algorithmist.com/wiki/UVa_10252_-_Common_Permutation with the following explanation:

This problem is actually very easy to code, after you have a little bit of insight. 
Rather than considering the input strings as strings, consider them a multiset of characters. 
Let y be any string, and let Y be the multiset of characters produced from y. 
Suppose that Z is another multiset of characters. 
When can Z be ordered to get a subsequence of y? 
If some character occurs more times in Z than it does in Y, this is clearly impossible. 
In all other cases we can easily form the subsequence – take y and leave out some characters until all counts match.

Thus a string z can be permuted to obtain a subsequence of y if and only if Z is a subset (actually, a submultiset) of Y.

Now consider both strings a and b as multisets A and B. 
Let c be the string we seek, and C the corresponding multiset. 
We saw that C has to be a subset of both A and B. 
And as we want c to be as long as possible, C has to be the intersection of A and B. 
In other words, if a character occurs x times in a and y times in b, we may use it min(x,y) times in c.

To get the lexicographically smallest c, simply output C in sorted order.


Provided examples (words)       answer
pretty                          e
women                   

walking                         nw
down

the                             et
street

                                <blank>
thelineabovethisoneisblank

elementary                      aeeert
weatherreport

*/