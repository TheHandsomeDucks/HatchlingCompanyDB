using HatchlingCompany.Core.Common.Contracts;
using System;

namespace HatchlingCompany.Core
{
    public class Engine : IEngine
    {
        private readonly ICommandParser commandParser;
        private readonly IReader reader;
        private readonly IWriter writer;

        public Engine(ICommandParser commandParser, IReader reader, IWriter writer)
        {
            this.commandParser = commandParser ?? throw new ArgumentNullException("commandParser");
            this.reader = reader ?? throw new ArgumentNullException(nameof(reader));
            this.writer = writer ?? throw new ArgumentNullException(nameof(writer));
        }

        public void Start()
        {
            this.writer.WriteLine("System running...");

            while (true)
            {
                var commandLine = this.reader.ReadLine();

                if (String.IsNullOrEmpty(commandLine) || String.IsNullOrWhiteSpace(commandLine))
                {
                    throw new ArgumentNullException("Command cannot be null, empty or whitespace");
                }

                if (commandLine.Trim().ToLower() == "end")
                {
                    this.writer.WriteLine("Ciao!");
                    break;
                }

                this.commandParser.ParseCommand(commandLine);

                this.writer.WriteLine("Waiting for command...");
            }
        }
    }
}
