using System.Collections.Generic;

namespace CavemanTools.Data
{
    public class JsonStruct
    {
        public const string MessageKey = "Message";
        public string status { get; set; }
        public Dictionary<string, object> data { get; private set; }
      

        public object this[string key]
        {
            get { return data[key]; }
            set { data[key] = value; }
        }

        public bool isOk
        {
            get { return status == "Ok"; }
        }


        public bool hasError
        {
            get { return status == "Error"; }
        }

        /// <summary>
        /// Sets the status and inserts a message.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="status"></param>
        public JsonStruct(string message,JsonStatus status = JsonStatus.Ok):this(status)
        {
            this[MessageKey] = message;
        }
        public JsonStruct(JsonStatus status=JsonStatus.Ok)
        {
            this.status = status.ToString();
            data= new Dictionary<string, object>();            
        }

        public static JsonStruct Error(string message)
        {
            return new JsonStruct(message,JsonStatus.Error);
        }

    }

}