using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using Newtonsoft.Json;
using Fssp;

namespace levin.FSSPv1
{
    partial class Program
    {
        static void Main(string[] args)
        {
            SearchClient searchClient = new SearchClient();

            string token = "DIWmQoRIZ5sD";

            List<string> tasks = new List<string>();

            tasks.Add(searchClient.Physical(token: token, region: 46, firstName: "Илья", lastName: "Левин", secondName: "Владимирович", birthDate: "01.11.1999"));

            //Генерация пользователей для группового запроса.

            RequestParams request = new RequestParams();

            string[] names = new string[] { "Максим", "Илья", "Даниил", "Владимир", "Артем", "Андрей", "Геннадий" };

            string[] lastNames = new string[] { "Прокофьев", "Зиновьев", "Артеменко", "Баклаков", "Семен", "Синий", "Белоконь" };

            string[] secoundNames = new string[] { "Владимирович", "Андреевич", "Романович", "Константинович", "Маратович", "Васильевич", "Евгеньевич" };

            string[] dates = new string[] { "01.11.1999", "01.11.1999", "01.11.1999", "01.11.1999", "01.11.1999", "01.11.1999", "01.11.1999" };

            List<QueryParams> peoples = new List<QueryParams>();

            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    peoples.Add(new QueryParams { FirstName = names[i], LastName = lastNames[j], SecoundName = secoundNames[j], DirthDate = dates[i], Region = "46" });
                }

            }

            GroupQuery groupQuery = new GroupQuery
            {
                Token = token,
                Request = new List<Params>
                {
                    new Params {
                        Request = new List<RequestParams> {
                            new RequestParams() {
                                Type = 1,
                                Params=  peoples
                            }
                        }
                    }
                }
            };

            tasks.Add(searchClient.Group(groupQuery));

            //с готовым результатом.
            tasks.Add("86b16aaa-ce53-4f62-91d4-a730bc868328"); 

            System.Console.WriteLine("Запрос отправлен");

            Client client = new Client();

            while (true)
            {
                ShowStatus( token,  tasks,  client);

                /// Тайм-аут задежки между запросами.
                System.Threading.Thread.Sleep(60000);
            }

         

        }
        public static void  ShowStatus(string token,List<string> tasks, Client client )
        {

            for (int i = 0; i < tasks.Count; i++)
            {
                var status = client.GetStatus(token, tasks[i]);

                string b = status.ToString();

                string a = $"{tasks[i]} - {b}";

                System.Console.WriteLine(a);

                if (status.Status == StatusTaskEnum.Ready)
                {
                    ShowResult(token, tasks[i], client);

                    tasks.Remove(tasks[i]);

                    break;
                }

            }

            System.Console.WriteLine("_____");
        }

        public static void ShowResult(string token, string task, Client client)
        {
            System.Console.WriteLine();

            System.Console.WriteLine($"Информация по task {task}");

            var result = client.GetResults(token,task);

            System.Console.WriteLine($"Время начала {result.TaskStart}");

            System.Console.WriteLine($"Время начала {result.TaskEnd}");

            foreach (var resultTask in result.ResultsTask)
            {
                System.Console.WriteLine($"Тип запроса {resultTask.Query.Type}");

                if (resultTask.RecordsFssp.Length == 0)
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
                    }
                }

                System.Console.WriteLine();

            }



        }

    }


}
