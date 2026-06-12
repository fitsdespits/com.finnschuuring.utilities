namespace FinnSchuuring.Utilities {
    using System;

    public static class ComparisonHelper {
        public static bool Predicate<T>(T valueA, T valueB, ComparisonType type) where T : IComparable<T> {
            if (valueA is null || valueB is null) {
                return type switch {
                    ComparisonType.Equal => valueA is null && valueB is null,
                    ComparisonType.NotEqual => !(valueA is null && valueB is null),
                    _ => false
                };
            }
            int comparison = valueA.CompareTo(valueB);
            return type switch {
                ComparisonType.Equal => comparison == 0,
                ComparisonType.NotEqual => comparison != 0,
                ComparisonType.Greater => comparison > 0,
                ComparisonType.GreaterEqual => comparison >= 0,
                ComparisonType.Less => comparison < 0,
                ComparisonType.LessEqual => comparison <= 0,
                _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
            };
        }
    }
}
