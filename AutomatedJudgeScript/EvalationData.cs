using System.Text;

namespace AutomatedJudgeScript
{
    internal class EvalationData
    {
        enum Result
        {
            Accepted,
            Presentation,
            Wrong
        }

        private readonly int Execution;
        private readonly List<string> Expected;
        private readonly List<string> Received;

        public EvalationData(int number, List<string> expected, List<string> received)
        {
            Execution = number;
            Expected = expected;
            Received = received;
        }

        public string Process()
        {
            /*
             * 1. Check if we have nothing in either result set and mark as Wrong if either set it empty
             * 2. Mark the best possible results based on if the line counts match, this allows for skipping an evaluation
             * 3. Loop over the expected results
             *   a. If Accepted in possible, evaluate the line in both sets to check for match, if not, mark best possible as presentation
             *   b. Regardless track the numeric values in the line
             *   c. If valid, track the numeric values in the corresponding received array
             * 4. If still best possible is accepted, return that
             * 5. Finish looping over the received lines not covered in 3
             * 6. Compare the grand collection of numbers to see if we have them all and in the exact order, return Presentation
             * 7. Return Wrong
             * 
             * Effort is to minimize overall loops with the understanding presentation errors are going to be most prevalent.
             */

            StringBuilder firstNumbers = new StringBuilder();
            StringBuilder secondNumbers = new StringBuilder();

            if (Expected == null || Received == null)
                return GenerateResultMessage(Result.Wrong);

            //defaulting to accepted, and proving false
            var bestResultPossible = Expected.Count == Received.Count ? Result.Accepted : Result.Presentation;

            for (int i = 0; i < Expected.Count; i++)
            {
                if (bestResultPossible == Result.Accepted)
                {
                    if (Expected[i] != Received[i])
                        bestResultPossible = Result.Presentation;
                }

                //loop over line to pull out numbers
                FindNumerics(Expected[i], firstNumbers);
                if (Received.Count > i)
                    FindNumerics(Received[i], secondNumbers);
            }

            //if the expected exactly equals the received, then accepted
            if (bestResultPossible == Result.Accepted)
                return GenerateResultMessage(Result.Accepted);

            //then only looking at the numbers, if those are in the same order, presentation
            //finish looking at the rest of the received lines not covered in the above loop
            for (int i = Expected.Count + 1; i < Received.Count; i++)
            {
                FindNumerics(Received[i], secondNumbers);
            }

            if (firstNumbers.ToString() == secondNumbers.ToString())
                return GenerateResultMessage(Result.Presentation);

            //otherwise, wrong
            return GenerateResultMessage(Result.Wrong);
            
        }

        private static void FindNumerics(string line, StringBuilder numbers)
        {
            foreach (char x in line)
            {
                int xPrime = (int)x;
                if (xPrime >= 48 && xPrime <= 57)
                    numbers.Append(x);
            }
        }

        private string GenerateResultMessage(Result result)
        {
            return result switch
            {
                Result.Accepted => $"Run #{Execution}: Accepted",
                Result.Presentation => $"Run #{Execution}: Presentation Error",
                _ => $"Run #{Execution}: Wrong Answer",
            };
        }
    }
}
