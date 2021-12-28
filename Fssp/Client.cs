using System;
using System.Net;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Fssp
{

   

    public class Client
    {
       

        public string BaseUrl { get; set; } = "https://api-ip.fssp.gov.ru/api/v1.0/";

       

        /// <summary>
        /// Отправить запрос на получение статуса выполнения задачи
        /// </summary>
        /// <param name="token">Ключ доступа к API</param>
        /// <param name="task">Task UUID, выданный при успешном выполнении запроса</param>
        /// <returns>Объект StatusTask содержаший информацию о статусе задачи.</returns>

        public StatusTask GetStatus(string token, string task)
        {
            if (String.IsNullOrEmpty(token))
                throw new ArgumentException("token");

            if (String.IsNullOrEmpty(task))
                throw new ArgumentException("task");


            var urlBuilder_ = new System.Text.StringBuilder();

            urlBuilder_.Append(BaseUrl).Append("status?");

            urlBuilder_.Append(("token") + "=").Append((token)).Append("&");

            urlBuilder_.Append(("task") + "=").Append((task));


            using (var client = new WebClient())
            {

                var url_ = urlBuilder_.ToString();

                var responseText_ = client.DownloadString(url_);

                JObject json = JObject.Parse(responseText_);

                var code =  int.Parse(json["code"].ToString()); 


                if (code == 0)
                {
                    
                    string statusString = json["response"]["status"].ToString();

                    string[] progress = json["response"]["progress"].ToString().Split(" of ");


                    var statusTask = (StatusTaskEnum)int.Parse(statusString);

                    string tasksDone = progress[0];

                    string totalTasks = progress[1];

                    return new StatusTask { Status = statusTask, TasksDone = int.Parse(tasksDone), TotalTasks = int.Parse(totalTasks) };

                }
                else if (code == 400)
                {
                   
                    throw new ApiException("Task not exist!", code, responseText_);
                }
                else if (code == 401)
                {
                    throw new ApiException("Token is invalid!", code, responseText_);
                }
                else
                {
                    throw new ApiException("The HTTP status code of the response was not expected!", code, responseText_);
                }
            }
        }


        /// <summary>
        /// Отправить запрос на получение результата выполнения задачи
        /// </summary>
        /// <param name="token">Ключ доступа к API</param>
        /// <param name="task">Task UUID, выданный при успешном выполнении запроса</param>
        /// <returns>Получаем объект класса ResponseTask.</returns>
        public ResponseTask GetResults(string token, string task)
        {
            if (String.IsNullOrEmpty(token))
                throw new ArgumentException("token");

            if (String.IsNullOrEmpty(task))
                throw new ArgumentException("task");

            var urlBuilder_ = new System.Text.StringBuilder();

            urlBuilder_.Append(BaseUrl != null ? BaseUrl.TrimEnd('/') : "").Append("/result?");

            urlBuilder_.Append(System.Uri.EscapeDataString("token") + "=").Append(System.Uri.EscapeDataString(token)).Append("&");

            urlBuilder_.Append(System.Uri.EscapeDataString("task") + "=").Append(System.Uri.EscapeDataString(task));

            using (var client = new WebClient())
            {

                var url_ = urlBuilder_.ToString();

                var responseText_ = client.DownloadString(url_);

                JObject json = JObject.Parse(responseText_);

                var code = int.Parse(json["code"].ToString());

                if (code == 0)
                {
                    var responseResult = JsonConvert.DeserializeObject<ResponseResult>(responseText_);

                    return responseResult.ResponseTask;

                }
                else if (code == 400)
                {
                    throw new ApiException("Task not exist!", code, responseText_);
                }
                else if (code == 401)
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
