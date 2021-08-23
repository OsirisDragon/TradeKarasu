using CrossModel.Enum;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TradeUtility
{
    public static class Common
    {
        public static bool IsValidTick(decimal inputValue, decimal unit)
        {
            bool result = false;

            if (inputValue % unit == 0)
                result = true;

            return result;
        }

        public static IEnumerable<(T item, int index)> WithIndex<T>(this IEnumerable<T> self) => self?.Select((item, index) => (item, index)) ?? new List<(T, int)>();
    }
}