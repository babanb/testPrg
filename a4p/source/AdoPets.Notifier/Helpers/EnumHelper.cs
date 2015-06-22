namespace AdoPets.Notifier.Helpers
{
    public static class EnumHelper
    {
        public static string GetResourceValueForEnumValue<TEnum>(TEnum enumValue)
        {
            if (Equals(default(TEnum), enumValue))
            {
                return string.Empty;
            }
            var key = string.Format("{0}_{1}", enumValue.GetType().Name, enumValue);
            return Resources.Enums.ResourceManager.GetString(key) ?? enumValue.ToString();
        }
    }
}