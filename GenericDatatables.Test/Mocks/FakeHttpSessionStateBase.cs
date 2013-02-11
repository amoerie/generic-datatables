using System.Collections.Generic;
using System.Web;

namespace GenericDatatables.Test.Mocks
{
    public class FakeHttpSessionStateBase : HttpSessionStateBase
    {
        private readonly IDictionary<string, object> _sessionObjects;

        public FakeHttpSessionStateBase()
        {
            _sessionObjects = new Dictionary<string, object>();
        }

        public override object this[string name]
        {
            get { return _sessionObjects.ContainsKey(name) ? _sessionObjects[name] : null; }
            set { _sessionObjects[name] = value; }
        }
    }
}