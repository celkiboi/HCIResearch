using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

// USE ONLY ON STATIC FIELDS AND PROPERTIES
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
internal sealed class SettingsAttribute : Attribute
{
    public string SettingsName { get; private set; }

    public SettingsAttribute(string settingsName)
    {
        SettingsName = settingsName;
    }

    public static IEnumerable<FieldInfo> GetAllMembers()
    {
        List<FieldInfo> filteredMembers = new();

        Assembly assembly = Assembly.GetExecutingAssembly();

        foreach(Type type in assembly.GetTypes()) 
        {

            FieldInfo[] fields = type.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static);

            foreach (FieldInfo field in fields)
            {
                if (IsDefined(field, typeof(SettingsAttribute)))
                {
                    filteredMembers.Add(field);
                }
            }
        }

        return filteredMembers.AsReadOnly();
    }
}

