using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AussieVoting.Objects
{
    public interface IExecute
    {

        string ResultsFileName { get; set; }
        ExecuteResults Execute(string inputFileName);

    }  // end interface
}  // end namespace
