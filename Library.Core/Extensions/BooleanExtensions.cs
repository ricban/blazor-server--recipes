﻿namespace Library.Core.Extensions
{
    public static class BooleanExtensions
    {
        public static T IIf<T>(this bool condition, T trueValue, T falseValue)
        {
            return condition ? trueValue : falseValue;
        }
    }
}