using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using Mono.CSharp;

namespace ScriptCs.Wrappers
{
    [Export(Constants.MonoContractName, typeof(IScriptEngine))]
    public class MonoScriptEngineWrapper : IScriptEngine
    {
        public Evaluator Evaluator { get; private set; }

        public string BaseDirectory { get; set; }

        [ImportingConstructor]
        public MonoScriptEngineWrapper()
        {
            var settings = new CompilerSettings {LoadDefaultReferences = false};
            Evaluator = new Evaluator(settings, new Report(new ConsoleReportPrinter()));
        }
        
        public void AddReference(string assemblyDisplayNameOrPath)
        {            
            if (File.Exists(assemblyDisplayNameOrPath))
            {
                Evaluator.ReferenceAssembly(Assembly.LoadFile(assemblyDisplayNameOrPath));    
            }
            else
            {
                var assembly = Assembly.LoadWithPartialName(assemblyDisplayNameOrPath);
                Evaluator.ReferenceAssembly(assembly);
            }            
        }

        public ISession CreateSession<THostObject>(THostObject hostObject) where THostObject : class
        {
            return new MonoSessionWrapper(this);
        }

        public ISession CreateSession(object hostObject, Type hostObjectType = null)
        {
            return new MonoSessionWrapper(this);
        }

        public ISession CreateSession()
        {
            return new MonoSessionWrapper(this);
        }
    }
}
