using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace GenericDatatables.Test.Models
{
    public class Person
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        public string Name { get; set; }
        public DateTime Birthday { get; set; }
        public TimeSpan Time { get; set; }

        public int AddressId { get; set; }
        public virtual Address Address { get; set; }

        /// <summary>
        ///     Returns a string that represents the current object.
        /// </summary>
        /// <returns>
        ///     A string that represents the current object.
        /// </returns>
        public override string ToString()
        {
            //return string.Format("Name: {0}", this.Name);
            return string.Format(
                "Id: {0}, " +
                "Name: {1}, " +
                "Birthday: {2}, " +
                "Time: {3}, " +
                "AddressId : {4}, " +
                "Address: [ {5} ]",
                Id,
                Name,
                Birthday,
                Time,
                AddressId,
                Address);
        }

        public bool Equals(Person other)
        {
            return Id == other.Id
                   && string.Equals(Name, other.Name)
                   && Birthday.Equals(other.Birthday)
                   && Time.Equals(other.Time)
                   && AddressId == other.AddressId
                   && Equals(Address, other.Address);
        }

        /// <summary>
        ///     Determines whether the specified <see cref="T:System.Object" /> is equal to the current <see cref="T:System.Object" />.
        /// </summary>
        /// <returns>
        ///     true if the specified object  is equal to the current object; otherwise, false.
        /// </returns>
        /// <param name="obj">The object to compare with the current object. </param>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }
            if (ReferenceEquals(this, obj))
            {
                return true;
            }
            if (obj.GetType() != GetType())
            {
                return false;
            }
            return Equals((Person) obj);
        }

        /// <summary>
        ///     Serves as a hash function for a particular type.
        /// </summary>
        /// <returns>
        ///     A hash code for the current <see cref="T:System.Object" />.
        /// </returns>
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = Id;
                hashCode = (hashCode*397) ^ (Name != null ? Name.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ Birthday.GetHashCode();
                hashCode = (hashCode*397) ^ Time.GetHashCode();
                hashCode = (hashCode*397) ^ AddressId;
                hashCode = (hashCode*397) ^ (Address != null ? Address.GetHashCode() : 0);
                return hashCode;
            }
        }
    }
}