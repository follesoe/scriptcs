using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Roslyn.Scripting;

namespace ScriptCs.Wrappers
{
    public class MonoSessionWrapper : ISession
    {
        public IScriptEngine Engine { get; private set; }
        public MonoScriptEngineWrapper WrappedEnginge { get; private set; }
        public Session WrappedSession { get; private set; }

        public MonoSessionWrapper(MonoScriptEngineWrapper engine)
        {
            Engine = engine;
            WrappedEnginge = engine;
        }

        public object Execute(string code)
        {
            Console.WriteLine("Execute:\r\n{0}\r\n", code);

            
            foreach (var line in code.Split(new[] {Environment.NewLine}, StringSplitOptions.RemoveEmptyEntries))
            {
                object result;
                bool resultSet;
                WrappedEnginge.Evaluator.Evaluate(line, out result, out resultSet);
            }

            return null;
        }

        public void AddReference(string assemblyDisplayNameOrPath)
        {
            Console.WriteLine("Add reference: {0}", assemblyDisplayNameOrPath);
            Engine.AddReference(assemblyDisplayNameOrPath);
        }

        public void ImportNamespace(string @namespace)
        {
            Console.WriteLine("Import namespace: {0}", @namespace);
            throw new NotImplementedException();
        }

        public ISubmission<T> CompileSubmission<T>(string code)
        {
            Console.WriteLine("Compile: {0}", code);
            throw new NotImplementedException();
        }
    }
}
