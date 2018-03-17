using HatchlingCompany.Core.Factories.Contracts;
using HatchlingCompany.Core.Providers.Contracts;
using System;
using System.Linq;

namespace HatchlingCompany.Core.Common
{
    public class CommandProcessor : ICommandProcessor
    {
        private readonly IWriter writer;
        private readonly ICommandFactory commandFactory;

        public CommandProcessor(IWriter writer, ICommandFactory commandFactory)
        {
            this.commandFactory = commandFactory;
            this.writer = writer;
        }

        public void ProcessCommand(string commandLine)
        {
            var commandParts = commandLine.Split(new[] { ' ', ',', ' ' }, StringSplitOptions.RemoveEmptyEntries).ToList();
            var delimeter = "----------------------------------------";

            if (!commandParts.Any())
            {
                this.writer.WriteLine("Please add a valid command!");
                return;
            }

            try
            {
                var commandName = commandParts[0];
                var command = this.commandFactory.CreateCommand(commandName);

                if (command == null)
                {
                    this.writer.WriteLine("Invalid command! To read about all commmands, write listCommands and press enter");
                }
                else
                {
                    command.Parameters = commandParts;
                    command.Execute();
                }
            }
            catch (Exception ex)
            {
                this.writer.WriteLine(ex.Message);
            }
            finally
            {
                this.writer.WriteLine(delimeter);
            }
        }
    }
}
