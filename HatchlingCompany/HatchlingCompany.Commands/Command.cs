using HatchlingCompany.Core.Common.Contracts;
using System;
using System.Collections.Generic;

namespace HatchlingCompany.Console.Commands
{
    public abstract class Command : ICommand
    {
        private IList<string> parameters;

        public Command()
        {
            this.Parameters = new List<string>();
        }

        public IList<string> Parameters
        {
            get => this.parameters;
            set => this.parameters = value ?? throw new ArgumentNullException(nameof(parameters));
        }

        public abstract void Execute();
    }
}
