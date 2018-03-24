using HatchlingCompany.Core.Common.Contracts;
using HatchlingCompany.Core.Factories.Contracts;
using System;
using System.Linq;

namespace HatchlingCompany.Core.Common.Implementations
{
    public class CommandParser : ICommandParser
    {
        private readonly IWriter writer;
        private readonly ICommandFactory commandFactory;

        public CommandParser(ICommandFactory commandFactory, IWriter writer)
        {
            this.commandFactory = commandFactory;
            this.writer = writer;
        }

        public void ParseCommand(string commandLine)
        {
            var commandParts = commandLine.Split(new[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries).ToList();
            var delimeter = "----------------------------------------";

            try
            {
                if (!commandParts.Any())
                {
                    throw new ArgumentException("Please write a valid command with parameters!");
                }

                var commandName = commandParts[0].ToLower();
                var command = this.commandFactory.CreateCommand(commandName);

                if (command == null)
                {
                    throw new ArgumentException("Invalid command! Type in help to get all commands");
                }
                else
                {
                    command.Execute(commandParts);
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
