﻿using HatchlingCompany.Core.Contracts;

namespace HatchlingCompany.Core.Factories.Contracts
{
    public interface ICommandFactory
    {
        ICommand CreateCommand(string name);
    }
}
