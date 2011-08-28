using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;

namespace FolderTrackGuiTest1
{
    public class Functions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <remarks>
        /// From Chapter 12 (Object Serialization) page 513 of Programming
        /// Microsoft Visual C# 2005: The Base Class Library
        /// </remarks>
        /// <typeparam name="T"></typeparam>
        /// <param name="path"></param>
        /// <param name="obj"></param>
        public static void SerializeToFile<T>(string path, T obj)
        {
            using (FileStream fs = new FileStream(path, FileMode.Create))
            {
                //Create a formatter
                BinaryFormatter bf = new BinaryFormatter(null, new StreamingContext(StreamingContextStates.File));

                // Serialize the object and close the stream
                bf.Serialize(fs, obj);
            }
        }

        public static T DeserializeFromFile<T>(string path)
        {
            // Open the stream for input
            using (FileStream fs = new FileStream(path, FileMode.Open))
            {
                //Create a formatter
                BinaryFormatter bf = new BinaryFormatter(null, new StreamingContext(StreamingContextStates.File));
                //Deserialize the object from the stream
                return (T) bf.Deserialize(fs);
            }
        }
    }
}
