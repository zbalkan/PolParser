namespace PolParser
{
    public class GPRegistryPolicy
    {
        public string? KeyName { get; set; }
        public string? ValueName { get; set; }
        public RegType ValueType { get; set; }
        public int ValueLength { get; set; }
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
                int valueLength,
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
            string? result;
            switch (ValueType)
            {
                case RegType.REG_SZ: { result = "String"; break; }
                case RegType.REG_EXPAND_SZ: { result = "ExpandString"; break; }
                case RegType.REG_BINARY: { result = "Binary"; break; }
                case RegType.REG_DWORD: { result = "DWord"; break; }
                case RegType.REG_MULTI_SZ: { result = "MultiString"; break; }
                case RegType.REG_QWORD: { result = "QWord"; break; }
                default: { result = string.Empty; break; }
            }

            return result;
        }

        public static RegType GetRegTypeFromString(string type)
        {
            RegType result;
            switch (type)
            {
                case "String": { result = RegType.REG_SZ; break; }
                case "ExpandString": { result = RegType.REG_EXPAND_SZ; break; }
                case "Binary": { result = RegType.REG_BINARY; break; }
                case "DWord": { result = RegType.REG_DWORD; break; }
                case "MultiString": { result = RegType.REG_MULTI_SZ; break; }
                case "QWord": { result = RegType.REG_QWORD; break; }
                default: { result = RegType.REG_NONE; break; }
            }

            return result;
        }
    }
}
