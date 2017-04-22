using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQCommonLib
{
    internal class BytesSerializer<T> where T : class
    {
        public byte[] ObjectToByteArray(T obj)
        {
            if (obj == null)
                return null;

            BinaryFormatter bf = new BinaryFormatter();
            MemoryStream ms = new MemoryStream();

            byte[] result = null;
            try
            {
                bf.Serialize(ms, obj);
                result = ms.ToArray();
            }
            catch { }
            finally
            {
                ms.Dispose();
            }

            return result;
        }

        public T ByteArrayToObject(byte[] arrBytes)
        {
            MemoryStream memStream = new MemoryStream();
            BinaryFormatter binForm = new BinaryFormatter();
            T result = null;

            try
            {
                memStream.Write(arrBytes, 0, arrBytes.Length);
                memStream.Seek(0, SeekOrigin.Begin);
                result = (T)binForm.Deserialize(memStream);
            }
            catch { }
            finally
            {
                memStream.Dispose();
            }
            
            return result;
        }
    }
}
