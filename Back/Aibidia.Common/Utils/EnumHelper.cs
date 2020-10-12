using System;

namespace Aibidia.Common.Utils
{
    public static class EnumHelper
    {
        public static T GetEnum<T>(string enumValue)
        {
            return (T)Enum.Parse(typeof(T), enumValue);
        }
    }
}