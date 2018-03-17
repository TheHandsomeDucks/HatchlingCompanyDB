namespace HatchlingCompany.Core.Common.Contracts
{
    public interface ICommandProcessor
    {
        void ProcessCommand(string commandLine);
    }
}
