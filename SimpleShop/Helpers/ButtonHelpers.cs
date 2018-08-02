using System.Web.Mvc;

namespace SimpleShop.Helpers
{
	public  enum ButtonSize
	{
		large,
		small
	}

	public static class ButtonHelpers
	{

		public static MvcHtmlString Button(this HtmlHelper helper, ActionResult action, string text)
		{
			var urlHelper = new UrlHelper(helper.ViewContext.RequestContext);
			var url = urlHelper.Action(action);
			var html = $"<a class='waves-effect waves-light btn' href='{url}'>{text}</a>";
			
			return new MvcHtmlString(html);
		}

		public static MvcHtmlString Button(this HtmlHelper helper, ActionResult action, string text,ButtonSize size)
		{
			var urlHelper = new UrlHelper(helper.ViewContext.RequestContext);
			var url = urlHelper.Action(action);
			var html = $"<a class='waves-effect waves-light btn' href='{url}'>{text}</a>";

			return new MvcHtmlString(html);
		}

		public static MvcHtmlString Button(this HtmlHelper helper, ActionResult action, string text,string materialIconName)
		{
			var urlHelper = new UrlHelper(helper.ViewContext.RequestContext);
			var url = urlHelper.Action(action);
			var html = $"<a class='waves-effect waves-light btn btn-large' href='{url}'><i class='material-icons right'>{materialIconName}</i>{text}</a>";

			return new MvcHtmlString(html);
		}

		public static MvcHtmlString Button(this HtmlHelper helper, ActionResult action, string text, string materialIconName,ButtonSize size)
		{
			var urlHelper = new UrlHelper(helper.ViewContext.RequestContext);
			var url = urlHelper.Action(action);
			var html = $"<a class='waves-effect waves-light btn btn-{size}' href='{url}'><i class='material-icons right'>{materialIconName}</i>{text}</a>";

			return new MvcHtmlString(html);
		}
		public static MvcHtmlString ButtonSubmit(this HtmlHelper helper, string text, string materialIconName = "send", ButtonSize size = ButtonSize.large)
		{
			var urlHelper = new UrlHelper(helper.ViewContext.RequestContext);
			var html = $"<button class='waves-effect waves-light btn btn-{size}' type='submit'><i class='material-icons right'>{materialIconName}</i>{text}</button>";

			return new MvcHtmlString(html);
		}

	}
}