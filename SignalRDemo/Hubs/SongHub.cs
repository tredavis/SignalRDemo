using System.Web.UI;
using Microsoft.AspNet.SignalR;

namespace SignalRDemo.Hubs
{
    /// <summary>
    /// 
    /// </summary>
    public class SongHub : Hub
    {
        public void Hello()
        {
            Clients.All.hello();
           
        }
    }
}