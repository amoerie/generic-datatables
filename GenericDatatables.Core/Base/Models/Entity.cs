using System;
using GenericDatatables.Core.Base.Contracts;

namespace GenericDatatables.Core.Base.Models
{
    /// <summary>
    ///     This is the base class for all entities (classes that need to be persisted to the database)
    /// </summary>
    public abstract class Entity : IIdentifiable, IDeletable
    {
        /// <summary>
        ///     Gets or sets the id.
        /// </summary>
        public virtual int Id { get; set; }

        /// <summary>
        ///     Gets or sets the creation user id.
        /// </summary>
        public virtual int? CreationUserId { get; set; }

        /// <summary>
        ///     Gets or sets the creation date.
        /// </summary>
        public virtual DateTime CreationDate { get; set; }

        /// <summary>
        ///     Gets or sets the modification user id.
        /// </summary>
        public virtual int? ModificationUserId { get; set; }

        /// <summary>
        ///     Gets or sets the modification date.
        /// </summary>
        public virtual DateTime? ModificationDate { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether deleted.
        /// </summary>
        public bool Deleted { get; set; }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>
        /// A string that represents the current object.
        /// </returns>
        public override string ToString()
        {
            return string.Format("Id: {0}", Id);
        }
    }
}
