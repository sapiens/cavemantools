using System;

namespace CavemanTools.Model
{
    public struct OperationId:IEquatable<OperationId>
    {
        public Guid Value { get; }

        public OperationId(Guid value)
        {
            value.MustNotBeDefault();
            Value = value;
        }

        public bool Equals(OperationId other)
        {
            return Value.Equals(other.Value);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is OperationId && Equals((OperationId) obj);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }
    }
}