using System.Collections.Generic;
using System.Dynamic;

namespace CaseInsensitiveDynamicModelBinder
{
    /// <summary>
    /// Case-insensitive Dynamic Dictionary
    /// </summary>
    public class InsensitiveDynamicDictionary : DynamicObject
    {
        // The inner dictionary.
        Dictionary<string, object> dictionary = new Dictionary<string, object>();

        // This property returns the number of elements
        // in the inner dictionary.
        public int Count
        {
            get
            {
                return dictionary.Count;
            }
        }

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            // Converting the property name to lowercase
            // so that property names become case-insensitive.
            string name = binder.Name.ToLower();

            dictionary.TryGetValue(name, out result);

            // always return true so that visiting a not existing 
            // property will not throw an exception.
            return true;
        }

        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            // Converting the property name to lowercase
            // so that property names become case-insensitive.
            dictionary[binder.Name.ToLower()] = value;

            // You can always add a value to a dictionary,
            // so this method always returns true.
            return true;
        }

        public void SetMember(string name, object value)
        {
            dictionary[name.ToLower()] = value;
        }
    }
}
