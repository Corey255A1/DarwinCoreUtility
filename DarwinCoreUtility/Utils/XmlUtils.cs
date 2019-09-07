using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DarwinCoreUtility.Utils
{
    public static class XmlUtils
    {
        public static void Save<T>(T file, string path, XmlSerializerNamespaces xsn=null)
        {
            try
            {
                using (var s = new FileStream(path, FileMode.Create))
                {
                    var xsz = new XmlSerializer(typeof(T));
                    if (xsn != null)
                    {
                        xsz.Serialize(s, file, xsn);
                    }
                    else
                    {
                        xsz.Serialize(s, file);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        public static T Load<T>(string path)
        {
            try
            {
                using (var s = new FileStream(path, FileMode.Open))
                {
                    var xsz = new XmlSerializer(typeof(T));
                    return (T)xsz.Deserialize(s);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            return default;
        }

    }
}
