using System.Collections.Generic;
using System.Linq;

namespace PolViewer
{
    public abstract class ValueObject<T> : System.IEquatable<ValueObject<T>>
    where T : ValueObject<T>
    {
        protected abstract IEnumerable<object> GetEqualityComponents();

        public override bool Equals(object? obj)
        {
            if (obj is not T valueObject)
                return false;

            if (GetType() != obj.GetType())
                return false;

            return GetEqualityComponents().SequenceEqual(valueObject.GetEqualityComponents());
        }

        public override int GetHashCode() => GetEqualityComponents()
                .Aggregate(1, (current, obj) =>
                {
                    unchecked
                    {
                        return (current * 23) + (obj?.GetHashCode() ?? 0);
                    }
                });

        public bool Equals(ValueObject<T>? other) => Equals(other as T);

        public static bool operator ==(ValueObject<T> a, ValueObject<T> b)
        {
            if (a is null && b is null)
                return true;

            if (a is null || b is null)
                return false;

            return a.Equals(b);
        }

        public static bool operator !=(ValueObject<T> a, ValueObject<T> b)
        {
            return !(a == b);
        }
    }
}
