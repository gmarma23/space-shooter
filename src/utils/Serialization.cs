using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;

namespace SpaceShooter.src.utils
{
    public static class Serialization
    {
        public static void Serialize(object obj, string filename)
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(filename, FileMode.OpenOrCreate, FileAccess.Write);
            formatter.Serialize(stream, obj);
            stream.Close();
        }

        public static T Deserialize<T>(string filename)
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(filename, FileMode.Open, FileAccess.Read);
            T obj = (T)formatter.Deserialize(stream);
            stream.Close();
            return obj;
        }
    }
}
