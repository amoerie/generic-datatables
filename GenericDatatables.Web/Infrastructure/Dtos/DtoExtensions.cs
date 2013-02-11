namespace GenericDatatables.Web.Infrastructure.Dtos
{
    using Omu.ValueInjecter;

    public static class DtoExtensions
    {
        /// <summary>
        /// Make a new model and inject dto values
        /// </summary>
        /// <typeparam name="TModel">The type of the model</typeparam>
        /// <param name="dto">The dto object that contains the property values</param>
        /// <returns>A new instance of TModel with the same property values of this dto</returns>
        public static TModel ToModel<TModel>(this IDto<TModel> dto)
            where TModel : class, new()
        {
            var model = new TModel();
            dto.UpdateModel(model);
            return model;
        }

        /// <summary>
        /// Updates an existing model with the property values of the dto
        /// </summary>
        /// <typeparam name="TModel">The type of the model</typeparam>
        /// <param name="dto">The dto object that will provide the property values</param>
        /// <param name="model">The model object that will take the property values</param>
        public static void UpdateModel<TModel>(this IDto<TModel> dto, TModel model)
            where TModel : class, new()
        {
            model.InjectFrom<DtoInjection>(dto);
        }

        /// <summary>
        /// Injects the properties of the given model into this dto
        /// </summary>
        /// <typeparam name="TDto">The type of the DTO, which should implement IDto of type TModel</typeparam>
        /// <typeparam name="TModel">The type of the model</typeparam>
        /// <param name="dto">The dto that will take the property values</param>
        /// <param name="model">The model that provides the property values</param>
        /// <returns>This dto with its properties injected by the model</returns>
        public static TDto FromModel<TDto, TModel>(this TDto dto, TModel model)
            where TDto : IDto<TModel>
            where TModel : class, new()
        {
            dto.InjectFrom<DtoInjection>(model);
            return dto;
        } 
    }
}
