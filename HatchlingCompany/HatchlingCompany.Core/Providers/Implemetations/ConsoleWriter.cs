using HatchlingCompany.Core.Providers.Contracts;
using System;

namespace HatchlingCompany.Core.Providers.Implemetations
{
    public class ConsoleWriter : IWriter
    {
        public void Write(object value) => Console.Write(value);

        public void WriteLine(object value) => Console.WriteLine(value.ToString().TrimEnd());
    }
}
