using System;
using System.Reflection;
using System.Reflection.Emit;

namespace ProjDynamicMethod
{
    public class Worker
    {
        private readonly int ITERATIONS;

        public Worker(int iterations)
        {
            ITERATIONS = iterations;
        }

        public delegate object ConstructorDelegate();

        protected ConstructorDelegate GetConstructor(string TypeName)
        {
            //Get the defaul constructor of the type
            Type t = Type.GetType(TypeName);
            ConstructorInfo ctor = t.GetConstructor(new Type[0]);

            // create a new dynamic method that constructs and returns the type
            string methodName = t.Name + "Ctor";
            DynamicMethod dm = new DynamicMethod(methodName, t, new Type[0], typeof(Activator));
            ILGenerator lgen = dm.GetILGenerator();
            lgen.Emit(OpCodes.Newobj, ctor);
            lgen.Emit(OpCodes.Ret);

            //add delegate to dictionary and return
            ConstructorDelegate creator = (ConstructorDelegate)dm.CreateDelegate(typeof(ConstructorDelegate));

            //return a delegate to the method
            return creator;

        }

        public bool InstanceByReflection()
        {
            var type = Type.GetType("System.Text.StringBuilder");
            for (int i = 0; i < ITERATIONS; i++)
            {
                var obj = Activator.CreateInstance(type);
                if (obj.GetType() != typeof(System.Text.StringBuilder))
                {
                    throw new InvalidOperationException("Constructed Object is not a StringBuilder");
                }
            }

            return true;
        }

        public bool InstanceByDynamicMEthod()
        {

            var constructor = GetConstructor("System.Text.StringBuilder");
            for (int i = 0; i < ITERATIONS; i++)
            {
                var obj = constructor();
                if (obj.GetType() != typeof(System.Text.StringBuilder))
                {
                    throw new InvalidOperationException("Constructed Object is not a StringBuilder");
                }
            }

            return true;
        }

        public bool InstanceByDirectReference()
        {
            for (int i = 0; i < ITERATIONS; i++)
            {
                var obj = new System.Text.StringBuilder();
                if (obj.GetType() != typeof(System.Text.StringBuilder))
                {
                    throw new InvalidOperationException("Constructed Object is not a StringBuilder");
                }
            }

            return true;
        }

    }
}
