//------------------------------------------------------------------------------
// <auto-generated>
//   This code was generated by a tool.
//
//    Umbraco.ModelsBuilder.Embedded v10.4.2+c5fe779
//
//   Changes to this file will be lost if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Linq.Expressions;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.PublishedCache;
using Umbraco.Cms.Infrastructure.ModelsBuilder;
using Umbraco.Cms.Core;
using Umbraco.Extensions;

namespace Umbraco.Cms.Web.Common.PublishedModels
{
	/// <summary>Radios settings</summary>
	[PublishedModel("govukRadiosSettings")]
	public partial class GovukRadiosSettings : PublishedElementModel, IGovukCssClasses, IGovukGrid, IGovukGridColumnClasses, IGovukLegendIsPageHeading, IGovukModelProperty, IGovukValidationCustom, IGovukValidationRequired
	{
		// helpers
#pragma warning disable 0109 // new is redundant
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "10.4.2+c5fe779")]
		public new const string ModelTypeAlias = "govukRadiosSettings";
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "10.4.2+c5fe779")]
		public new const PublishedItemType ModelItemType = PublishedItemType.Content;
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "10.4.2+c5fe779")]
		[return: global::System.Diagnostics.CodeAnalysis.MaybeNull]
		public new static IPublishedContentType GetModelContentType(IPublishedSnapshotAccessor publishedSnapshotAccessor)
			=> PublishedModelUtility.GetModelContentType(publishedSnapshotAccessor, ModelItemType, ModelTypeAlias);
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "10.4.2+c5fe779")]
		[return: global::System.Diagnostics.CodeAnalysis.MaybeNull]
		public static IPublishedPropertyType GetModelPropertyType<TValue>(IPublishedSnapshotAccessor publishedSnapshotAccessor, Expression<Func<GovukRadiosSettings, TValue>> selector)
			=> PublishedModelUtility.GetModelPropertyType(GetModelContentType(publishedSnapshotAccessor), selector);
#pragma warning restore 0109

		private IPublishedValueFallback _publishedValueFallback;

		// ctor
		public GovukRadiosSettings(IPublishedElement content, IPublishedValueFallback publishedValueFallback)
			: base(content, publishedValueFallback)
		{
			_publishedValueFallback = publishedValueFallback;
		}

		// properties

		///<summary>
		/// Layout: Only use horizontal radios when the question only has two options and both options are short.
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "10.4.2+c5fe779")]
		[global::System.Diagnostics.CodeAnalysis.MaybeNull]
		[ImplementPropertyType("layout")]
		public virtual string Layout => this.Value<string>(_publishedValueFallback, "layout");

		///<summary>
		/// CSS classes
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "10.4.2+c5fe779")]
		[global::System.Diagnostics.CodeAnalysis.MaybeNull]
		[ImplementPropertyType("cssClasses")]
		public virtual string CssClasses => global::Umbraco.Cms.Web.Common.PublishedModels.GovukCssClasses.GetCssClasses(this, _publishedValueFallback);

		///<summary>
		/// CSS classes for row
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "10.4.2+c5fe779")]
		[global::System.Diagnostics.CodeAnalysis.MaybeNull]
		[ImplementPropertyType("cssClassesForRow")]
		public virtual string CssClassesForRow => global::Umbraco.Cms.Web.Common.PublishedModels.GovukGrid.GetCssClassesForRow(this, _publishedValueFallback);

		///<summary>
		/// Column size
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "10.4.2+c5fe779")]
		[global::System.Diagnostics.CodeAnalysis.MaybeNull]
		[ImplementPropertyType("columnSize")]
		public virtual string ColumnSize => global::Umbraco.Cms.Web.Common.PublishedModels.GovukGridColumnClasses.GetColumnSize(this, _publishedValueFallback);

		///<summary>
		/// Column size (from desktop): Defaults to 'two-thirds' if both column size properties are left blank.
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "10.4.2+c5fe779")]
		[global::System.Diagnostics.CodeAnalysis.MaybeNull]
		[ImplementPropertyType("columnSizeFromDesktop")]
		public virtual string ColumnSizeFromDesktop => global::Umbraco.Cms.Web.Common.PublishedModels.GovukGridColumnClasses.GetColumnSizeFromDesktop(this, _publishedValueFallback);

		///<summary>
		/// CSS classes for column
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "10.4.2+c5fe779")]
		[global::System.Diagnostics.CodeAnalysis.MaybeNull]
		[ImplementPropertyType("cssClassesForColumn")]
		public virtual string CssClassesForColumn => global::Umbraco.Cms.Web.Common.PublishedModels.GovukGridColumnClasses.GetCssClassesForColumn(this, _publishedValueFallback);

		///<summary>
		/// The legend is the page heading
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "10.4.2+c5fe779")]
		[ImplementPropertyType("legendIsPageHeading")]
		public virtual bool LegendIsPageHeading => global::Umbraco.Cms.Web.Common.PublishedModels.GovukLegendIsPageHeading.GetLegendIsPageHeading(this, _publishedValueFallback);

		///<summary>
		/// Model property: The name of the property on the view model being bound to in the code.
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "10.4.2+c5fe779")]
		[global::System.Diagnostics.CodeAnalysis.MaybeNull]
		[ImplementPropertyType("modelProperty")]
		public virtual object ModelProperty => global::Umbraco.Cms.Web.Common.PublishedModels.GovukModelProperty.GetModelProperty(this, _publishedValueFallback);

		///<summary>
		/// Custom error 1: Sets the message displayed by a custom validator.
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "10.4.2+c5fe779")]
		[global::System.Diagnostics.CodeAnalysis.MaybeNull]
		[ImplementPropertyType("customError1")]
		public virtual string CustomError1 => global::Umbraco.Cms.Web.Common.PublishedModels.GovukValidationCustom.GetCustomError1(this, _publishedValueFallback);

		///<summary>
		/// Custom error 2: Sets the message displayed by a custom validator.
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "10.4.2+c5fe779")]
		[global::System.Diagnostics.CodeAnalysis.MaybeNull]
		[ImplementPropertyType("customError2")]
		public virtual string CustomError2 => global::Umbraco.Cms.Web.Common.PublishedModels.GovukValidationCustom.GetCustomError2(this, _publishedValueFallback);

		///<summary>
		/// Custom error 3: Sets the message displayed by a custom validator.
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "10.4.2+c5fe779")]
		[global::System.Diagnostics.CodeAnalysis.MaybeNull]
		[ImplementPropertyType("customError3")]
		public virtual string CustomError3 => global::Umbraco.Cms.Web.Common.PublishedModels.GovukValidationCustom.GetCustomError3(this, _publishedValueFallback);

		///<summary>
		/// Required: Sets the message displayed if the field is set by the code to be required.
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "10.4.2+c5fe779")]
		[global::System.Diagnostics.CodeAnalysis.MaybeNull]
		[ImplementPropertyType("errorMessageRequired")]
		public virtual string ErrorMessageRequired => global::Umbraco.Cms.Web.Common.PublishedModels.GovukValidationRequired.GetErrorMessageRequired(this, _publishedValueFallback);
	}
}
