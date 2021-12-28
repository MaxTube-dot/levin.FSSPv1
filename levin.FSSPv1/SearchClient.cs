using System;
using System.Net;
using System.Net.Http;
using Newtonsoft.Json.Linq;

namespace levin.FSSPv1
{
    partial class Program
    {
        public class SearchClient
        {
            private HttpClient _httpClient;

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

        }
    }
}
