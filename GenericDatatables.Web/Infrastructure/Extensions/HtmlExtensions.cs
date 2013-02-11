using System;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace GenericDatatables.Web.Infrastructure.Extensions
{
    /// <summary>
    /// Author: Alex
    /// </summary>
    public static class HtmlExtensions
    {
        /// <summary>
        /// Method to build form input with label, editor and validation message.
        /// 
        /// Example usage: Html.FormElementFor(model => model.MyProperty, Html.TextAreaFor)
        /// </summary>
        /// <typeparam name="TModel">Model</typeparam>
        /// <typeparam name="TProperty">Property of the model</typeparam>
        /// <param name="html">This Html Helper</param>
        /// <param name="expression">Expression to get property of the model</param>
        /// <param name="editor">Editor expression that takes the previous expression parameter and converts it into a MvcHtmlString</param>
        /// <returns></returns>
        public static MvcHtmlString FormElementFor<TModel, TProperty>(this HtmlHelper<TModel> html,
                                                              Expression<Func<TModel, TProperty>> expression,
                                                              Func<Expression<Func<TModel, TProperty>>, MvcHtmlString> editor)
        {
            return FormElementFor(html, expression, editor(expression));
        }

        /// <summary>
        /// Method to build form input with label, editor and validation message.
        /// 
        /// Example usage: Html.FormElementFor(model => model.MyProperty)
        /// </summary>
        /// <typeparam name="TModel">Model</typeparam>
        /// <typeparam name="TProperty">Property of the model</typeparam>
        /// <param name="html">This Html Helper</param>
        /// <param name="expression">Expression to get property of the model</param>
        /// <returns></returns>
        public static MvcHtmlString FormElementFor<TModel, TProperty>(this HtmlHelper<TModel> html,
                                                                      Expression<Func<TModel, TProperty>> expression)
        {
            return FormElementFor(html, expression, html.EditorFor(expression));
        }

        /// <summary>
        /// Method to build form input with label, editor and validation message.
        /// 
        /// Example usage: Html.FormElementFor(model => model.MyProperty, Html.DropDownListFor(model => model.MyProperty, MySelectList))
        /// </summary>
        /// <typeparam name="TModel">Model</typeparam>
        /// <typeparam name="TProperty">Property of the model</typeparam>
        /// <param name="html">This Html Helper</param>
        /// <param name="expression">Expression to get property of the model</param>
        /// <param name="editor">Some custom MvcHtmlString to use as the editor field</param>
        /// <returns></returns>
        public static MvcHtmlString FormElementFor<TModel, TProperty>(this HtmlHelper<TModel> html, 
            Expression<Func<TModel, TProperty>> expression, MvcHtmlString editor)
        {
            var label = new TagBuilder("div");
            label.AddCssClass("editor-label");
            label.InnerHtml = html.LabelFor(expression).ToHtmlString();
            var field = new TagBuilder("div");
            field.AddCssClass("editor-field");
            field.InnerHtml = editor.ToHtmlString() + html.ValidationMessageFor(expression);
            return MvcHtmlString.Create(label.ToString(TagRenderMode.Normal) + field.ToString(TagRenderMode.Normal));
        }

        public static IHtmlString DisplayNameFor<TModel, TProperty> (this HtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> expression) 
        {
            var name = ExpressionHelper.GetExpressionText(expression);
            name = helper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(name);
            var metadata = ModelMetadataProviders.Current.GetMetadataForProperty(() => Activator.CreateInstance<TModel>(), typeof(TModel), name);
            return new MvcHtmlString(metadata.DisplayName);
        }
    }
}