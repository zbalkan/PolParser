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
    internal static class Parser
    {
        public static IReadOnlyList<GPRegistryPolicy> ReadPolFile(string path)
        {
            var policies = new List<GPRegistryPolicy>();
            var content = File.ReadAllText(path).Replace("\0", "");
            ValidateSignature(content);
            ValidateVersion(content);

            var settingsRead = content[6..].Replace("][", "\n").Split('\n', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
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

        private static string GetKeyName(string[] parts)
        {
            return parts[0] ?? string.Empty;
        }

        private static string GetValueName(string[] parts)
        {
            return parts[1] ?? string.Empty;
        }

        private static RegType GetValueType(string[] parts)
        {
            return parts[2] != "" ? (RegType)(int)char.Parse(parts[2]) : RegType.REG_NONE;
        }

        private static string GetValueData(string[] elements, RegType vType)
        {
            string vData;

            if (vType == RegType.REG_DWORD || vType == RegType.REG_DWORD_BIG_ENDIAN || vType == RegType.REG_DWORD_LITTLE_ENDIAN || vType == RegType.REG_QWORD || vType == RegType.REG_QWORD_LITTLE_ENDIAN)
            {
                var bytes = System.Text.Encoding.ASCII.GetBytes(elements[4]);
                var val = ByteArrayToInt(bytes);
                vData = val.ToString();
            }
            else
            {
                vData = elements[4];
            }

            return vData;
        }

        private static int GetValueLength(string[] elements)
        {
            int vLength;
            if (elements[3].Length == 0)
            {
                vLength = 0;
            }
            else if (elements[3].Length == 1)
            {
                vLength = (int)elements[3][0];
            }
            else
            {
                vLength = ByteArrayToInt(System.Text.Encoding.ASCII.GetBytes(elements[3]));
            }

            return vLength;
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

        private static int ByteArrayToInt(byte[] input)
        {
            Int32 result32 = 0;
            Int64 result64 = 0;

            if (input.Length > 8)
            {
                throw new Exception("Invalid value size.");
            }
            if (input.Length <= 4)
            {
                for (var i = input.Length - 1; i >= 0; i -= 1)
                {
                    result32 <<= 8;
                    result32 += input[i];
                }
                return result32;
            }
            else if (input.Length <= 4)
            {
                for (var i = input.Length - 1; i >= 0; i -= 1)
                {
                    result64 <<= 8;
                    result64 += input[i];
                }
                return (int)result64;
            }
            else
            {
                throw new Exception("Invalid value size.");
            }
        }
    }
}
