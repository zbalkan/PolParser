using System;
using System.Collections.Generic;
using System.IO;

namespace PolParser
{
    /// <summary>
    ///     Parses POL file and returns as a list of <see cref="GPRegistryPolicy"/> instances.
    /// </summary>
    /// <remarks>
    ///     Based on <see href="https://github.com/PowerShell/GPRegistryPolicyParser"/>.
    /// </remarks>
    public static class Parser
    {
        public static IReadOnlyList<GPRegistryPolicy> ReadPolFile(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                throw new ArgumentException($"'{nameof(path)}' cannot be null or empty.", nameof(path));
            }

            var content = File.ReadAllText(path).Replace("\0", "");
            ValidateSignature(content);
            ValidateVersion(content);

            var policies = new List<GPRegistryPolicy>(20);
            var settingsRead = content[6..].Replace('[', '\n').Replace(']', '\n').Split('\n', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            foreach (var setting in settingsRead)
            {
                var parts = setting.Split(';');

                policies.Add(new GPRegistryPolicy(GetKeyName(parts),
                                                  GetValueName(parts),
                                                  GetValueType(parts),
                                                  GetValueLength(parts),
                                                  (string?)GetValueData(parts, GetValueType(parts))));
            }

            return policies.AsReadOnly();
        }

        private static string GetKeyName(string[] parts) => parts[0] ?? string.Empty;

        private static string GetValueName(string[] parts) => parts[1] ?? string.Empty;

        private static RegType GetValueType(string[] parts) => parts[2]?.Length != 0 ? (RegType)(int)char.Parse(parts[2]) : RegType.REG_NONE;

        private static string GetValueData(string[] elements, RegType vType)
        {
            if (vType == RegType.REG_DWORD || vType == RegType.REG_DWORD_BIG_ENDIAN || vType == RegType.REG_DWORD_LITTLE_ENDIAN || vType == RegType.REG_QWORD || vType == RegType.REG_QWORD_LITTLE_ENDIAN)
            {
                var bytes = System.Text.Encoding.ASCII.GetBytes(elements[4]);
                if(bytes.Length == 0)
                {
                    return default(int).ToString();
                }
                var val = ByteArrayToInt(bytes);
                return val.ToString();
            }
            else
            {
                return elements[4];
            }
        }

        private static long GetValueLength(string[] elements)
        {
            if (elements[3].Length == 0)
            {
                return 0;
            }
            else if (elements[3].Length == 1)
            {
                return (int)elements[3][0];
            }
            else
            {
                return ByteArrayToInt(System.Text.Encoding.ASCII.GetBytes(elements[3]));
            }
        }

        private static void ValidateVersion(string content)
        {
            var version = (int)content[4];
            if (version != 1)
            {
                throw new Exception("Invalid Version.");
            }
        }

        private static void ValidateSignature(string content)
        {
            var signature = content[..4];
            if (signature != "PReg")
            {
                throw new Exception("Invalid header.");
            }
        }

        private static long ByteArrayToInt(byte[] input)
        {
            if (input.Length > 0 && input.Length <= 8)
            {
                if (input.Length <= 4)
                {
                    var result32bit = 0;
                    for (var i = input.Length - 1; i >= 0; --i)
                    {
                        result32bit <<= 8;
                        result32bit += input[i];
                    }
                    return Convert.ToInt64(result32bit);
                }
                else
                {
                    long result64bit = 0;
                    for (var i = input.Length - 1; i >= 0; --i)
                    {
                        result64bit <<= 8;
                        result64bit += input[i];
                    }
                    return result64bit;
                }
            }
            throw new Exception("Invalid value size.");
        }
    }
}
