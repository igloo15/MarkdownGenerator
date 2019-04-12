# [TypeWrapper](./TypeWrapper.md)

Namespace: [Igloo15]() > [MarkdownApi]() > [Core](./README.md)

Assembly: igloo15.MarkdownApi.Core.dll

## Summary
A wrapper around a type

## Constructors

| Name | Summary | 
| --- | --- | 
| TypeWrapper ( [`PropertyInfo`](https://docs.microsoft.com/en-us/dotnet/api/System.Reflection.PropertyInfo) info ) |  | 
| TypeWrapper ( [`FieldInfo`](https://docs.microsoft.com/en-us/dotnet/api/System.Reflection.FieldInfo) info ) |  | 
| TypeWrapper ( [`MethodInfo`](https://docs.microsoft.com/en-us/dotnet/api/System.Reflection.MethodInfo) info ) |  | 
| TypeWrapper ( [`EventInfo`](https://docs.microsoft.com/en-us/dotnet/api/System.Reflection.EventInfo) info ) |  | 
| TypeWrapper ( [`ConstructorInfo`](https://docs.microsoft.com/en-us/dotnet/api/System.Reflection.ConstructorInfo) info ) |  | 
| TypeWrapper ( [`Type`](https://docs.microsoft.com/en-us/dotnet/api/System.Type) info ) |  | 
| TypeWrapper ( [`String`](https://docs.microsoft.com/en-us/dotnet/api/System.String) name ) |  | 


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


