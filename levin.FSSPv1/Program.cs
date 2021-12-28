using System.IO;
using System.Net.Http;

using Newtonsoft.Json;

namespace levin.FSSPv1
{
    partial class Program
    {
        static void Main(string[] args)
        {
            SearchClient searchClient = new SearchClient();

            string token = "DIWmQoRIZ5sD";

            var task = searchClient.Physical(token: token, region: 46, firstName: "Илья", lastName: "Левин", secondName: "Владимирович", birthDate: "01.11.1999");


            //var task = "86b16aaa-ce53-4f62-91d4-a730bc868328"; //с готовым результатом.


            System.Console.WriteLine("Запрос отправлен");

            Client client = new Client();

            while (true)
            {
                var status = client.GetStatus(token, task);

                System.Console.WriteLine(status.ToString());

                if (status.Status == StatusTaskEnum.Ready)
                {
                    break;
                }

                /// Тайм-аут задежки между запросами.
                System.Threading.Thread.Sleep(60000);
            }

            var result = client.GetResults(token, task);

            System.Console.WriteLine($"Время начала {result.TaskStart}");

            System.Console.WriteLine($"Время начала {result.TaskEnd}");

            foreach (var resultTask in result.ResultsTask)
            {
                System.Console.WriteLine($"Тип запроса {resultTask.Query.Type}");

                if (resultTask.RecordsFssp.Length==0)
                {
                    System.Console.WriteLine($"Записей нет.");
                }
                else
                {
                foreach (var RecordFssp in resultTask.RecordsFssp) //не уверен в смысловой нагрузке свойств
                {
                    System.Console.WriteLine($"Имя {RecordFssp.Name ?? ""}");

                    System.Console.WriteLine($"Номер договора {RecordFssp.Details ?? ""}");

                    System.Console.WriteLine($"Исполнительный лист {RecordFssp.ExeProduction ?? ""}");

                    System.Console.WriteLine($"Предмет {RecordFssp.Subject ?? ""}");

                    System.Console.WriteLine($"bailiff {RecordFssp.Bailiff ?? ""}");

                    System.Console.WriteLine($"ip_end {RecordFssp.IIpEnd ?? ""}");

                    System.Console.WriteLine("----");
                }
                }

                System.Console.WriteLine();

                System.Console.WriteLine("----");
            }




        }


    }


}
