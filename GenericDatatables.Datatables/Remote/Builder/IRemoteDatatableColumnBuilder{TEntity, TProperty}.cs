using System;
using System.Linq.Expressions;
using System.Web;
using GenericDatatables.Core.Base.Models;
using GenericDatatables.Core.Infrastructure.Attributes.JetBrains.Annotations;
using GenericDatatables.Datatables.Html.Components.DisplayComponents;
using GenericDatatables.Datatables.Html.SearchComponents;

namespace GenericDatatables.Datatables.Remote.Builder
{
    /// <summary>
    ///     This interface provides builder-style methods to construct instances of <see cref="IRemoteDatatableColumn{TEntity, TProperty}"/>
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity</typeparam>
    /// <typeparam name="TProperty">The type of the property</typeparam>
    public interface IRemoteDatatableColumnBuilder<TEntity, TProperty>: IRemoteDatatableBuilder<TEntity> where TEntity : Entity
    {
        /// <summary>
        ///     Configures how this <see cref="IRemoteDatatableColumn{TEntity, TProperty}"/> should be displayed using a display function
        /// </summary>
        /// <param name="display">The display function that specifies how each row should be displayed</param>
        /// <example>
        /// <code>
        ///     Display(gymMember => string.Format("{0} {1}", gymMember.FirstName, gymMember.LastName))
        /// </code>
        /// </example>
        /// <returns>this <see cref="IRemoteDatatableColumnBuilder{TEntity,TProperty}"/></returns>
        IRemoteDatatableColumnBuilder<TEntity, TProperty> Display([NotNull] Func<TEntity, string> display);

        /// <summary>
        ///     Configures how this <see cref="IRemoteDatatableColumn{TEntity, TProperty}"/> should be displayed using a display function
        /// </summary>
        /// <param name="display">The display function that specifies how each row should be displayed</param>
        /// <returns>this <see cref="IRemoteDatatableColumnBuilder{TEntity,TProperty}"/></returns>
        IRemoteDatatableColumnBuilder<TEntity, TProperty> Display([NotNull] Func<TEntity, IHtmlString> display);

        /// <summary>
        ///     Configures how this <see cref="IRemoteDatatableColumn{TEntity, TProperty}"/> should be displayed using an <see cref="IDisplayComponent{TProperty}"/>
        /// </summary>
        /// <param name="displayComponent">The display component that specifies how each row should be displayed</param>
        /// <returns>this <see cref="IRemoteDatatableColumnBuilder{TEntity,TProperty}"/></returns>
        IRemoteDatatableColumnBuilder<TEntity, TProperty> Display([NotNull] IDisplayComponent<TProperty> displayComponent);

        /// <summary>
        ///     Configures how this <see cref="IRemoteDatatableColumn{TEntity, TProperty}"/> should be filtered when the a search value is provided for this <see cref="IRemoteDatatableColumn{TEntity, TProperty}"/>
        /// </summary>
        /// <example>
        /// <code>
        ///     Search((gymMember, search) => gymMember.FirstName.ToLower().Contains(search.ToLower()) 
        ///                                    || gymMember.LastName.ToLower().Contains(search.ToLower()))
        /// </code>
        /// </example>
        /// <param name="searchFilter">The <see cref="Expression{TDelegate}"/> that takes a <typeparamref name="TEntity"/> and a <see cref="string"/> value and returns a <see cref="bool"/> value indicating whether the <see cref="TEntity"/> satisfies the filter or not</param>
        /// <returns>this <see cref="IRemoteDatatableColumnBuilder{TEntity,TProperty}"/></returns>
        IRemoteDatatableColumnBuilder<TEntity, TProperty> Search([NotNull] Expression<Func<TEntity, string, bool>> searchFilter);

        /// <summary>
        ///     Configures how this <see cref="IRemoteDatatableColumn{TEntity, TProperty}"/> should be filtered when the a search value is provided for this <see cref="IRemoteDatatableColumn{TEntity, TProperty}"/>
        /// </summary>
        /// <example>
        /// <code>
        ///     Search(search => Convert.ToDateTime(search), 
        ///            (gymMember, search) => gymMember.DateOfBirth == search)
        /// </code>
        /// </example>
        /// <param name="searchParser">The parser that takes the incoming <see cref="string"/> search value and converts into a value of type <typeparamref name="TSearch"/>. This is essentially a preprocessor for incoming search filters.</param>
        /// <param name="searchFilter">The <see cref="Expression{TDelegate}"/> that takes a <typeparamref name="TEntity"/> and a <see cref="string"/> value and returns a <see cref="bool"/> value indicating whether the <see cref="TEntity"/> satisfies the filter or not</param>
        /// <typeparam name="TSearch">The type of the search value after the <paramref name="searchParser"/> has parsed the initial <see cref="string"/> search value</typeparam>
        /// <returns>this <see cref="IRemoteDatatableColumnBuilder{TEntity,TProperty}"/></returns>
        IRemoteDatatableColumnBuilder<TEntity, TProperty> Search<TSearch>([NotNull] Func<string, TSearch> searchParser, [NotNull] Expression<Func<TEntity, TSearch, bool>> searchFilter);

        /// <summary>
        ///     Configures whether this <see cref="IRemoteDatatableColumn{TEntity, TProperty}"/> is sortable or not.
        /// </summary>
        /// <param name="sortable">True if the column should be sortable or false otherwise</param>
        /// <returns>this <see cref="IRemoteDatatableColumn{TEntity, TProperty}"/></returns>
        IRemoteDatatableColumnBuilder<TEntity, TProperty> Sortable(bool sortable);

        /// <summary>
        ///     Configures whether this <see cref="IRemoteDatatableColumn{TEntity, TProperty}"/> is searchable or not.
        /// </summary>
        /// <param name="searchable">True if the column should be searchable or false otherwise</param>
        /// <returns>this <see cref="IRemoteDatatableColumn{TEntity, TProperty}"/></returns>
        IRemoteDatatableColumnBuilder<TEntity, TProperty> Searchable(bool searchable);

        /// <summary>
        ///     Configures whether this <see cref="IRemoteDatatableColumn{TEntity, TProperty}"/> is visible or not.
        /// </summary>
        /// <param name="visible">True if the column should be visible or false otherwise</param>
        /// <returns>this <see cref="IRemoteDatatableColumn{TEntity, TProperty}"/></returns>
        IRemoteDatatableColumnBuilder<TEntity, TProperty> Visible(bool visible);

        /// <summary>
        ///     Configures the width of this <see cref="IRemoteDatatableColumn{TEntity, TProperty}"/>
        /// </summary>
        /// <param name="width">The width value. Any css width value is accepted</param>
        /// <returns>this <see cref="IRemoteDatatableColumn{TEntity, TProperty}"/></returns>
        IRemoteDatatableColumnBuilder<TEntity, TProperty> Width([NotNull] string width);

        /// <summary>
        ///     Configures the class of this <see cref="IRemoteDatatableColumn{TEntity, TProperty}"/>
        /// </summary>
        /// <param name="class">The class that should be added to this <see cref="IRemoteDatatableColumn{TEntity, TProperty}"/></param>
        /// <returns>this <see cref="IRemoteDatatableColumn{TEntity, TProperty}"/></returns>
        IRemoteDatatableColumnBuilder<TEntity, TProperty> Class([NotNull] string @class);

        /// <summary>
        ///     Configures the default content of this <see cref="IRemoteDatatableColumn{TEntity, TProperty}"/>
        /// </summary>
        /// <param name="defaultContent">The default content that should be used when a specific row has a null value for this column</param>
        /// <returns>this <see cref="IRemoteDatatableColumn{TEntity, TProperty}"/></returns>
        IRemoteDatatableColumnBuilder<TEntity, TProperty> DefaultContent([NotNull] string defaultContent);

        /// <summary>
        ///     Configures the <see cref="ISearchComponent"/> for this <see cref="IRemoteDatatableColumn{TEntity, TProperty}"/>
        /// </summary>
        /// <param name="searchComponent">The <see cref="ISearchComponent"/> that will be used for this <see cref="IRemoteDatatableColumn{TEntity, TProperty}"/></param>
        /// <returns>this <see cref="IRemoteDatatableColumn{TEntity, TProperty}"/></returns>
        IRemoteDatatableColumnBuilder<TEntity, TProperty> SearchComponent([NotNull] ISearchComponent searchComponent);
    }
}
