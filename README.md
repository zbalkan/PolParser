# PolParser
A simple library to parse `Registry.pol` files to investigate group Policy Settings. The idea is to enable cross-platform parsing of POL files.

## Usage
```csharp
	var pol = Parser.ReadPolFile("Registry.pol");
```