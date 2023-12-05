namespace DB_Service.Dtos
{
    public class LinkDto
    {
        public List<Tuple<int, int>> Edges { get; set; }
        public List<Tuple<int, int>>? Dependences { get; set; }
    }
}
