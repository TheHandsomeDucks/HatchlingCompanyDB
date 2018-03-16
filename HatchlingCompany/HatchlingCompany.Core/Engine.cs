using HatchlingCompany.Core.Common;
using HatchlingCompany.Core.Providers.Contracts;
using System;

namespace HatchlingCompany.Core
{
    public class Engine : IEngine
    {
        private readonly ICommandProcessor commandProcessor;
        private readonly IReader reader;
        private readonly IWriter writer;

        public Engine(ICommandProcessor commandProcessor, IReader reader, IWriter writer)
        {
            this.commandProcessor = commandProcessor ?? throw new ArgumentNullException("commandProcessor");
            this.reader = reader ?? throw new ArgumentNullException("reader");
            this.writer = writer ?? throw new ArgumentNullException("writer");
        }

        public void Start()
        {
            this.writer.WriteLine("System running...");

            while (true)
            {
                var commandLine = this.reader.ReadLine();

                if (commandLine.Trim().ToLower() == "end")
                {
                    this.writer.WriteLine("Ciao!");
                    break;
                }

                this.commandProcessor.ProcessCommand(commandLine);

                this.writer.WriteLine("Waiting for command...");
            }
        }
    }
}
