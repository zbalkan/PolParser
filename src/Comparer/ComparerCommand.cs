using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using Microsoft.Win32;
using PolParser;
using Spectre.Console;
using Spectre.Console.Cli;

namespace Comparer
{
    public class ComparerCommand : Command<ComparerCommand.Settings>
    {
        public class Settings : CommandSettings
        {
            [Description("Path of Registry.pol file to parse.")]
            [CommandArgument(0, "<path>")]
            public string Path { get; set; }

            [Description("A flag to display only conflicting values")]
            [CommandOption("-c|--conflicts")]
            public bool ConflictsOnly { get; set; }
        }

        public override int Execute(CommandContext context, Settings settings)
        {
            try
            {
                var policies = Parser.ReadPolFile(settings.Path).GroupBy(p => p.KeyName);

                AnsiConsole.MarkupLine("[bold underline yellow]Configuration Comparison[/]");

                var tree = new Tree("Registry");

                foreach (var group in policies)
                {
                    var keyName = group.Key;
                    if (string.IsNullOrEmpty(keyName)) throw new Exception("Invalid registry key.");

                    var innerTable = new Table
                    {
                        Alignment = Justify.Left,
                        Border = TableBorder.HeavyEdge,
                        Expand = true,
                        UseSafeBorder = true
                    };

                    innerTable.AddColumn("Registry Value");
                    innerTable.AddColumn("Registry.Pol Value Data (Target)");
                    innerTable.AddColumn("Registry Value Data (Current)");

                    foreach (var policy in group)
                    {
                        var regValue = Registry.LocalMachine?.OpenSubKey(policy.KeyName)?.GetValue(policy.ValueName)?.ToString();
                        var fullPath = string.Join('\\', policy.KeyName, policy.ValueName);

                        if (regValue == policy.ValueData && !settings.ConflictsOnly)
                        {
                            innerTable.AddRow($"{fullPath}\n", $"[green]{policy.ValueData ?? string.Empty}[/]", $"[green]{regValue ?? string.Empty}[/]");
                        }
                        else
                        {
                            innerTable.AddRow($"{fullPath}\n", $"[red]{policy.ValueData ?? string.Empty}[/]", $"[red]{regValue ?? string.Empty}[/]");
                        }
                    }
                    tree.AddNode(keyName).AddNode(innerTable);
                }
                AnsiConsole.Write(tree);
            }
            catch (Exception ex)
            {
                AnsiConsole.WriteException(ex, ExceptionFormats.ShortenEverything);
                return 1;
            }
            return 0;
        }

        public override ValidationResult Validate(CommandContext context, Settings settings) => File.Exists(settings.Path) && Path.HasExtension(".pol")
                ? ValidationResult.Success()
                : ValidationResult.Error("Names must be at least two characters long");
    }
}
