// using System;
// using System.IO;
// using System.Xml.Serialization;
// using Newtonsoft.Json;
// using UnityEngine;
// using Newtonsoft.Json.Linq;

// namespace MyUnityHelpers {




//     public class VectorConverter : JsonConverter {
//         public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) {
//             var vector = (Vector3)value;
//             var t = new JObject();
//             t.Add("x", vector.x);
//             t.Add("y", vector.y);
//             t.Add("z", vector.z);
//             t.WriteTo(writer);
//         }

//         public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer) {
//             reader.Read();
//             float x = (float)reader.ReadAsDouble();
//             reader.Read();
//             float y = (float)reader.ReadAsDouble();
//             reader.Read();
//             float z = (float)reader.ReadAsDouble();
//             reader.Read();
//             return new Vector3(x, y, z);
//         }

//         public override bool CanConvert(Type objectType) {
//             return typeof(Vector3).IsAssignableFrom(objectType);
//         }
//     }

//     public static class SerializationHelper {
//         public static T DeserializeXml<T>(this string toDeserialize) {
//             XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
//             using (StringReader textReader = new StringReader(toDeserialize)) {
//                 return (T)xmlSerializer.Deserialize(textReader);
//             }
//         }

//         public static string SerializeXml<T>(this T toSerialize) {
//             XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
//             using (StringWriter textWriter = new StringWriter()) {
//                 xmlSerializer.Serialize(textWriter, toSerialize);
//                 return textWriter.ToString();
//             }
//         }

//         public static string SerializeJson<T>(this T obj, JsonSerializerSettings? settings = null) {
//             return JsonConvert.SerializeObject(obj, settings);
//         }
//         public static T DeserializeJson<T>(this string json, JsonSerializerSettings? settings = null) {
//             return JsonConvert.DeserializeObject<T>(json, settings);
//         }
//     }

// }