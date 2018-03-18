using HatchlingCompany.Core.Common.Contracts;
using HatchlingCompany.Core.Common.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace HatchlingCompany.Core.Services.Listing
{
    public class HelpCommand : Command
    {
        private readonly IWriter writer;

        public HelpCommand(IWriter writer)
        {
            this.writer = writer ?? throw new ArgumentNullException(nameof(writer));
        }

        public override void Execute()
        {
            var commandsList = this.GetAllCommandNames().OrderBy(x => x);

            if (commandsList == null)
            {
                throw new ArgumentNullException("No commands created yet");
            }

            this.writer.WriteLine(("The following commands are available:"));
            var counter = 0;

            foreach (var command in commandsList)
            {
                this.writer.WriteLine($"{++counter}. {command.Substring(0, command.Length - "command".Length)}");
            }
        }

        private IEnumerable<string> GetAllCommandNames()
        {
            var assembly = Assembly.GetAssembly(typeof(ICommand));
            var types = assembly.DefinedTypes
                .Where(type => type.ImplementedInterfaces.Any(i => i == typeof(ICommand))).ToList();

            var result = new HashSet<string>();

            foreach (var type in types)
            {
                string commandName = type.Name; //.Substring(0, type.Name.Length - "Command".Length);
                if (commandName != "Command")
                {
                    result.Add(commandName);
                }
            }

            return result;
        }
    }
}
