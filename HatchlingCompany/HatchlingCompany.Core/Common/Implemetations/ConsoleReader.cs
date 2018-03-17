using HatchlingCompany.Core.Common.Contracts;
using System;

namespace HatchlingCompany.Core.Common.Implementations
{
    public class ConsoleReader : IReader
    {
        public string ReadLine() => Console.ReadLine();
    }
}
