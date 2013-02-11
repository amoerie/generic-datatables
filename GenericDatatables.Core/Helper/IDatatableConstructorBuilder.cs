namespace GenericDatatables.Core.Helper
{
    /// <summary>
    ///     The DatatableConstructorBuilder interface.
    /// </summary>
    internal interface IDatatableConstructorBuilder
    {
        #region Public Methods and Operators

        /// <summary>
        ///     The with constructor arguments.
        /// </summary>
        /// <param name="constructorArguments">
        ///     The constructor arguments.
        /// </param>
        /// <returns>
        ///     The <see cref="IDatatableInstanceBuilder" />.
        /// </returns>
        IDatatableInstanceBuilder WithConstructorArguments(object[] constructorArguments);

        #endregion
    }
}