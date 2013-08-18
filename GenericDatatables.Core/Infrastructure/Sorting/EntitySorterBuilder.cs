using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using GenericDatatables.Core.Base.Contracts;

namespace GenericDatatables.Core.Infrastructure.Sorting
{
    /// <summary>
    ///     Allows creation of <see cref="IEntitySorter{TEntity}" /> types, based on a supplied property name.
    /// </summary>
    /// <typeparam name="TEntity">
    ///     The type of the entity.
    /// </typeparam>
    internal class EntitySorterBuilder <TEntity>
    {
        /// <summary>
        ///     The _key selector.
        /// </summary>
        private readonly LambdaExpression _keySelector;

        /// <summary>
        ///     The _key type.
        /// </summary>
        private readonly Type _keyType;

        /// <summary>
        ///     Initializes a new instance of the <see cref="EntitySorterBuilder{TEntity}" /> class.
        /// </summary>
        /// <param name="propertyName">
        ///     Name of the property.
        /// </param>
        /// <exception cref="ArgumentNullException">
        ///     Thrown when <paramref name="propertyName" /> is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        ///     Thrown when <paramref name="propertyName" /> is an empty string
        ///     or <paramref name="propertyName" /> could not be parsed or is not a property or chain of properties
        ///     referenced by the given <typeparamref name="TEntity" />.
        /// </exception>
        public EntitySorterBuilder(string propertyName)
        {
            if (propertyName == null)
            {
                throw new ArgumentNullException("propertyName");
            }

            if (propertyName.Length == 0)
            {
                throw new ArgumentException("The value should not be an empty string.", "propertyName");
            }

            List<PropertyInfo> properties = GetProperties(propertyName);

            _keyType = properties.Last().PropertyType;

            _keySelector = BuildLambda(properties, _keyType);
        }

        /// <summary>Gets or sets the sort direction.</summary>
        public SortDirection SortDirection { get; set; }

        /// <summary>
        ///     The build order by entity sorter.
        /// </summary>
        /// <returns>
        ///     The <see cref="IEntitySorter{TEntity}" />.
        /// </returns>
        internal IEntitySorter<TEntity> BuildOrderByEntitySorter()
        {
            var typeArguments = new[] {typeof (TEntity), _keyType};

            Type sorterType = typeof (OrderByEntitySorter<,>).MakeGenericType(typeArguments);

            object[] constructorArguments = {_keySelector, SortDirection};

            object instance = Activator.CreateInstance(sorterType, constructorArguments);

            return (IEntitySorter<TEntity>) instance;
        }

        /// <summary>
        ///     The build then by entity sorter.
        /// </summary>
        /// <param name="baseSorter">
        ///     The base sorter.
        /// </param>
        /// <returns>
        ///     The <see cref="IEntitySorter{TEntity}" />.
        /// </returns>
        internal IEntitySorter<TEntity> BuildThenByEntitySorter(IEntitySorter<TEntity> baseSorter)
        {
            var typeArguments = new[] {typeof (TEntity), _keyType};

            Type sorterType = typeof (ThenByEntitySorter<,>).MakeGenericType(typeArguments);

            object[] constructorArguments = {baseSorter, _keySelector, SortDirection};

            object instance = Activator.CreateInstance(sorterType, constructorArguments);

            return (IEntitySorter<TEntity>) instance;
        }

        // Builds a Expression<Func<TEntity, TKey>>
        /// <summary>
        ///     The build lambda.
        /// </summary>
        /// <param name="properties">
        ///     The properties.
        /// </param>
        /// <param name="keyType">
        ///     The key type.
        /// </param>
        /// <returns>
        ///     The <see cref="LambdaExpression" />.
        /// </returns>
        private static LambdaExpression BuildLambda(IEnumerable<PropertyInfo> properties, Type keyType)
        {
            ILambdaBuilder lambdaBuilder = CreateGenericLambdaBuilder(keyType);

            return lambdaBuilder.BuildLambda(properties);
        }

        /// <summary>
        ///     The create generic lambda builder.
        /// </summary>
        /// <param name="keyType">
        ///     The key type.
        /// </param>
        /// <returns>
        ///     The <see cref="EntitySorterBuilder{TEntity}" />.
        /// </returns>
        private static ILambdaBuilder CreateGenericLambdaBuilder(Type keyType)
        {
            var typeArguments = new[] {typeof (TEntity), keyType};

            Type lambdaBuilderType = typeof (GenericLambdaBuilder<>).MakeGenericType(typeArguments);

            return (ILambdaBuilder) Activator.CreateInstance(lambdaBuilderType);
        }

        // Throws an ArgumentException when the propertyNameChain is invalid.
        /// <summary>
        ///     The get property accessors.
        /// </summary>
        /// <param name="propertyName">
        ///     The property name.
        /// </param>
        /// <returns>
        ///     The <see cref="List{T}" />.
        /// </returns>
        /// <exception cref="ArgumentException">
        /// </exception>
        private static List<PropertyInfo> GetProperties(string propertyName)
        {
            try
            {
                string propertyNameChain = propertyName;

                return GetPropertiesFromPropertyNameChain(propertyNameChain);
            }
            catch (InvalidOperationException ex)
            {
                string exceptionMessage = string.Format(
                    CultureInfo.InvariantCulture, "'{0}' could not be parsed. ", propertyName);

                // We throw a more expressive exception at this level.
                throw new ArgumentException(exceptionMessage + ex.Message, "propertyName");
            }
        }

        /// <summary>
        ///     The get property accessors from property name chain.
        /// </summary>
        /// <param name="propertyNameChain">
        ///     The property name chain.
        /// </param>
        /// <returns>
        ///     The <see cref="List{T}" />.
        /// </returns>
        private static List<PropertyInfo> GetPropertiesFromPropertyNameChain(string propertyNameChain)
        {
            var properties = new List<PropertyInfo>();

            Type declaringTypeForProperty = typeof (TEntity);

            string[] propertyNames = propertyNameChain.Split('.');

            foreach (string propertyName in propertyNames)
            {
                PropertyInfo property = GetProperty(declaringTypeForProperty, propertyName);

                properties.Add(property);

                declaringTypeForProperty = property.PropertyType;
            }

            return properties;
        }

        // Throws an InvalidOperationException when property with name does not exist or doens't have a getter.
        /// <summary>
        ///     The get property accessor.
        /// </summary>
        /// <param name="declaringType">
        ///     The declaring type.
        /// </param>
        /// <param name="propertyName">
        ///     The property name.
        /// </param>
        /// <returns>
        ///     The <see cref="MethodInfo" />.
        /// </returns>
        private static PropertyInfo GetProperty(Type declaringType, string propertyName)
        {
            PropertyInfo property = GetPropertyByName(declaringType, propertyName);
            return property;
        }

        /// <summary>
        ///     The get property by name.
        /// </summary>
        /// <param name="declaringType">
        ///     The declaring type.
        /// </param>
        /// <param name="propertyName">
        ///     The property name.
        /// </param>
        /// <returns>
        ///     The <see cref="PropertyInfo" />.
        /// </returns>
        /// <exception cref="InvalidOperationException">
        /// </exception>
        private static PropertyInfo GetPropertyByName(Type declaringType, string propertyName)
        {
            const BindingFlags flags = BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.Public;

            PropertyInfo property = declaringType.GetProperty(propertyName, flags);

            if (property == null)
            {
                string exceptionMessage = string.Format(
                    CultureInfo.InvariantCulture,
                    "{0} does not contain a property named '{1}'.",
                    declaringType,
                    propertyName);

                throw new InvalidOperationException(exceptionMessage);
            }

            return property;
        }

        /// <summary>
        ///     The get property getter.
        /// </summary>
        /// <param name="propertyInfo">
        ///     The property info.
        /// </param>
        /// <param name="declaringType">
        ///     The declaring type.
        /// </param>
        /// <returns>
        ///     The <see cref="MethodInfo" />.
        /// </returns>
        /// <exception cref="InvalidOperationException">
        /// </exception>
        private static MethodInfo GetPropertyGetter(PropertyInfo propertyInfo, Type declaringType)
        {
            MethodInfo propertyAccessor = propertyInfo.GetGetMethod();

            if (propertyAccessor == null)
            {
                string exceptionMessage = string.Format(
                    CultureInfo.InvariantCulture,
                    "The property '{1}' of type {0} does not contain a public getter.",
                    declaringType,
                    propertyInfo.Name);

                throw new InvalidOperationException(exceptionMessage);
            }

            return propertyAccessor;
        }

        /// <summary>
        ///     Concrete (generic) implementation of the LambdaBuilder class to allow easy creation of
        ///     Expression{Func{TEntity, TKey}} objects.
        /// </summary>
        /// <typeparam name="TKey">
        ///     The type of the key.
        /// </typeparam>
        private sealed class GenericLambdaBuilder <TKey> : ILambdaBuilder
        {
            /// <summary>
            ///     Builds the lambda from the supplied list of property accessors.
            /// </summary>
            /// <param name="properties">
            ///     The property accessors.
            /// </param>
            /// <returns>
            ///     A new <see cref="LambdaExpression" />.
            /// </returns>
            public LambdaExpression BuildLambda(IEnumerable<PropertyInfo> properties)
            {
                ParameterExpression parameterExpression = Expression.Parameter(typeof (TEntity), "entity");

                Expression propertyExpression = BuildPropertyExpression(properties, parameterExpression);

                return Expression.Lambda<Func<TEntity, TKey>>(propertyExpression, new[] {parameterExpression});
            }

            /// <summary>
            ///     The build property expression.
            /// </summary>
            /// <param name="properties">
            ///     The properties.
            /// </param>
            /// <param name="parameterExpression">
            ///     The parameter expression.
            /// </param>
            /// <returns>
            ///     The <see cref="Expression" />.
            /// </returns>
            private static Expression BuildPropertyExpression(
                IEnumerable<PropertyInfo> properties, ParameterExpression parameterExpression)
            {
                Expression propertyExpression = null;

                foreach (PropertyInfo property in properties)
                {
                    Expression innerExpression = propertyExpression ?? parameterExpression;

                    propertyExpression = Expression.Property(innerExpression, property);
                }

                return propertyExpression;
            }
        }

        /// <summary>Defines a method to build LambdaExpression from a list of property accessors.</summary>
        private interface ILambdaBuilder
        {
            /// <summary>
            ///     Builds the lambda from the supplied list of properties.
            /// </summary>
            /// <param name="properties">
            ///     The properties.
            /// </param>
            /// <returns>
            ///     A new <see cref="LambdaExpression" />.
            /// </returns>
            LambdaExpression BuildLambda(IEnumerable<PropertyInfo> properties);
        }
    }
}