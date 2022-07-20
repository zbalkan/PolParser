namespace PolParser
{
    public class GPRegistryPolicy
    {
        public string? KeyName { get; set; }
        public string? ValueName { get; set; }
        public RegType ValueType { get; set; }
        public long ValueLength { get; set; }
        public string? ValueData { get; set; }

        public GPRegistryPolicy()
        {
            KeyName = null;
            ValueName = null;
            ValueType = RegType.REG_NONE;
            ValueLength = 0;
            ValueData = default;
        }

        public GPRegistryPolicy(
                string keyName,
                string valueName,
                RegType valueType,
                long valueLength,
                string? valueData
            )
        {
            KeyName = keyName;
            ValueName = valueName;
            ValueType = valueType;
            ValueLength = valueLength;
            ValueData = valueData;
        }

        public string GetRegTypeString()
        {
            switch (ValueType)
            {
                case RegType.REG_SZ: { return "String"; }
                case RegType.REG_EXPAND_SZ: { return "ExpandString"; }
                case RegType.REG_BINARY: { return "Binary"; }
                case RegType.REG_DWORD: { return "DWord"; }
                case RegType.REG_MULTI_SZ: { return "MultiString"; }
                case RegType.REG_QWORD: { return "QWord"; }
                default: { return string.Empty; }
            }
        }

        public static RegType GetRegTypeFromString(string type)
        {
            switch (type)
            {
                case "String": { return RegType.REG_SZ; }
                case "ExpandString": { return RegType.REG_EXPAND_SZ; }
                case "Binary": { return RegType.REG_BINARY; }
                case "DWord": { return RegType.REG_DWORD; }
                case "MultiString": { return RegType.REG_MULTI_SZ; }
                case "QWord": { return RegType.REG_QWORD; }
                default: { return RegType.REG_NONE; }
            }
        }
    }
}
