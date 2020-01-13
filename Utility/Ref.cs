using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UtilityScript
{
    public class Ref<T>
    {
        private T reference;

        public T Value { get { return reference; } set { reference = value; } }

        public Ref(T reference)
        {
            this.reference = reference;
        }

        public Ref() { }
    }
}
