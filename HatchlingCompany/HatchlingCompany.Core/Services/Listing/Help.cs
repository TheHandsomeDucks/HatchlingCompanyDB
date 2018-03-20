using HatchlingCompany.Core.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace HatchlingCompany.Core.Services.Listing
{
    public class Help : ICommand
    {
        private readonly IWriter writer;

        public Help(IWriter writer)
        {
            this.writer = writer ?? throw new ArgumentNullException(nameof(writer));
        }

        public string Execute(IList<string> parameters)
        {
            if (parameters == null)
            {
                throw new ArgumentNullException(nameof(parameters));
            }

            var commandsList = this.GetAllCommandNames().OrderBy(x => x);

            if (commandsList == null)
            {
                throw new ArgumentNullException("No commands created yet");
            }

            this.writer.WriteLine("The following commands are available:");
            var counter = 0;

            foreach (var command in commandsList)
            {
                this.writer.WriteLine($"{++counter}. {command}");
            }

            return $"Listing available commands...";
        }

        private IEnumerable<string> GetAllCommandNames()
        {
            var assembly = Assembly.GetAssembly(typeof(ICommand));
            var types = assembly.DefinedTypes
                .Where(type => type.ImplementedInterfaces.Any(i => i == typeof(ICommand))).ToList();

            var result = new HashSet<string>();

            foreach (var type in types)
            {
                string commandName = type.Name;
                if (commandName != "Command")
                {
                    result.Add(commandName);
                }
            }

            return result;
        }
    }
}
