namespace DB_Service.Models
{
    public class Edge: BaseEntity
    {
        public int? Start { get; set; }
        public Stage? StartStage { get; set; }
        public int? End { get; set; }
        public Stage? EndStage { get; set; }
    }
}
