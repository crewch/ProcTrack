namespace DB_Service.Models
{
    public class Dependence: BaseEntity
    {
        public int? First { get; set; }
        public Stage? FirstStage { get; set; }
        public int? Second { get; set; }
        public Stage? SecondStage { get; set; }
    }
}
