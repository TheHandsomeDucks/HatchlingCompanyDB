using System.Collections.Generic;

namespace HatchlingCompany.Core.Common.Contracts
{
    public interface ICommand
    {
        void Execute();

        IList<string> Parameters { get; set; }
    }
}
