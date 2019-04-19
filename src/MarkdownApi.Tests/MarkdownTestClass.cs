using System;
using System.Collections.Generic;
using System.Text;

namespace igloo15.MarkdownApi.Tests
{
    /// <summary>
    /// Test Class Summary
    /// </summary>
    public class MarkdownTestClass
    {
        /// <summary>
        /// Test Constructor
        /// </summary>
        public MarkdownTestClass()
        {
        }

        /// <summary>
        /// Test Normal Property
        /// </summary>
        public string TestNormal { get; set; }

        /// <summary>
        /// Test Generic List Property
        /// </summary>
        public List<string> TestListNormal { get; set; }

        /// <summary>
        /// Test Array Property
        /// </summary>
        public string[] TestArrayNormal { get; set; }

        /// <summary>
        /// Test MultiArray Property
        /// </summary>
        public string[,] TestMultiArrayNormal { get; set; }

        /// <summary>
        /// Test Normal Method
        /// </summary>
        public void TestMethodNormal1()
        {
        }

        /// <summary>
        /// Test Normal Method Param
        /// </summary>
        /// <param name="normalParam">Param1</param>
        public void TestMethodNormal2(string normalParam)
        {
        }

        /// <summary>
        /// Test Normal Method Param 2
        /// </summary>
        /// <param name="normalParam">Param1</param>
        /// <param name="normalParam2">Param2</param>
        public void TestMethodNormal3(string normalParam, List<string> normalParam2)
        {
        }

        /// <summary>
        /// Test Normal Method Param 3
        /// </summary>
        /// <param name="normalParam">Param1</param>
        /// <param name="normalParam2">Param2</param>
        /// <param name="normalParam3">Param3</param>
        public void TestMethodNormal4(string normalParam, List<string> normalParam2, string[] normalParam3)
        {
        }

        /// <summary>
        /// Test Normal Method Param 4
        /// </summary>
        /// <param name="normalParam">Param1</param>
        /// <param name="normalParam2">Param2</param>
        /// <param name="normalParam3">Param3</param>
        /// <param name="normalParam4">Param4</param>
        public void TestMethodNormal5(string normalParam, List<string> normalParam2, string[] normalParam3, string[,] normalParam4)
        {
        }

        /// <summary>
        /// Test Generic Method Param
        /// </summary>
        /// <typeparam name="T">Generic1</typeparam>
        /// <param name="normalParam">Param1</param>
        public void TestGenericMethod1<T>(T normalParam)
        {
        }

        /// <summary>
        /// Test Generic Method Param 2
        /// </summary>
        /// <typeparam name="T">Generic1</typeparam>
        /// <param name="normalParam">Param1</param>
        /// <param name="normalParam2">Param2</param>
        public void TestGenericMethod2<T>(T normalParam, List<T> normalParam2)
        {
        }

        /// <summary>
        /// Test Generic Method Param 3
        /// </summary>
        /// <typeparam name="T">Generic1</typeparam>
        /// <param name="normalParam">Param1</param>
        /// <param name="normalParam2">Param2</param>
        /// <param name="normalParam3">Param3</param>
        public void TestGenericMethod3<T>(T normalParam, List<T> normalParam2, T[] normalParam3)
        {
        }

        /// <summary>
        /// Test Generic Method Param 4
        /// </summary>
        /// <typeparam name="T">Generic1</typeparam>
        /// <param name="normalParam">Param1</param>
        /// <param name="normalParam2">Param2</param>
        /// <param name="normalParam3">Param3</param>
        /// <param name="normalParam4">Param4</param>
        public void TestGenericMethod4<T>(T normalParam, List<T> normalParam2, T[] normalParam3, T[,] normalParam4)
        {
        }

        /// <summary>
        /// Basic Generic Method
        /// </summary>
        /// <typeparam name="T">Generic1</typeparam>
        /// <param name="normalParam">Param1</param>
        public void TestGenericMethod5<T>(MarkdownTestGenericClass<T, string, string> normalParam)
        {
        }

        /// <summary>
        /// Basic Generic Method with Array Type
        /// </summary>
        /// <typeparam name="T">Generic1</typeparam>
        /// <param name="normalParam">Param1</param>
        public void TestGenericMethod6<T>(MarkdownTestGenericClass<T, string, string[,]> normalParam)
        {
        }

        /// <summary>
        /// Generic Test Method with lots of nesting
        /// </summary>
        /// <typeparam name="T">Generic1</typeparam>
        /// <param name="normalParam">Param1</param>
        /// <param name="normalParam2">Param2</param>
        public void TestGenericMethod7<T>(MarkdownTestGenericClass<MarkdownTestGenericClass<string[,], List<string[,]>, string>, string, string[,]> normalParam, MarkdownTestGenericClass<T, string, string[,]> normalParam2)
        {
        }

        /// <summary>
        /// TestNullableMethod1
        /// </summary>
        /// <param name="test1">test1</param>
        public void TestNullableMethod1(int? test1)
        {
        }

        /// <summary>
        /// TestNullableMethod2
        /// </summary>
        /// <param name="test1">test1</param>
        /// <param name="test2">test2</param>
        public void TestNullableMethod2(int? test1, int?[] test2)
        {
        }

        /// <summary>
        /// TestNullableMethod3
        /// </summary>
        /// <param name="test1">test1</param>
        /// <param name="test2">test2</param>
        /// <param name="test3">test3</param>
        public void TestNullableMethod3(int? test1, int?[] test2, int?[,] test3)
        {
        }
    }

    /// <summary>
    /// Markdown Generic Class
    /// </summary>
    /// <typeparam name="T">Generic1</typeparam>
    /// <typeparam name="K">Generic2</typeparam>
    /// <typeparam name="J">Generic3</typeparam>
    public class MarkdownTestGenericClass<T, K, J>
    {
        /// <summary>
        /// Test Construct Generic Class
        /// </summary>
        public MarkdownTestGenericClass()
        {
        }
    }

    /// <summary>
    /// Constructor Class Test 1
    /// </summary>
    public class MarkdownConstructorTestClass1
    {
        /// <summary>
        /// Constructor Test 1 Param
        /// </summary>
        /// <param name="test1">Test1</param>
        public MarkdownConstructorTestClass1(int test1)
        {
        }
    }

    /// <summary>
    /// Constructor Class Test 2
    /// </summary>
    public class MarkdownConstructorTestClass2
    {
        /// <summary>
        /// Constructor Test 2 Param
        /// </summary>
        /// <param name="test1">Test1</param>
        /// <param name="test2">Test2</param>
        public MarkdownConstructorTestClass2(int test1, List<int> test2)
        {
        }
    }

    /// <summary>
    /// Constructor Class Test 3
    /// </summary>
    public class MarkdownConstructorTestClass3
    {
        /// <summary>
        /// Constructor Test 3 Param
        /// </summary>
        /// <param name="test1">Test1</param>
        /// <param name="test2">Test2</param>
        /// <param name="test3">Test3</param>
        public MarkdownConstructorTestClass3(int test1, List<int> test2, int[] test3)
        {
        }
    }

    /// <summary>
    /// Constructor Class Test 4
    /// </summary>
    public class MarkdownConstructorTestClass4
    {
        /// <summary>
        /// Constructor Test 4 Param
        /// </summary>
        /// <param name="test1">Test1</param>
        /// <param name="test2">Test2</param>
        /// <param name="test3">Test3</param>
        /// <param name="test4">Test4</param>
        public MarkdownConstructorTestClass4(int test1, List<int> test2, int[] test3, int[,] test4)
        {
        }
    }
}