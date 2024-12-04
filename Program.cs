class Program
{
  static void Main()
  {
    Console.WriteLine("changes!");

    int port = 5000;
    var counter = 0;
    string[] usernames = [];
    string[] passwords = [];
    string[] ids = [];
    var server = new Server(port);
    Console.WriteLine("The server is running");
    Console.WriteLine($"Main Page: http://localhost:{port}/website/pages/index.html");

    while (true)
    {
      (var request, var response) = server.WaitForRequest();

      Console.WriteLine("recieved a request: " + request.Path);

      if (File.Exists(request.Path))
      {
        var file = new File(request.Path);
        response.Send(file);
      }
      else if (request.ExpectsHtml())
      {
        var file = new File("website/peges/404.html");
        response.SetStatusCode(404);
        response.Send(file);
      }
      else
      {
        try
        {
          if (request.Path == "Message")
          {
            string text = request.GetBody<string>();
            Console.WriteLine("Recieved ' " + text + " ' from the client!");
          }
          if (request.Path == "Button")
          {
            counter++;
            Console.WriteLine(counter);
            response.Send(counter);
          }
          if (request.Path == "Mess")
          {
            counter--;
            Console.WriteLine(counter);
            response.Send(counter);
          }
          else if (request.Path == "signup")
          {
            (string username, string password) = request.GetBody<(string, string)>();
            usernames = [.. usernames, username];
            passwords = [.. passwords, password];
            ids = [.. ids, Guid.NewGuid().ToString()];
            Console.WriteLine(username + ", " + password);
          }
          else if (request.Path == "login")
          {
            (string username, string password) = request.GetBody<(string, string)>();

            bool FoundUser = false;
            string UserID = "";

            for (int i = 0; i < usernames.Length; i++)
            {
              if (username == usernames[i] && password == passwords[i])
              {
                FoundUser = true;
                UserID = ids[i];
              }
            }

            response.Send((FoundUser, UserID));
          }
          else if (request.Path == "GetUserName")
          {
            string UserID = request.GetBody<string>();

            int i = 0;
            while (ids[i] != UserID)
            {
              i++;
            }

            string UserName = usernames[i];
            response.Send(UserName);
          }
        }

        catch (Exception exception)
        {
          Log.WriteException(exception);
        }
      }

      response.Close();
    }
  }
}