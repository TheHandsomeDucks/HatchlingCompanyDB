using HatchlingCompany.Core.Providers.Contracts;
using System;

namespace HatchlingCompany.Core.Providers.Implemetations
{
    public class ConsoleReader : IReader
    {
        public string ReadLine() => Console.ReadLine();
    }
}
