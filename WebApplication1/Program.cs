using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Topshelf;

namespace WebApplication1
{
    public class Program
    {
        public static void Main(string[] args)
        {

            var rc = HostFactory.Run(x =>                                   //1
            {
                x.Service<MainService>(s =>                                   //2
                {
                    s.ConstructUsing(name => new MainService(args));                //3
                    s.WhenStarted(tc => tc.Start());                         //4
                    s.WhenStopped(tc => tc.Stop());                          //5
                });
                x.RunAsLocalSystem();                                       //6

                x.SetDescription("JwtAPIService");                   //7
                x.SetDisplayName("JwtAPIService");                                  //8
                x.SetServiceName("JwtAPIService");                                  //9
            });                                                             //10
        }
    }
}
