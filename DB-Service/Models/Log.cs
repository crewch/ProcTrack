namespace DB_Service.Models
{
    public class Log: BaseEntity
    {
        public string? Table { get; set; } // название таблицы в которой происходит изменние
        public string? Field { get; set; } 
        public string? Operation { get; set; } // update / insert / delete
        public string? LogId { get; set; } // Id записи которую меняем
        public string? Old { get; set; } // старое значение 
        public string? New { get; set; } // новое значение
        public string? Author { get; set; } // чел
        public DateTime? CreatedAt { get; set; } // время изменения 
    }

    // писать на все запросы на изменение / создание
}
