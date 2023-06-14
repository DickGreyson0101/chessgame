using Data.Model;

namespace Data.IO
{
    public interface ISaver
    {
        void Save(Container container, string path);
        string Filter();
    }
}