namespace BankUI.Endpoints
{
    public static class AccountsEndpoints
    {
        public static string Add = "api/accounts/add";
        public static string Transact = "api/accounts/transact";
        public static string GetAll = "api/accounts/all";

        public static string GetById(int id)
        {
            return $"api/accounts/by-id/{id}";
        }

        public static string GetByAccountId(int id)
        {
            return $"api/accounts/transactions/{id}";
        }

        public static string GetByAccountNumber(string accountNumber)
        {
            return $"api/accounts/by-account-number/{accountNumber}";
        }

        //public static string DeleteById(int id)
        //{
        //    return $"api/accounts/{id}";
        //}
    }
}
