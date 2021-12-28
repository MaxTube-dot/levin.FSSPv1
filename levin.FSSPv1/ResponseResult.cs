using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace levin.FSSPv1
{
    /// <summary>
    /// Результат запроса 
    /// </summary>
    public class ResponseResult
    {
        [Newtonsoft.Json.JsonProperty("status", Required = Newtonsoft.Json.Required.Always)]
        public string Status { get; set; }

        [Newtonsoft.Json.JsonProperty("code", Required = Newtonsoft.Json.Required.Always)]
        public int Code { get; set; }

        [Newtonsoft.Json.JsonProperty("exception", NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string Exception { get; set; }

        [Newtonsoft.Json.JsonProperty("response", Required = Newtonsoft.Json.Required.Always)]
        public ResponseTask ResponseTask { get; set; }
    }

    /// <summary>
    /// Результат запроса.
    /// </summary>
    public class ResponseTask
    {
        [Newtonsoft.Json.JsonProperty("status", Required = Newtonsoft.Json.Required.Always)]
        public int Status { get; set; }

        [Newtonsoft.Json.JsonProperty("task_start", Required = Newtonsoft.Json.Required.Always)]
        public string TaskStart { get; set; }

        [Newtonsoft.Json.JsonProperty("task_end", Required = Newtonsoft.Json.Required.Always)]
        public string TaskEnd { get; set; }

        [Newtonsoft.Json.JsonProperty("result", Required = Newtonsoft.Json.Required.Always)]
        public ResultTask[] ResultsTask { get; set; }
    }

    /// <summary>
    /// Результат выполнения задачи.
    /// </summary>
    public class ResultTask
    {
        
        [Newtonsoft.Json.JsonProperty("status", Required = Newtonsoft.Json.Required.Always)]
        public int Status { get; set; }

        [Newtonsoft.Json.JsonProperty("query", Required = Newtonsoft.Json.Required.Always)]
        public Query Query { get; set; }

        [Newtonsoft.Json.JsonProperty("result", NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public RecordFssp[] RecordsFssp { get; set; }
    }

    /// <summary>
    /// Параметры поиска записи в БД ФССП, с информацией о типе искомого объекта.
    /// </summary>
    public class Query
    {
        /// <summary>
        /// Тип искомого объекта. 
        /// 1 = физическое лицо,
        /// 2 = юридическое лицо,
        /// 3 = поиск по исполнителному договору.
        /// </summary>
        [Newtonsoft.Json.JsonProperty("type")]
        public int Type { get; set; }

        [Newtonsoft.Json.JsonProperty("_params",NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public QueryParams Params { get; set; }
    }

    /// <summary>
    /// Параметры поиска записи в БД ФССП.
    /// </summary>
    public class QueryParams
    {
        [Newtonsoft.Json.JsonProperty("name", NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string Name { get; set; }

        [Newtonsoft.Json.JsonProperty("region", NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string Region { get; set; }

        [Newtonsoft.Json.JsonProperty("firstname", NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string FirstName { get; set; }

        [Newtonsoft.Json.JsonProperty("lastname", NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string LastName { get; set; }

        [Newtonsoft.Json.JsonProperty("number", NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string Number { get; set; }
    }

    /// <summary>
    /// Результат поиска в базе данных ФССП.
    /// </summary>
    public class RecordFssp
    {
        [Newtonsoft.Json.JsonProperty("name", NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string Name { get; set; }

        [Newtonsoft.Json.JsonProperty("exe_production", NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string ExeProduction { get; set; }

        [Newtonsoft.Json.JsonProperty("details", NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string Details { get; set; }

        [Newtonsoft.Json.JsonProperty("subject", NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string Subject { get; set; }

        [Newtonsoft.Json.JsonProperty("department", NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public object Department { get; set; }

        [Newtonsoft.Json.JsonProperty("bailiff", NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string Bailiff { get; set; }

        [Newtonsoft.Json.JsonProperty("ip_end",NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string IIpEnd { get; set; }
    }






}
