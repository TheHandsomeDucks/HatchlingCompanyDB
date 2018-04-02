using Autofac;
using HatchlingCompany.Core.Common.Contracts;
using HatchlingCompany.Core.Factories.Contracts;
using System;

namespace HatchlingCompany.Core.Factories.Implementations
{
    public class CommandFactory : ICommandFactory
    {
        private readonly IComponentContext container;

        public CommandFactory(IComponentContext container)
        {
            this.container = container ?? throw new ArgumentNullException(nameof(container));
        }

        public ICommand CreateCommand(string name)
        {
            if (String.IsNullOrEmpty(name) || String.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException("Command name cannot be null, empty or whitespace");
            }

            return this.container.ResolveNamed<ICommand>(name);
        }
    }
}
