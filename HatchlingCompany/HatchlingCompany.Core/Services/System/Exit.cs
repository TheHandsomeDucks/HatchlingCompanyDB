using System;
using System.Collections.Generic;
using HatchlingCompany.Core.Common.Contracts;

namespace HatchlingCompany.Core.Services.System
{
    public class Exit : ICommand
    {
        private IWriter writer;

        public Exit(IWriter writer)
        {
            this.writer = writer;
        }

        public void Execute(IList<string> parameters)
        {
            this.writer.WriteLine("Goodbye!");
            Environment.Exit(0);
        }
    }
}
