﻿using Microsoft.AspNetCore.Mvc.ModelBinding;
using POC_NamingApi.ModelBinders;

namespace POC_NamingApi.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class SnakeCaseObjectAttribute : Attribute, ISnakeCaseNamingAttribute, IBinderTypeProviderMetadata
    {
        public string Name { get; }
        public BindingSource BindingSource => BindingSource.Custom;
        public Type BinderType => typeof(SnakeCaseModelBinder);
    }
}