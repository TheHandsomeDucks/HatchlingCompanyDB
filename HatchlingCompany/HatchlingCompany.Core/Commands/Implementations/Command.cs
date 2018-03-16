using HatchlingCompany.Core.Contracts;
using System;
using System.Collections.Generic;

namespace HatchlingCompany.Core.Commands.Implementations
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
            set => this.parameters = value ?? throw new ArgumentNullException("parameters");
        }

        public abstract void Execute();
    }
}
