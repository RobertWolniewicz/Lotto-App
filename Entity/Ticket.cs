namespace Lotto.Entity
{
    public class Ticket
    {
        public int Id { get; set; }
        public string Result { get; set; }
        public DateTime Created { get; set; }
        public Player Player { get; set; }
        public int PlayerId { get; set; }
    }
}
