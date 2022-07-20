# PolParser
A simple library to parse `Registry.pol` files to investigate group Policy Settings. The idea is to enable cross-platform parsing of POL files.

## Usage
```csharp
    IReadOnlyList<GPRegistryPolicy> policies = Parser.ReadPolFile("Registry.pol");
```

## Sample application
The `Comparer` is a commandline application that accepts a path to a `Registry.pol` file. It is created to demonstrate the use of the PolParser. It is a highly simplified version of `LGPO.exe`. It parses the pol file, compares it with current computer's registry. It checks only HKLM for the sake of simplicity. It is possible  

```bash
USAGE:
    comparer <path> [OPTIONS]

EXAMPLES:
    comparer registry.pol -c

ARGUMENTS:
    <path>    Path of Registry.pol file to parse

OPTIONS:
    -h, --help         Prints help information
    -c, --conflicts    A flag to display only conflicting values
```

You will get a result like this:
![Alt text](assets/comparer.png?raw=true "Comparer output")
