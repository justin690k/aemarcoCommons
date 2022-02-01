﻿using aemarcoCommons.Extensions.CryptoExtensions;
using Microsoft.Extensions.Configuration;
using System;
using System.Reflection;

namespace aemarcoCommons.Toolbox.AppConfiguration.Transformations
{

    /// <summary>
    /// Use this to En-/Decrypt string properties while Saving and Loading
    /// You need to add the ProtectedTransformer during setup
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class ProtectedAttribute : Attribute { }



    //TODO: Add some summary
    public class ProtectedTransformer : StringTransformerBase
    {
        private readonly string _password;
        public ProtectedTransformer(
            string password)
        {
            _password = password;
        }


        public override string PerformReadTransformation(string currentValue, PropertyInfo propertyInfo, IConfigurationRoot configRoot)
        {
            //don´t attempt to decrypt empty strings
            if (string.IsNullOrWhiteSpace(currentValue)) return currentValue;


            //password must be specified
            var _ = _password ?? throw new ArgumentException("No Password provided for Cryptography");


            //skip if not a protected string, otherwise decrypt
            return Attribute.IsDefined(propertyInfo, typeof(ProtectedAttribute))
                ? currentValue.DecryptFromBase64(_password)
                : currentValue;
        }

        public override string PerformWriteTransformation(string currentValue, PropertyInfo propertyInfo, IConfigurationRoot configRoot)
        {
            //don´t attempt to decrypt empty strings
            if (string.IsNullOrWhiteSpace(currentValue)) return currentValue;


            //password must be specified
            var _ = _password ?? throw new ArgumentException("No Password provided for Cryptography");


            //skip if not a protected string, otherwise decrypt
            return Attribute.IsDefined(propertyInfo, typeof(ProtectedAttribute))
                ? currentValue.EncryptToBase64(_password)
                : currentValue;
        }
    }
}
