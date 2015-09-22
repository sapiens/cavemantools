using System;

namespace CavemanTools.Model
{
    public class IdName:IEquatable<IdName>
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public IdName()
        {
            
        }

        public IdName(int id,string name)
        {
            Id = id;
            Name = name;
        }

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <returns>
        /// true if the current object is equal to the <paramref name="other"/> parameter; otherwise, false.
        /// </returns>
        /// <param name="other">An object to compare with this object.</param>
        public bool Equals(IdName other)
        {
            if (other == null) return false;
            return Id == other.Id && Name == other.Name;
        }

        /// <summary>
        /// Determines whether the specified <see cref="T:System.Object"/> is equal to the current <see cref="T:System.Object"/>.
        /// </summary>
        /// <returns>
        /// true if the specified object  is equal to the current object; otherwise, false.
        /// </returns>
        /// <param name="obj">The object to compare with the current object. </param>
        public override bool Equals(object obj)
        {
            return Equals(obj as IdName);
        }

        /// <summary>
        /// Serves as a hash function for a particular type. 
        /// </summary>
        /// <returns>
        /// A hash code for the current <see cref="T:System.Object"/>.
        /// </returns>
        public override int GetHashCode()
        {
            return Id.GetHashCode() + 37*Name.GetHashCode();
        }
    }
}