using System;
using System.Net;
using System.Net.Http;
using Newtonsoft.Json.Linq;

namespace Fssp
{

    public class SearchClient
    {

        public string BaseUrl { get; set; } = "https://api-ip.fssp.gov.ru/api/v1.0/";




        /// <summary> 
        /// Отправить запрос на поиск физического лица
        /// </summary>
        /// <param name="token">Ключ доступа к API</param>
        /// <param name="region">Номер региона</param>
        /// <param name="firstname">Имя физического лица</param>
        /// <param name="secondname">Отчество физического лица</param>
        /// <param name="lastname">Фамилия физического лица</param>
        /// <param name="birthdate">Дата рождения физического лица, в формате dd.mm.YYYY</param>
        /// <returns>При успешном выполнении запроса пользователю вернётся Task UUID, по которому он сможет получить статус и результат выполнения задачи</returns>
        public string Physical(string token, int region, string firstName, string secondName, string lastName, string birthDate)
        {
            if (String.IsNullOrEmpty(token))
                throw new ArgumentException("token");

            if (region > 1 && region > 99)
                throw new ArgumentException("region");

            if (String.IsNullOrEmpty(firstName))
                throw new ArgumentException("firstName");

            if (String.IsNullOrEmpty(lastName))
                throw new ArgumentException("lastName");

            if (String.IsNullOrEmpty(birthDate))
                throw new ArgumentException("birthDate");


            var urlBuilder_ = new System.Text.StringBuilder();

            urlBuilder_.Append(BaseUrl).Append("search/physical?");

            urlBuilder_.Append(Uri.EscapeDataString("token") + "=").Append(Uri.EscapeDataString(token)).Append("&");

            urlBuilder_.Append(Uri.EscapeDataString("region") + "=").Append(region).Append("&");

            urlBuilder_.Append(Uri.EscapeDataString("firstname") + "=").Append(Uri.EscapeDataString(firstName)).Append("&");

            if (secondName != null)
            {
                urlBuilder_.Append(System.Uri.EscapeDataString("secondname") + "=").Append(Uri.EscapeDataString(secondName)).Append("&");
            }

            urlBuilder_.Append(System.Uri.EscapeDataString("lastname") + "=").Append(Uri.EscapeDataString(lastName)).Append("&");

            urlBuilder_.Append(System.Uri.EscapeDataString("birthdate") + "=").Append(Uri.EscapeDataString(birthDate));

            try
            {

            
            using (var client = new WebClient())
            {

                var url_ = urlBuilder_.ToString();

                var responseText_ = client.DownloadString(url_);

                JObject json = JObject.Parse(responseText_);

                var code = int.Parse(json["code"].ToString());

                if (code == 0)
                {


                    string task = json["response"]["task"].ToString();

                    return task;
                }

                if (code == 401)
                {
                    throw new ApiException("Token is invalid!", code, responseText_);
                }
                else
                {
                    throw new ApiException("The HTTP status code of the response was not expected!", code, responseText_);
                }
            }
            }
            catch (WebException ex)
            {
              
                throw new ApiException(ex.Message);
               
            }

        }


        public string Group(GroupRequest query)
        {
            if (query == null)
                throw new System.ArgumentException("query");

            var urlBuilder_ = new System.Text.StringBuilder();

            urlBuilder_.Append(BaseUrl != null ? BaseUrl.TrimEnd('/') : "").Append("/search/group");

            using (var request_ = new HttpRequestMessage())
            {

                var a = Newtonsoft.Json.JsonConvert.SerializeObject(query);

                var content_ = new StringContent(a);

                content_.Headers.ContentType = System.Net.Http.Headers.MediaTypeHeaderValue.Parse("application/json");

                request_.Content = content_;

                request_.Method = new System.Net.Http.HttpMethod("POST");

                request_.Headers.Accept.Add(System.Net.Http.Headers.MediaTypeWithQualityHeaderValue.Parse("application/json"));

                WebClient webClient = new WebClient();

                HttpClient client_ = new HttpClient();

                var url_ = urlBuilder_.ToString();

                request_.RequestUri = new System.Uri(url_, System.UriKind.RelativeOrAbsolute);

                var response_ = client_.SendAsync(request_, System.Net.Http.HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(true).GetAwaiter().GetResult().Content.ReadAsStringAsync().Result;

                JObject json = JObject.Parse(response_);

                var code = int.Parse(json["code"].ToString());

                if (code == 0)
                {
                    string task = json["response"]["task"].ToString();

                    return task;
                }

                if (code == 401)
                {
                    throw new ApiException("Token is invalid!", code, response_);
                }
                else
                {
                    throw new ApiException("The HTTP status code of the response was not expected!", code, response_);
                }

            }

        }
    }


    public class GroupRequest
    {
        [Newtonsoft.Json.JsonProperty("token", NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string Token { get; set; }

        [Newtonsoft.Json.JsonProperty("request", NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public Request[] Request { get; set; }
    }

    public class Request
    {
        [Newtonsoft.Json.JsonProperty("type", NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public int Type { get; set; }

        [Newtonsoft.Json.JsonProperty("_params", NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public Params Params { get; set; }
    }

    public class Params
    {
        [Newtonsoft.Json.JsonProperty("firstname", NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string FirstName { get; set; }

        [Newtonsoft.Json.JsonProperty("lastname", NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string LastName { get; set; }

        [Newtonsoft.Json.JsonProperty("secondname", NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string SecondName { get; set; }

        [Newtonsoft.Json.JsonProperty("region", NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string Region { get; set; }

        [Newtonsoft.Json.JsonProperty("birthdate", NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string BirthDate { get; set; }

        [Newtonsoft.Json.JsonProperty("name", NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string Name { get; set; }

        [Newtonsoft.Json.JsonProperty("address", NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]

        public string Address { get; set; }

        [Newtonsoft.Json.JsonProperty("number", NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string Number { get; set; }
    }


}
