using System.Collections.Generic;

namespace HatchlingCompany.Core.Common.Contracts
{
    public interface ICommand
    {
        string Execute(IList<string> parameters);
    }
}
