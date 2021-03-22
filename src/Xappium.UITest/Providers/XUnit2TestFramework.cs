﻿using System;
using System.Reflection;

namespace Xappium.UITest.Providers
{
    internal class XUnit2TestFramework : ITestFramework
    {
        private Assembly assembly;

        public bool IsAvailable
        {
            get
            {
                try
                {
                    assembly = Assembly.Load(new AssemblyName("xunit.assert"));

                    return assembly is not null;
                }
                catch
                {
                    return false;
                }
            }
        }

        public void Throw(string message)
        {
            Type exceptionType = assembly.GetType("Xunit.Sdk.XunitException");
            if (exceptionType is null)
            {
                throw new Exception("Failed to create the XUnit assertion type");
            }

            throw (Exception)Activator.CreateInstance(exceptionType, message);
        }

        public virtual void AttachFile(string filePath, string description)
        {
        }

        public virtual void WriteLine(string message)
        {
        }
    }
}
