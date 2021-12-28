using System;

namespace Fssp
{

    public enum StatusTaskEnum
    {
        Ready=0,
        InWork=1,
        Wait=2,
        ReadyWithError=3

    }

    /// <summary>
    /// Структура в которой содержится статус выполняемой задачи.
    /// </summary>
    public struct StatusTask
    {
        /// <summary>
        /// Код статуса задачи.
        /// </summary>
        public StatusTaskEnum Status { get; set; }

        /// <summary>
        /// Общеее количества задач.
        /// </summary>
        public int TotalTasks { get; set; }

        /// <summary>
        /// Количество выполненных задач из общего количества задач.
        /// </summary>
        public int TasksDone { get; set; }

        /// <summary>
        /// Информация о статусе задачи.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string result = String.Empty;

            switch ((int)Status)
            {
                case 0:
                    result = $"Запрос выполнен! Выполнено {TasksDone}/{TotalTasks} задач.";
                    break;
                case 1:
                    result = $"Запрос выполняется! Выполнено {TasksDone}/{TotalTasks} задач.";
                    break;
                case 2:
                    result = $"Запрос в очереди на выполнение! Выполнено {TasksDone}/{TotalTasks} задач.";
                    break;

                case 3:
                    result = $"Запрос выполнен частично, возможно с ошибками! Выполнено {TasksDone}/{TotalTasks} задач.";
                    break;
            }

            return result;
        }

    }




}
