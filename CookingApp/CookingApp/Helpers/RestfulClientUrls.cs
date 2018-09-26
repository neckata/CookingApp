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
                case GetActionMethods.RecipeFullInformation:
                    actionUrl = "receipt-full-information/{0}";
                    break;
                case GetActionMethods.Cookers:
                    actionUrl = "cuisine-cookers/{0}";
                    break;
                case GetActionMethods.Cooker:
                    actionUrl = "cooker/{0}";
                    break;
                case GetActionMethods.TimeTable:
                    actionUrl = "cooker-schedule/{0}";
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
                case PostActionMethods.Login:
                    actionUrl = "login";
                    break;
                case PostActionMethods.Logout:
                    actionUrl = "logout";
                    break;
                case PostActionMethods.SaveUserInformation:
                    actionUrl = "saveUserInformation";
                    break;
                case PostActionMethods.SaveAddress:
                    actionUrl = "saveAddress";
                    break;
                case PostActionMethods.SaveCooker:
                    actionUrl = "saveCookerInformation";
                    break;
                case PostActionMethods.Order:
                    actionUrl = "place-order";
                    break;
                case PostActionMethods.Orders:
                    actionUrl = "list-orders";
                    break;
                case PostActionMethods.VoteOrder:
                    actionUrl = "order-rating";
                    break;
                case PostActionMethods.DeleteAddress:
                    actionUrl = "deleteAddress";
                    break;
                case PostActionMethods.GetAddresses:
                    actionUrl = "listAddresses";
                    break;
                case PostActionMethods.ReplyOrder:
                    actionUrl = "reply-order";
                    break;
                default:
                    break;
            }
            return actionUrl;
        }
    }
}
