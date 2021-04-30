using System.Data.SqlClient;
using Dapper;
using System.Linq;
using System.IO;
using System.Text;
using static System.Console;

namespace Trainnig.My.English.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            string insertGroupsAndWords = "";

            using (var con = new SqlConnection("Server=(localdb)\\mssqllocaldb;Database=TrainningMyEnglish;Trusted_Connection=True;"))
            {
                var groups = con.Query<Group>("select * from \"group\" order by groupid, name");

                foreach (var group in groups)
                {
                    insertGroupsAndWords += $"insert \"groups\" values (GroupId, Name, Translator) values ({group.GroupId}, '{group.Name}', '{group.Translator}') \n\n";

                    var words = con.Query<Word>($"select * from \"word\" where groupid = {group.GroupId} order by name, groupid").ToList();

                    foreach (var word in words)
                    {
                        insertGroupsAndWords += $"insert \"word\" values (Name, Translator, Groupid) values ('{word.Name}', '{word.Translator}', {word.GroupId}) \n";

                        if (words.Last() == word)
                            insertGroupsAndWords += "\n";
                    }
                }
            }

            string path = @"c:\temp\";
            if (!Directory.Exists(path) || !File.Exists(path + "\\sql.txt"))
            {
                Directory.CreateDirectory(path);
                File.Create(path + "\\sql.txt").Dispose();
            }

            using (FileStream fs = new FileStream(path + "\\sql.txt", FileMode.OpenOrCreate))
            using (StreamWriter sw = new StreamWriter(fs, Encoding.UTF8))
            {
                sw.Write(insertGroupsAndWords);
            }

            WriteLine(insertGroupsAndWords);

            ReadKey();
        }
    }
}
