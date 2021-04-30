
namespace Trainnig.My.English.Console
{
    public class Word
    {
        public int WordId { get; set; }
        public string Name { get; set; }
        public string Translator { get; set; }
        public bool Arquived { get; set; }
        public int UserId { get; set; }
        public int? GroupId { get; set; }
    }
}
