using Data.Model;

namespace Data.IO
{
    public interface ILoader
    {
        Container Load(string path);
        string Filter();
    }
}