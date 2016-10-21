using System;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using System.Text.RegularExpressions;
using System.IO;

namespace Server
{
    // Класс-обработчик клиента
    class Client
    {
        // Отправка страницы с ошибкой
        private static void SendError(TcpClient Client, int Code)
        {
            // Получаем строку вида "200 OK"
            // HttpStatusCode хранит в себе все статус-коды HTTP/1.1
            string CodeStr = Code.ToString() + " " + ((HttpStatusCode)Code).ToString();
            // Код простой HTML-странички
            string Html = "<html><body><h1>" + CodeStr + "</h1></body></html>";
            // Необходимые заголовки: ответ сервера, тип и длина содержимого. После двух пустых строк - само содержимое
            string Str = "HTTP/1.1 " + CodeStr + "\nContent-type: text/html\nContent-Length:" + Html.Length.ToString() + "\n\n" + Html;
            // Приведем строку к виду массива байт
            byte[] Buffer = Encoding.ASCII.GetBytes(Str);
            // Отправим его клиенту
            Client.GetStream().Write(Buffer, 0, Buffer.Length);
            // Закроем соединение
            Client.Close();
        }
        // Конструктор класса. Ему нужно передавать принятого клиента от TcpListener
        public Client(TcpClient Client)
        {
            // Объявим строку, в которой будет хранится запрос клиента
            string Request = "";
            // Буфер для хранения принятых от клиента данных
            byte[] Buffer = new byte[10 * 1024 * 1024];
            // Переменная для хранения количества байт, принятых от клиента
            int Count;
            // Читаем из потока клиента до тех пор, пока от него поступают данные
            while ((Count = Client.GetStream().Read(Buffer, 0, Buffer.Length)) > 0)
            {
                // Преобразуем эти данные в строку и добавим ее к переменной Request
                Request += Encoding.ASCII.GetString(Buffer, 0, Count);
            }

            // Парсим строку запроса с использованием регулярных выражений
            // При этом отсекаем все переменные GET-запроса
            Match ReqMatch = Regex.Match(Request, @"^\w+\s+([^\s\?]+)[^\s]*\s+HTTP/.*|");

            // Если запрос не удался
            if (ReqMatch == Match.Empty)
            {
                // Передаем клиенту ошибку 400 - неверный запрос
                SendError(Client, 400);
                return;
            }

            // Получаем строку запроса
            string RequestUri = ReqMatch.Groups[1].Value;

            // Приводим ее к изначальному виду, преобразуя экранированные символы
            // Например, "%20" -> " "
            RequestUri = Uri.UnescapeDataString(RequestUri);

            // Если в строке содержится двоеточие, передадим ошибку 400
            // Это нужно для защиты от URL типа http://example.com/../../file.txt
            if (RequestUri.IndexOf("..") >= 0)
            {
                SendError(Client, 400);
                return;
            }

            // Читаем запрос до пробела, тем самым записывая тип запроса
            string HttpMethod = Request.Substring(0, Request.IndexOf(' '));

            // Определяем тип запроса и вызываем соответствующий петод
            switch (HttpMethod)
            {
                case "GET":
                    GetRequest(Buffer, Client, RequestUri);
                    break;
                case "POST":
                    PostRequest(Request);
                    break;
                case "PUT":
                    PutRequest(Client, RequestUri, Request);
                    break;
                case "DELETE":
                    DeleteRequest(Client, RequestUri);
                    break;
                default:
                    SendError(Client, 400);
                    return;
            }

            // Закроем соединение
            Client.Close();
        }
        // Обработчик POST-запроса
        public static void PostRequest(string Request)
        {
            // Записываем тело запроса
            string RequestBody = Request.Substring(Request.IndexOf("\r\n\r\n"), Request.Length);


            // Приводим ее к изначальному виду, преобразуя экранированные символы
            // Например, "%20" -> " "
            RequestBody = Uri.UnescapeDataString(RequestBody);

            // Создаём файл записи, если он отсутствует
            if (!File.Exists(@"PostData.txt"))
            {
                File.Create(@"PostData.txt");
            }

            // Записываем в него тело запроса
            File.WriteAllText(@"PostData.txt", RequestBody);

        }
        // Обработчик PUT-запроса
        public static void PutRequest(TcpClient Client, string RequestUri, string Request)
        {
            // Добавляем путь к файлу к директории программы
            string FilePath = Directory.GetCurrentDirectory() + RequestUri;

            // Записываем тело запроса
            string RequestBody = Request.Substring(Request.IndexOf("\r\n\r\n"), Request.Length);


            // Пробуем создать файл, если не получается, выдаём ошибку
            try
            {
                File.Create(FilePath);
                File.WriteAllText(FilePath, RequestBody);
            }
            catch (Exception)
            {
                SendError(Client, 404);
                return;
            }
        }
        // Обработчик DELETE-запроса
        public static void DeleteRequest(TcpClient Client, string RequestUri)
        {
            // Добавляем путь к файлу к директории программы
            string FilePath = Directory.GetCurrentDirectory() + RequestUri;

            // Если файл не существует, выдаём ошибку, иначе - удаляем
            if (!File.Exists(FilePath))
            {
                SendError(Client, 404);
                return;
            }
            else
            {
                File.Delete(FilePath);
            }
        }
        // Обработчик GET-запроса
        public static void GetRequest(byte[] Buffer, TcpClient Client, string RequestUri)
        {
            // Добавляем путь к файлу к директории программы
            string FilePath = Directory.GetCurrentDirectory() + RequestUri;

            // Если в папке www не существует данного файла, посылаем ошибку 404
            if (!File.Exists(FilePath))
            {
                SendError(Client, 404);
                return;
            }

            // Открываем файл, страхуясь на случай ошибки
            FileStream FS;
            try
            {
                FS = new FileStream(FilePath, FileMode.Open, FileAccess.Read, FileShare.Read);
            }
            catch (Exception)
            {
                // Если случилась ошибка, посылаем клиенту ошибку 500
                SendError(Client, 500);
                return;
            }

            int Count;
            // Пока не достигнут конец файла
            while (FS.Position < FS.Length)
            {
                // Читаем данные из файла
                Count = FS.Read(Buffer, 0, Buffer.Length);
                // И передаем их клиенту
                Client.GetStream().Write(Buffer, 0, Count);
            }

            // Закроем файл
            FS.Close();
        }
    }
    class Server
    {
        // Объект, принимающий TCP-клиентов
        TcpListener Listener;
        // Запуск сервера
        public Server(int Port)
        {
            // Создаем "слушателя" для указанного порта
            Listener = new TcpListener(IPAddress.Any, Port);
            // Запускаем его
            Listener.Start();

            // В бесконечном цикле
            while (true)
            {
                // Принимаем новых клиентов. После того, как клиент был принят, он передается в новый поток (ClientThread)
                // с использованием пула потоков.
                ThreadPool.QueueUserWorkItem(new WaitCallback(ClientThread),
                    Listener.AcceptTcpClient());
            }
        }
        static void ClientThread(Object StateInfo)
        {
            // Просто создаем новый экземпляр класса Client и передаем ему приведенный к классу TcpClient объект StateInfo
            new Client((TcpClient)StateInfo);
        }
        // Остановка сервера
        ~Server()
        {
            // Если "слушатель" был создан
            if (Listener != null)
            {
                // Остановим его
                Listener.Stop();
            }
        }
        static void Main(string[] args)
        {
            // Определим нужное максимальное количество потоков
            // Пусть будет по 4 на каждый процессор
            int MaxThreadsCount = Environment.ProcessorCount * 4;
            // Установим максимальное количество рабочих потоков
            ThreadPool.SetMaxThreads(MaxThreadsCount, MaxThreadsCount);
            // Установим минимальное количество рабочих потоков
            ThreadPool.SetMinThreads(2, 2);
            // Создадим новый сервер на порту 80
            new Server(80);
        }
    }
}

