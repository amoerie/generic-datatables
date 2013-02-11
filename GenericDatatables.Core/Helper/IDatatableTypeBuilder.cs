using System;

namespace GenericDatatables.Core.Helper
{
    /// <summary>
    ///     The DatatableTypeBuilder interface.
    /// </summary>
    internal interface IDatatableTypeBuilder
    {
        #region Public Methods and Operators

        /// <summary>
        ///     The with type arguments.
        /// </summary>
        /// <param name="typeArguments">
        ///     The type arguments.
        /// </param>
        /// <returns>
        ///     The <see cref="IDatatableConstructorBuilder" />.
        /// </returns>
        IDatatableConstructorBuilder WithTypeArguments(Type[] typeArguments);

        #endregion
    }
}