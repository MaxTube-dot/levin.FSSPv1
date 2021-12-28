using System.Collections.Generic;
using Fssp;
using System;
using NLog;

namespace levin.FSSPv1
{
    partial class Program
    {
        static string Token = "DIWmQoRIZ5sD";

        private static List<string> _tasksIds = new List<string>();

        static void Main(string[] args)
        {
            //var logger = LogManager.GetCurrentClassLogger();

            //logger.Debug("Собрал");


            SearchClient searchClient = new SearchClient();

            _tasksIds.Add(searchClient.Physical(token: Token, region: 46, firstName: "Илья", lastName: "Левин", secondName: "Владимирович", birthDate: "01.11.1999"));



            //Генерация пользователей для группового запроса.

            var randomPersons = GenerateRandomPersons();

            GroupRequest groupRequest = GenerateGroupRequest(randomPersons);

            _tasksIds.Add(searchClient.Group(groupRequest));



            // Task с готовым результатом. После 13 часов может быть битым!

            _tasksIds.Add("86b16aaa-ce53-4f62-91d4-a730bc868328"); 





            Client client = new Client();

            while (true)
            {
                foreach (var taslId in _tasksIds)
                {
                    StatusTaskEnum = ShowStatus(taslId);
                }

                /// Тайм-аут задежки между запросами.
                System.Threading.Thread.Sleep(60000);
            }

         

        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="client"></param>
        public static StatusTaskEnum GetStatus(string token,string taskId,Client client )
        {

            var status = client.GetStatus(Token, taskId);

            Console.WriteLine(status.ToString());
           
        }

        private static Person[] GenerateRandomPersons()
        {
            string[] names = new string[] { "Максим", "Илья", "Даниил", "Владимир", "Артем", "Андрей", "Геннадий" };

            string[] lastNames = new string[] { "Прокофьев", "Зиновьев", "Артеменко", "Баклаков", "Семен", "Синий", "Белоконь" };

            string[] secoundNames = new string[] { "Владимирович", "Андреевич", "Романович", "Константинович", "Маратович", "Васильевич", "Евгеньевич" };

            string[] dates = new string[] { "01.11.1999", "01.11.1999", "01.11.1999", "01.11.1999", "01.11.1999", "01.11.1999", "01.11.1999" };

            List<Person> persons = new List<Person>();

            for (int i = 0; i < 7; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    persons.Add(new Person { FirstName = names[i], LastName = lastNames[j], SecondName = secoundNames[j], BirthDate = dates[i], Region = "46" });
                }

            }

            return persons.ToArray();
        }


        private static GroupRequest GenerateGroupRequest(Person[] persons )
        {
            List<Params> parametrs = new List<Params>();  

            foreach (var person in persons)
            {
                parametrs.Add(new Params() 
                {
                    FirstName = person.FirstName,                    
                    SecondName = person.SecondName,
                    LastName = person.LastName, 
                    BirthDate = person.BirthDate,
                    Region = person.Region 
                });
            }

            List<Request> requests = new List<Request>();

            foreach (var item in parametrs)
            {
                requests.Add(new Request() { Params = item, Type = 1 });
            }

            GroupRequest groupRequest = new GroupRequest() { Token = Token, Request = requests.ToArray() };

            return groupRequest;
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

    public class Person
    {
        public string FirstName { get; set; }

        public string LastName {  get; set; }

        public string SecondName { get; set; }

        public string BirthDate { get; set; }

        public string Region { get; set; }
    }
}
