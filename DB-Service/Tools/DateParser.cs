namespace DB_Service.Tools
{
    public class DateParser
    {
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

            List<string> days = new List<string>()
            {
                "пн",
                "вт",
                "ср",
                "чт",
                "пт",
                "сб",
                "вс",
                "12412"
            };

            var dayWeek = days[(int)input.DayOfWeek - 1];
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
