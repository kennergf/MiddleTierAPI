namespace MiddleTier.API.Response
{
    public static class ResponseMessage
    {
        public const string COMPANY_CREATED = "101 - Company Created. ";

        public const string ISIN_DUPLICATED = "401 - ISIN Must be Unique. ";

        public const string FIRST_TWO_CHARACTER_ISIN_MUST_BE_LETTERS = "402 - The first two characters of an ISIN must be letters. ";

        public const string ID_PROVIDED_DOES_NOT_MATCH_COMPANY_ID = "403 - The Id Providede does not Match the Company Id. ";

        public const string COMPANY_UPDATED = "102 - Company Updated. ";

        public const string COMPANY_RETRIEVED = "103 - Company Retrieved. ";

        public const string COMPANY_NOT_FOUND = "404 - Comany Not Found. ";

        public const string COULD_NOT_RETRIEVE_INFORMATION = "502 - Could Not Retrieve Information Requested. ";

        public const string COULD_NOT_PERFORM_OPERATION = "503 - Could Not Perform Operation Requested. ";


        public const string ERROR_PROCESSING_REQUEST = "501 - Error Processing Request. ";
    }
}