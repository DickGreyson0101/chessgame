using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using Data.Model;

namespace Data.IO
{
    public class BinarySaver : ISaver
    {
        public void Save(Container container, string path)
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None);
            formatter.Serialize(stream, container);
            stream.Close();
        }

        public string Filter()
        {
            return "Chess Binary Save Files (*.we)|*.we";
        }
    }
}