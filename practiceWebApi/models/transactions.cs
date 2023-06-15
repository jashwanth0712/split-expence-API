namespace practiceWebApi.models
{
    public class transactions
    {
        public int Transaction_id { get; set; }
        public int group_id { get; set; }
        public string? sender { get; set; }
        public string? recipient { get; set; }
        public int amount { get; set; }
    }
}
