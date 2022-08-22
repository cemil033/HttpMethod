using System.Net;
using System.Reflection;
using HomeHttpServer;
using SimpleInjector;

Container container = new Container();
container.Register<Home>();
HttpListener listener = new HttpListener();
listener.Prefixes.Add("http://localhost:63291/");
listener.Start();
string cls="";
string mth="";
Type? h;
while (true)
{
    var context = listener.GetContext(); 
    var request = context.Request;
    var response = context.Response;
    foreach (string key in request.QueryString.Keys)
    {
        if (key == "con")
        {
            cls = request.QueryString[key];
        }
        else if(key == "method")
        {
            mth = request.QueryString[key];
        }
        else
        {

        }
        Console.WriteLine(key);
        Console.WriteLine(request.QueryString[key]);
    }
    try
    {

        if (cls != "")
        {
            h = Type.GetType("HomeHttpServer." + cls);
            var m = h?.GetMethod(mth);
            var t = Activator.CreateInstance(h);
            if (m != null)
            {
                m.Invoke(t, null);
            }
        }

    }
    catch (Exception e)
    {

        Console.WriteLine(e.Message);
    }
    
}
