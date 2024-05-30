namespace apilib.Модели
{
    public class Messages
    {
        public string Id { get; set; }
        public string Content { get; set; }
        public User Author { get; set; }
    }
    public class User
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string Discrim { get; set; }
        public string Ava { get; set; }

        public override string ToString()
        {
            return $"{Username}{Discrim}";
        }
    }
}