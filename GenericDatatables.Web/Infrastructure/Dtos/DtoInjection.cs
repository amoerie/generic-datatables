namespace GenericDatatables.Web.Infrastructure.Dtos
{
    using System;
    using System.Linq;

    using Omu.ValueInjecter;

    /// <summary>
    /// Custom injection to support nested DTO properties
    /// </summary>
    public class DtoInjection: LoopValueInjection
    {
        /// <summary>
        /// If one of the types implements IDto, check if its generic type parameter
        /// matches the other type
        /// Otherwise, just use the default typematcher.
        /// </summary>
        /// <param name="sourceType"></param>
        /// <param name="targetType"></param>
        /// <returns></returns>
        protected override bool TypesMatch(Type sourceType, Type targetType)
        {
            if (this.IsDtoAndModelType(sourceType, targetType) || this.IsDtoAndModelType(targetType, sourceType))
                return true;
            return base.TypesMatch(sourceType, targetType);
        }

        /// <summary>
        /// If the target property or source property is a DTO, we'll need to recursively use 
        /// this class to set the value
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        protected override object SetValue(object v)
        {
            if (this.IsDtoAndModelType(this.SourcePropType, this.TargetPropType) || this.IsDtoAndModelType(this.TargetPropType, this.SourcePropType))
            {
                var target = Activator.CreateInstance(this.TargetPropType);
                if(v != null)
                    target.InjectFrom<DtoInjection>(v);
                return target;
            }
            return base.SetValue(v);
        }

        /// <summary>
        /// Returns true if the dtoType implements IDto with generic type modelType
        /// </summary>
        /// <param name="dtoType"></param>
        /// <param name="modelType"></param>
        /// <returns></returns>
        private bool IsDtoAndModelType(Type dtoType, Type modelType)
        {
            // get DTO interface from dtoType
            var dtoInterface = dtoType.GetInterfaces()
                .FirstOrDefault(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IDto<>));

            // if dtoType doesn't implement IDto, return false
            if (dtoInterface == null)
                return false;

            // Get the generic type argument and check if it matches the modeltype
            var dtoModelType = dtoInterface.GetGenericArguments().First();
            return dtoModelType == modelType;
        }
    }
}