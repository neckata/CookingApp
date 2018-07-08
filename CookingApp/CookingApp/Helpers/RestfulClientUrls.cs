using CookingApp.Enums;

namespace CookingApp.Helpers
{
    public partial class RestfulClient
    {
        private string FindGetUrl(GetActionMethods method)
        {
            string actionUrl = null;
            switch (method)
            {
                case GetActionMethods.CuisinesFilters:
                    actionUrl = "m-cuisine";
                    break;
                case GetActionMethods.Cuisines:
                    actionUrl = "n-cuisine";
                    break;
                case GetActionMethods.Receipts:
                    actionUrl = "receipts-code/{0}";
                    break;
                case GetActionMethods.RecipeInformation:
                    actionUrl = "receipt-information/{0}";
                    break;
                case GetActionMethods.Cookers:
                    actionUrl = "cuisine-cookers/{0}";
                    break;
                case GetActionMethods.Cooker:
                    actionUrl = "cooker/{0}";
                    break;
                default:
                    break;
            }
            return actionUrl;
        }

        private string FindPostUrl(PostActionMethods method)
        {
            string actionUrl = null;

            switch (method)
            {
                case PostActionMethods.CreateUser:
                    actionUrl = "register";
                    break;
                default:
                    break;
            }
            return actionUrl;
        }
    }
}
