using System;

namespace GenericDatatables.Core.Helper
{
    /// <summary>
    ///     Helper class that can instantiate a generically typed class when the type arguments are only known at runtime.
    ///     So instead of typing :
    ///     <para>
    ///         var myInstance = new GenericInstance&lt;Type1, Type2&gt;(myConstructorArguments)
    ///     </para>
    ///     you can say:
    ///     <para>
    ///         var myInstance = DatatableGenericTypeHelper.Create(typeof(GenericInstance&lt;,&gt;))
    ///         .WithTypeArguments(new Type[] { type1, type2 })
    ///         .WithConstructorArguments(myConstructorArguments)
    ///         .CreateInstance();
    ///     </para>
    ///     where the interesting thing is that type1 and type2 as type arguments can be computed at runtime
    ///     Why this class, you ask? You make a good point. This class just wraps a builder around what's essentially 2 lines of code.
    ///     A bit overkill, nay?
    ///     To tell you the truth, I might not write this class again had I the chance to do it all over again.
    ///     Still, I quite fancy it, and thus it stays. Such is life.
    /// </summary>
    internal class DatatableGenericTypeHelper : IDatatableTypeBuilder,
                                                IDatatableConstructorBuilder,
                                                IDatatableInstanceBuilder
    {
        #region Fields

        /// <summary>
        ///     The _type.
        /// </summary>
        private readonly Type _type;

        /// <summary>
        ///     The _constructor arguments.
        /// </summary>
        private object[] _constructorArguments;

        /// <summary>
        ///     The _type arguments.
        /// </summary>
        private Type[] _typeArguments;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="DatatableGenericTypeHelper" /> class.
        /// </summary>
        /// <param name="type">
        ///     The type.
        /// </param>
        private DatatableGenericTypeHelper(Type type)
        {
            _type = type;
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     Prepares the builder with constructor arguments
        /// </summary>
        /// <param name="constructorArguments">
        ///     The constructor arguments to be used on initialization of the creation type
        /// </param>
        /// <returns>
        ///     this builder
        /// </returns>
        public IDatatableInstanceBuilder WithConstructorArguments(object[] constructorArguments)
        {
            _constructorArguments = constructorArguments;
            return this;
        }

        /// <summary>
        ///     Creates an instance of the earlier provided type with the provided generic types, using the constructor arguments as the constructor arguments for the creation type
        /// </summary>
        /// <returns>
        ///     The <see cref="object" />.
        /// </returns>
        public object CreateInstance()
        {
            Type type = _type.MakeGenericType(_typeArguments);
            return Activator.CreateInstance(type, _constructorArguments);
        }

        /// <summary>
        ///     Prepares the builder with type arguments
        /// </summary>
        /// <param name="typeArguments">
        ///     The type arguments
        /// </param>
        /// <returns>
        ///     this builder
        /// </returns>
        public IDatatableConstructorBuilder WithTypeArguments(Type[] typeArguments)
        {
            _typeArguments = typeArguments;
            return this;
        }

        /// <summary>
        ///     Prepares the builder for the creation of a type
        /// </summary>
        /// <param name="type">
        ///     The desired type
        /// </param>
        /// <returns>
        ///     this builder
        /// </returns>
        public static IDatatableTypeBuilder Create(Type type)
        {
            return new DatatableGenericTypeHelper(type);
        }

        #endregion
    }
}