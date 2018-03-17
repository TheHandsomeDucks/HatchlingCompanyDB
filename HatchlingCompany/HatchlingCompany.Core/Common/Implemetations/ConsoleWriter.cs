using HatchlingCompany.Core.Common.Contracts;
using System;

namespace HatchlingCompany.Core.Common.Implementations
{
    public class ConsoleWriter : IWriter
    {
        public void Write(object value) => Console.Write(value);

        public void WriteLine(object value) => Console.WriteLine(value.ToString().TrimEnd());
    }
}
