namespace GenericDatatables.Core.Helper
{
    /// <summary>
    ///     The DatatableInstanceBuilder interface.
    /// </summary>
    internal interface IDatatableInstanceBuilder
    {
        #region Public Methods and Operators

        /// <summary>
        ///     The create instance.
        /// </summary>
        /// <returns>
        ///     The <see cref="object" />.
        /// </returns>
        object CreateInstance();

        #endregion
    }
}