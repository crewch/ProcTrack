namespace DB_Service.Tools
{
    public class DateParser
    {
        public static string? TryParse(DateTime? input)
        {
            if (input == null) return null;
            return Parse(input.Value);
        }

        public static string Parse(DateTime input)
        {
            List<string> months = new List<string>() 
            {
                "января",
                "февраля",
                "марта",
                "апреля",
                "мая",
                "июня",
                "июля",
                "августа",
                "сентября",
                "октября",
                "ноября",
                "декабря",
                "124124"
            };

            Dictionary<string, string> days = new Dictionary<string, string>();
            days["Monday"] = "пн";
            days["Tuesday"] = "вт";
            days["Wednesday"] = "ср";
            days["Thursday"] = "чт";
            days["Friday"] = "пт";
            days["Saturday"] = "сб";
            days["Sunday"] = "вс";

            var dayWeek = days[input.DayOfWeek.ToString()];
            var year = input.Year;
            var month = months[input.Month - 1];
            var day = input.Day;
            var hour = input.Hour;
            var minute = input.Minute;

            var res = $"{dayWeek}, {day.ToString("D2")} {month} {year}, {hour.ToString("D2")}:{minute.ToString("D2")}";
            return res;
        }
    }
}
