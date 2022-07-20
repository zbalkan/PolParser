// This file is used by Code Analysis to maintain SuppressMessage
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given
// a specific target and scoped to a namespace, type, member, etc.

using System.Diagnostics.CodeAnalysis;

[assembly: SuppressMessage("Style", "IDE0007:Use implicit type", Justification = "We need to differentiate 32 bit and 64 bit values.", Scope = "member", Target = "~M:PolParser.Parser.ByteArrayToInt(System.Byte[])~System.Int32")]
[assembly: SuppressMessage("Design", "CA1069:Enums values should not be duplicated", Justification = "Those values are the same by default according to the docs.", Scope = "type", Target = "~T:PolParser.RegType")]
[assembly: SuppressMessage("Roslynator", "RCS1234:Duplicate enum value.", Justification = "Those values are the same by default according to the docs.", Scope = "type", Target = "~T:PolParser.RegType")]
