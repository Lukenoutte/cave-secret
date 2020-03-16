using System.Xml.Serialization;
using System.IO;

public static class Helper 
{

    public static string Serialize<T>(this T toSerialize)
    {
        XmlSerializer xml = new XmlSerializer(typeof(T));
        StringWriter writer = new StringWriter();
        xml.Serialize(writer, toSerialize);
        return writer.ToString();
    }
    public static T Desirialize<T>(this string toDesirialize)
    {
        XmlSerializer xml = new XmlSerializer(typeof(T));
        StringReader reader = new StringReader(toDesirialize);
        return (T)xml.Deserialize(reader);
    }
}
