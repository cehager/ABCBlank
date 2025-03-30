namespace BankUI.Endpoints
{
    public static class AccountHoldersEndpoints
    {
        public static string Add = "api/accountholders/add";
        public static string Update = "api/accountholders/update";
        public static string Delete = "api/accountholders";
        public static string GetAll = "api/accountholders/all";

        public static string GetById(int id)
        {
            return $"api/accountholders/{id}";
        }

        public static string DeleteById(int id)
        {
            return $"api/accountholders/{id}";
        }
    }
}
