namespace Minos.Domain.Define
{
    public class Message
    {
        public static string RequiredErrorMessage
        {
            get
            {
                return "{0}을 입력해 주십시오.";
            }
        }

        public static string RangeErrorMessage
        {
            get
            {
                return "{0}의 범위는 {1}~{2}의 자릿수를 입력하여 주십시오";
            }
        }

        public static string FixedErrorMessage
        {
            get
            {
                return "{1}자리 길이값의 데이터만 입력 가능합니다.";
            }
        }

        public static string NotOveredErrorMessage
        {
            get
            {
                return "{1}자리 이상의 데이터는 입력이 불가능합니다.";
            }
        }

        public static string DataTypeErrorMessage
        {
            get
            {
                return "{1} 형식에 맞는 정보를 입력하여 주십시오.";
            }
        }
    }
}
