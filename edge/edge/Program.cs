using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EdgeJs;

namespace edge
{
  class Program
  {
    public static async Task Start()
    {
      var func = Edge.Func(@"
        var  swig = require('swig')
        return function (data, cb) {
            var template = swig.compile(data.template)
            var output = template(data.data)
            cb(null, output);
        }
    ");
      var output = await func(new
      {
        template = "{% if value > 10 %}<span>bigger than 10</span>{% else %}<span>less than or equal 10</span>{% endif %}",
        dta = new
        {
          value = 11
        }
      });
      Console.WriteLine(output);
      output = await func(new
      {
        template = "{{ a + b }}",
        data = new
        {
          a = 10,
          b = 11
        }
      });
      Console.WriteLine(output);
    }

    static void Main(string[] args)
    {
      var watch = Stopwatch.StartNew();
      for (int i = 0; i < 10000; i++)
      {
        Start().Wait();
      }
      watch.Stop();
      Console.WriteLine(watch.ElapsedMilliseconds / 1000.0);
      watch.Restart();
      Parallel.For(0, 10000, (i) => Start().Wait());
      watch.Stop();
      Console.WriteLine(watch.ElapsedMilliseconds / 1000.0);
      Console.ReadLine();
    }
  }
}
