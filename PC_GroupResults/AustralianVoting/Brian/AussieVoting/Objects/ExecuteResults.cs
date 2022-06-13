using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AussieVoting.Objects
{
    public class ExecuteResults
    {
        public long ExecutionTimeMilliseconds { get; set; }
        public long ExecutionTimeTicks { get; set; }
        public string ErrorMessage { get; set; }

        public ExecuteResults()
        {
            ErrorMessage = "";
        }
    }  // end class
}  // end namespace
