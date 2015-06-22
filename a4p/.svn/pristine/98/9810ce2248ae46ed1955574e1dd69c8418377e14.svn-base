using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using ADOPets.Web.Common.Helpers;
using Model;

namespace ADOPets.Web.Common.Extensions
{
    public static class HtmlHelperExtensions
    {
        #region Enums

        public static IHtmlString EnumDropDownListFor<TModel, TEnum>(this HtmlHelper<TModel> html,
            Expression<Func<TModel, TEnum>> expression, string optionalLabel = null, object htmlAttributes = null)
        {
            var metadata = ModelMetadata.FromLambdaExpression(expression, html.ViewData);

            var enumType = Nullable.GetUnderlyingType(metadata.ModelType) ?? metadata.ModelType;

            var enumValues = Enum.GetValues(enumType).Cast<TEnum>().ToList<TEnum>();

            var orderList = enumValues.OrderBy(e => e.ToString() == "Other").ThenBy(e => e);//.OrderBy(e => e.ToString());// EnumHelper.GetResourceValueForEnumValue(a));

            var items = from enumValue in orderList
                        select new SelectListItem
                        {
                            Text = EnumHelper.GetResourceValueForEnumValue(enumValue),
                            Value = enumValue.ToString(),
                            Selected = enumValue.Equals(metadata.Model)
                        };

            items = items.OrderBy(a => a.Text);
            return html.DropDownListFor(expression, items, optionalLabel, htmlAttributes);
        }

        public static IHtmlString EnumDropDownListFor<TModel, TEnum>(this HtmlHelper<TModel> html,
            Expression<Func<TModel, TEnum>> expression, List<TEnum> excludedItems, string optionalLabel = null, object htmlAttributes = null)
        {
            var metadata = ModelMetadata.FromLambdaExpression(expression, html.ViewData);

            var enumType = Nullable.GetUnderlyingType(metadata.ModelType) ?? metadata.ModelType;

            var enumValues = Enum.GetValues(enumType).Cast<TEnum>().Where(i => !excludedItems.Contains(i)).OrderBy(a => EnumHelper.GetResourceValueForEnumValue(a)).ToList();

            var orderList = enumValues.OrderBy(e => e.ToString() == "Other").ThenBy(e => e);
            var sortedList = orderList.OrderBy(a => EnumHelper.GetResourceValueForEnumValue(a));

            var items = from enumValue in sortedList
                        select new SelectListItem
                        {
                            Text = EnumHelper.GetResourceValueForEnumValue(enumValue),
                            Value = enumValue.ToString(),
                            Selected = enumValue.Equals(metadata.Model)
                        };


            return html.DropDownListFor(expression, items, optionalLabel, htmlAttributes);
        }

        public static IHtmlString EnumDropDownList(this HtmlHelper html, Type type, string name, List<string> excludedItems, object selected = null, string optionalLabel = null, object htmlAttributes = null)
        {
            var enumValues = Enum.GetValues(type).Cast<object>().OrderBy(a => EnumHelper.GetResourceValueForEnumValue(a));

            var enumVal = enumValues.Where(i => !excludedItems.Contains(i.ToString()));

            var items = from enumValue in enumVal
                        select new SelectListItem
                        {
                            Text = EnumHelper.GetResourceValueForEnumValue(enumValue),
                            Value = ((int)enumValue).ToString(),
                            Selected = enumValue.Equals(selected)
                        };
              items = items.OrderBy(a => EnumHelper.GetResourceValueForEnumValue(a));
            return html.DropDownList(name, items, optionalLabel, htmlAttributes);
        }


        public static IHtmlString EnumDropDownList(this HtmlHelper html, Type type, string name, object selected = null, string optionalLabel = null, object htmlAttributes = null)
        {
            var enumValues = Enum.GetValues(type).Cast<object>().OrderBy(a => EnumHelper.GetResourceValueForEnumValue(a));
            var items = from enumValue in enumValues
                        select new SelectListItem
                        {
                            Text = EnumHelper.GetResourceValueForEnumValue(enumValue),
                            Value = ((int)enumValue).ToString(),
                            Selected = enumValue.Equals(selected)
                        };
            //   items = items.OrderBy(a => EnumHelper.GetResourceValueForEnumValue(a));
            return html.DropDownList(name, items, optionalLabel, htmlAttributes);
        }

        public static IHtmlString EnumDisplayFor<TModel, TEnum>(this HtmlHelper<TModel> html, Expression<Func<TModel, TEnum>> expression)
        {
            var metadata = ModelMetadata.FromLambdaExpression(expression, html.ViewData);

            var enumType = Nullable.GetUnderlyingType(metadata.ModelType) ?? metadata.ModelType;

            var enumValues = Enum.GetValues(enumType).Cast<object>();

            var enumValue = enumValues.FirstOrDefault(e => e.Equals(metadata.Model));

            var text = enumValue == null ? string.Empty : EnumHelper.GetResourceValueForEnumValue(enumValue);

            return new HtmlString(text);
        }

        public static IHtmlString EnumDisplay<TEnum>(this HtmlHelper html, TEnum enumValue)
        {
            var text = EnumHelper.GetResourceValueForEnumValue(enumValue);

            return new HtmlString(text);
        }

        #endregion


        #region Metas

        public static IHtmlString MetaAcceptLanguage(this HtmlHelper html)
        {
            var acceptLanguage = HttpUtility.HtmlAttributeEncode(Thread.CurrentThread.CurrentUICulture.ToString());
            return new HtmlString(String.Format("<meta name=\"accept-language\" content=\"{0}\" />", acceptLanguage));
        }

        public static IHtmlString GetDomainName(this HtmlHelper html)
        {
            var acceptDomain = HttpUtility.HtmlAttributeEncode(DomainHelper.GetDomain().ToString());
            return new HtmlString(String.Format("<meta name=\"accept-domain\" content=\"{0}\" />", acceptDomain));
        }


        public static IHtmlString GetCurrencyName(this HtmlHelper html)
        {
            var acceptCurrency = HttpUtility.HtmlAttributeEncode(DomainHelper.GetCurrency());
            return new HtmlString(String.Format("<meta name=\"accept-currency\" content=\"{0}\" />", acceptCurrency));
        }

        #endregion


        #region File Extension Icon

        public static IHtmlString CreateSpanFromFileExtension(this HtmlHelper helper, string path)
        {
            var extNew = Path.GetExtension(path);
            var ext = extNew.ToLower();

            var className = "flaticon-txt";

            if (!string.IsNullOrEmpty(ext))
            {
                if (ext.Contains("jpg") || ext.Contains("png") || ext.Contains("gif") || ext.Contains("jpeg"))
                {
                    className = "flaticon-jpg2";
                }
                else if (ext.Contains("txt"))
                {
                    className = "flaticon-txt";
                }
                else if (ext.Contains("pdf"))
                {
                    className = "flaticon-pdf17";
                }
                else if (ext.Contains("doc"))
                {
                    className = "flaticon-doc";
                }
                else if (ext.Contains("xml"))
                {
                    className = "flaticon-xml6";
                }
                else if (ext.Contains("rar") || ext.Contains("zip"))
                {
                    className = "flaticon-zip5";
                }
                else if (ext.Contains("ppt"))
                {
                    className = "flaticon-ppt2";
                }
                else if (ext.Contains("xls"))
                {
                    className = "flaticon-xls2";
                }

            }

            var text = className != "flaticon-jpg2" ? string.Format("<i class=\"{0}\"></i>", className) : string.Format("<span class=\"{0}\"></span>", className);

            return new HtmlString(text);
        }

        public static IHtmlString CreateDivFromFileExtension(this HtmlHelper helper, string path)
        {
            var extNew = Path.GetExtension(path);
            var ext = extNew.ToLower();

            var className = "fa  fa-file-text-o";

            if (!string.IsNullOrEmpty(ext))
            {
                if (ext.Contains("jpg") || ext.Contains("png") || ext.Contains("gif") || ext.Contains("jpeg"))
                {
                    className = "fa fa-file-photo-o (alias)";
                }
                else if (ext.Contains("txt"))
                {
                    className = "fa  fa-file-text-o";
                }
                else if (ext.Contains("pdf"))
                {
                    className = "fa fa-file-pdf-o";
                }
                else if (ext.Contains("doc"))
                {
                    className = "fa  fa-file-word-o";
                }
                else if (ext.Contains("xls"))
                {
                    className = "fa fa-file-excel-o";
                }

            }

            var text = className != "fa fa-file-photo-o (alias)" ? string.Format("<i class=\"{0}\"></i>", className) : string.Format("<span class=\"{0}\"></span>", className);

            return new HtmlString(text);
        }

        #endregion

        public static IHtmlString RenderSuccessMessage(this HtmlHelper htmlHelper, string id, object message)
        {
            if (message != null)
            {
                return new HtmlString(string.Format("<input id=\"{0}\" value=\"{1}\" type=\"hidden\" />", id, message));
            }
            return null;
        }


        public static MvcHtmlString LabelRequiredFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, object htmlAttributes = null)
        {
            var customAttributes = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);
            if (customAttributes.ContainsKey("class"))
            {
                customAttributes["class"] = customAttributes["class"] + " required";
            }
            else
            {
                customAttributes["class"] = "required";
            }

            return htmlHelper.LabelFor(expression, customAttributes);
        }
    }
}