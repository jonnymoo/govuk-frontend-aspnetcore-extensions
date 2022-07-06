//------------------------------------------------------------------------------
// <auto-generated>
//   This code was generated by a tool.
//
//    Umbraco.ModelsBuilder.Embedded v10.0.0+e3f4b86
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
	/// <summary>Date input settings</summary>
	[PublishedModel("govukDateInputSettings")]
	public partial class GovukDateInputSettings : PublishedElementModel, IGovukCssClasses, IGovukGrid, IGovukGridColumnClasses, IGovukLegendIsPageHeading, IGovukModelProperty, IGovukValidationRequired
	{
		// helpers
#pragma warning disable 0109 // new is redundant
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "10.0.0+e3f4b86")]
		public new const string ModelTypeAlias = "govukDateInputSettings";
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "10.0.0+e3f4b86")]
		public new const PublishedItemType ModelItemType = PublishedItemType.Content;
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "10.0.0+e3f4b86")]
		[return: global::System.Diagnostics.CodeAnalysis.MaybeNull]
		public new static IPublishedContentType GetModelContentType(IPublishedSnapshotAccessor publishedSnapshotAccessor)
			=> PublishedModelUtility.GetModelContentType(publishedSnapshotAccessor, ModelItemType, ModelTypeAlias);
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "10.0.0+e3f4b86")]
		[return: global::System.Diagnostics.CodeAnalysis.MaybeNull]
		public static IPublishedPropertyType GetModelPropertyType<TValue>(IPublishedSnapshotAccessor publishedSnapshotAccessor, Expression<Func<GovukDateInputSettings, TValue>> selector)
			=> PublishedModelUtility.GetModelPropertyType(GetModelContentType(publishedSnapshotAccessor), selector);
#pragma warning restore 0109

		private IPublishedValueFallback _publishedValueFallback;

		// ctor
		public GovukDateInputSettings(IPublishedElement content, IPublishedValueFallback publishedValueFallback)
			: base(content, publishedValueFallback)
		{
			_publishedValueFallback = publishedValueFallback;
		}

		// properties

		///<summary>
		/// CSS classes
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "10.0.0+e3f4b86")]
		[global::System.Diagnostics.CodeAnalysis.MaybeNull]
		[ImplementPropertyType("cssClasses")]
		public virtual string CssClasses => global::Umbraco.Cms.Web.Common.PublishedModels.GovukCssClasses.GetCssClasses(this, _publishedValueFallback);

		///<summary>
		/// CSS classes for row
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "10.0.0+e3f4b86")]
		[global::System.Diagnostics.CodeAnalysis.MaybeNull]
		[ImplementPropertyType("cssClassesForRow")]
		public virtual string CssClassesForRow => global::Umbraco.Cms.Web.Common.PublishedModels.GovukGrid.GetCssClassesForRow(this, _publishedValueFallback);

		///<summary>
		/// Column size: Defaults to 'two-thirds' if both column size properties are left blank.
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "10.0.0+e3f4b86")]
		[global::System.Diagnostics.CodeAnalysis.MaybeNull]
		[ImplementPropertyType("columnSize")]
		public virtual string ColumnSize => global::Umbraco.Cms.Web.Common.PublishedModels.GovukGridColumnClasses.GetColumnSize(this, _publishedValueFallback);

		///<summary>
		/// Column size (from desktop)
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "10.0.0+e3f4b86")]
		[global::System.Diagnostics.CodeAnalysis.MaybeNull]
		[ImplementPropertyType("columnSizeFromDesktop")]
		public virtual string ColumnSizeFromDesktop => global::Umbraco.Cms.Web.Common.PublishedModels.GovukGridColumnClasses.GetColumnSizeFromDesktop(this, _publishedValueFallback);

		///<summary>
		/// CSS classes for column
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "10.0.0+e3f4b86")]
		[global::System.Diagnostics.CodeAnalysis.MaybeNull]
		[ImplementPropertyType("cssClassesForColumn")]
		public virtual string CssClassesForColumn => global::Umbraco.Cms.Web.Common.PublishedModels.GovukGridColumnClasses.GetCssClassesForColumn(this, _publishedValueFallback);

		///<summary>
		/// The legend is the page heading
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "10.0.0+e3f4b86")]
		[ImplementPropertyType("legendIsPageHeading")]
		public virtual bool LegendIsPageHeading => global::Umbraco.Cms.Web.Common.PublishedModels.GovukLegendIsPageHeading.GetLegendIsPageHeading(this, _publishedValueFallback);

		///<summary>
		/// Model property: The name of the property on the view model being bound to in the code.
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "10.0.0+e3f4b86")]
		[global::System.Diagnostics.CodeAnalysis.MaybeNull]
		[ImplementPropertyType("modelProperty")]
		public virtual object ModelProperty => global::Umbraco.Cms.Web.Common.PublishedModels.GovukModelProperty.GetModelProperty(this, _publishedValueFallback);

		///<summary>
		/// Required: Sets the message displayed if the field is set by the code to be required.
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "10.0.0+e3f4b86")]
		[global::System.Diagnostics.CodeAnalysis.MaybeNull]
		[ImplementPropertyType("errorMessageRequired")]
		public virtual string ErrorMessageRequired => global::Umbraco.Cms.Web.Common.PublishedModels.GovukValidationRequired.GetErrorMessageRequired(this, _publishedValueFallback);
	}
}
