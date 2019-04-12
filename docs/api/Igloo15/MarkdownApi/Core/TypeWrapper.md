# [TypeWrapper](./TypeWrapper.md)

Namespace: [igloo15]() > [MarkdownApi]() > [Core](./README.md)

Assembly: igloo15.MarkdownApi.Core.dll

## Summary
A wrapper around a type

## Constructors

| Name | Summary | 
| --- | --- | 
| TypeWrapper ( [`PropertyInfo`](https://docs.microsoft.com/en-us/dotnet/api/System.Reflection.PropertyInfo) info ) | Constructs a TypeWrapper with a PropertyInfo | 
| TypeWrapper ( [`FieldInfo`](https://docs.microsoft.com/en-us/dotnet/api/System.Reflection.FieldInfo) info ) | Constructs a TypeWrapper with FieldInfo | 
| TypeWrapper ( [`MethodInfo`](https://docs.microsoft.com/en-us/dotnet/api/System.Reflection.MethodInfo) info ) | Constructs a TypeWrapper with a MethodInfo | 
| TypeWrapper ( [`EventInfo`](https://docs.microsoft.com/en-us/dotnet/api/System.Reflection.EventInfo) info ) | Constructs a TypeWrapper with an Event Info | 
| TypeWrapper ( [`ConstructorInfo`](https://docs.microsoft.com/en-us/dotnet/api/System.Reflection.ConstructorInfo) info ) | Constructs a Typewrapper with a ConstructorInfo | 
| TypeWrapper ( [`Type`](https://docs.microsoft.com/en-us/dotnet/api/System.Type) info ) | Constructs a TypeWrapper with a basic type | 
| TypeWrapper ( [`String`](https://docs.microsoft.com/en-us/dotnet/api/System.String) name ) | Constructs a type wrapper with just a string name | 


## Properties

| Type | Name | Summary | 
| --- | --- | --- | 
| [String](https://docs.microsoft.com/en-us/dotnet/api/System.String) | FullName | Provides the full name of the wrapped type | 
| [MemberInfo](https://docs.microsoft.com/en-us/dotnet/api/System.Reflection.MemberInfo) | Info | Provide the base MemberInfo | 
| [String](https://docs.microsoft.com/en-us/dotnet/api/System.String) | Name | The short name of the wrapped type | 


## Methods

| Return | Name | Summary | 
| --- | --- | --- | 
| [String](https://docs.microsoft.com/en-us/dotnet/api/System.String) | GetId (  ) | Returns the id of the type wrapper based on the MemberInfo or the Name | 


