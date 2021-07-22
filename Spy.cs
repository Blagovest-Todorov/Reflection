using System;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Stealer
{
    public class Spy
    {
        public string StealFieldInfo(string investigatedClass, params string[]requestedFields) 
        {
            //We use reflection to get metadata about module, class, methos, property
            // Reflection uses Type, typeof , GetType()
            // Type is the root of the System.Reflection functionality and is the
            // primary way to access metadata. //Type is an abstract base class
            // type object = instance of the class Type
           // An instance of the Type class can represent any of the following types:
               //  Classes 
              ///Value types    
               //Arrays    
                 // Interfaces    
                // Enumerations    
                // Delegates    
                 //Constructed generic types and generic type definitions    
             // Type arguments and type parameters of constructed generic types,
             // generic type definitions, //and // generic method   definitions
    
            Type classType = Type.GetType(investigatedClass);
            FieldInfo[] classFields = classType.GetFields(
                BindingFlags.Instance | BindingFlags.Static | BindingFlags.NonPublic 
                | BindingFlags.Public);

            StringBuilder sb = new StringBuilder();

            Object classInstance = Activator.CreateInstance(classType, new object[] { });
            sb.AppendLine($"Class under investigation: {investigatedClass}");

            foreach (FieldInfo field in classFields.Where( f => requestedFields.Contains(f.Name)))
            {
                sb.AppendLine($"{field.Name} = {field.GetValue(classInstance)}");
            }

            return sb.ToString().Trim();
        }
    }
}
