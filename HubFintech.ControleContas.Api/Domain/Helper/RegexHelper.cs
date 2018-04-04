namespace HubFintech.ControleContas.Api.Domain.Helper
{
    public class RegexHelper
    {
        public const string ValidEmail =
                @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z";

        public const string ValidTelefone = @"^((\(([0-9]{2})\))|([0-9]{2}))([ .-]?)(9[0-9]|[0-9])[0-9]{3}([ .-]?)[0-9]{4}$";
        public const string ValidCep = @"[0-9]{5}(-|)[0-9]{3}";
        public const string ValidIpNumber = @"\b\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}\b";
        public const string ValidCodigoTaxa = "TS[\\S]{3}00001";



        public const string HasNumber = @"[0-9]+";
        public const string HasUpperChar = @"[A-Z]+";
        public const string HasMinimum8Chars = @".{8,}";

        public const string MatchAllButNumbers = @"[^$0-9]";
        public const string OnlyNumbers = @"^[0-9]*$";
        public const string FindAllNumbers = @"[0-9]+";

        public const string RemoveSenhaPayload = "\"(senha)\": \".*?\",?";
        public const string RemoveCaracteresEspeciais = @"[^\w\s]|[ºª]";
        public const string RemoveCaracteresParaLinkUrl = @"[/+=]";

    }
}
