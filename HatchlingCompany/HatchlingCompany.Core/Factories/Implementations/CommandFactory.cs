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

        public ICommand CreateCommand(string name) => this.container.ResolveNamed<ICommand>(name);
    }
}
