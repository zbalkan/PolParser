namespace PolParser
{
    internal enum RegType
    {
        REG_NONE = 0,   // No value type
        REG_SZ = 1, // Unicode null terminated string
        REG_EXPAND_SZ = 2,  // Unicode null terminated string (with environmental variable references)
        REG_BINARY = 3, // Free form binary
        REG_DWORD = 4,  // 32-bit number
        REG_DWORD_LITTLE_ENDIAN = 4,    // 32-bit number (same as REG_DWORD)
        REG_DWORD_BIG_ENDIAN = 5,   // 32-bit number
        REG_LINK = 6,   // Symbolic link (Unicode)
        REG_MULTI_SZ = 7,   // Multiple Unicode strings, delimited by \0, terminated by \0\0
        REG_RESOURCE_LIST = 8,  // Resource list in resource map
        REG_FULL_RESOURCE_DESCRIPTOR = 9,  // Resource list in hardware description
        REG_RESOURCE_REQUIREMENTS_LIST = 10,
        REG_QWORD = 11, // 64-bit number
        REG_QWORD_LITTLE_ENDIAN = 11, // 64-bit number (same as REG_QWORD)
    }
}
