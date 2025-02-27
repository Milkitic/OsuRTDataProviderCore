﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Text.RegularExpressions;

namespace OsuRTDataProvider
{
    public static class Utils
    {
        private static string _version;

        public static double ConvertVersionStringToValue(string osu_version_string)
        {
            if (double.TryParse(Regex.Match(osu_version_string, @"\d+(\.\d*)?").Value.ToString(), NumberStyles.Float, CultureInfo.InvariantCulture, out var ver))
            {

                return ver;
            }

            throw new Exception("Can't parse version: " + osu_version_string);
        }

        //https://gist.github.com/peppy/3a11cb58c856b6af7c1916422f668899
        public static List<double> GetErrorStatisticsArray(List<int> list)
        {
            if (list == null || list.Count == 0)
                return null;
            List<double> result = new List<double>(4);
            double total = 0, _total = 0, totalAll = 0;
            int count = 0, _count = 0;
            int max = 0, min = int.MaxValue;
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i] > max)
                    max = list[i];
                if (list[i] < min)
                    min = list[i];
                totalAll += list[i];
                if (list[i] >= 0)
                {
                    total += list[i];
                    count++;
                }
                else
                {
                    _total += list[i];
                    _count++;
                }
            }
            double avarage = totalAll / list.Count;
            double variance = 0;
            for (int i = 0; i < list.Count; i++)
            {
                variance += Math.Pow(list[i] - avarage, 2);
            }
            variance = variance / list.Count;
            result.Add(_count == 0 ? 0 : _total / _count); //0
            result.Add(count == 0 ? 0 : total / count); //1
            result.Add(avarage); //2
            result.Add(variance); //3
            result.Add(Math.Sqrt(variance)); //4
            result.Add(max); //5
            result.Add(min); //6
            return result;
        }

        public static string GetVersion()
        {
            if (_version != null) return _version;
            var ver = Assembly.GetExecutingAssembly().GetCustomAttribute<AssemblyInformationalVersionAttribute>();
            _version = ver.InformationalVersion;
            return _version;
        }
    }

    public enum LogLevel
    {
        Trace = 0,
        Debug = 1,
        Information = 2,
        Warning = 3,
        Error = 4,
        Critical = 5,
        None = 6,
    }

    public interface ILoggerFactory
    {
        ILogger CreateLogger(string name);
        ILogger<T> CreateLogger<T>();
    }

    public interface ILogger<T> : ILogger
    {
    }

    public interface ILogger
    {
        void Log(LogLevel logLevel, string message);
        void LogInformation(string message);
        void LogDebug(string message);
        void LogError(string message);
        void LogWarning(string message);
        void LogInformation(Exception exception, string message);
        void LogDebug(Exception exception, string message);
        void LogError(Exception exception, string message);
        void LogWarning(Exception exception, string message);
    }

    public static class Logger
    {
        private static ILoggerFactory _loggerFactory;
        public static void SetLoggerFactory(ILoggerFactory loggerFactory) => _loggerFactory = loggerFactory;

        public static void Info(string message)
        {
            if (_loggerFactory is null) Console.WriteLine(message);
            else _loggerFactory.CreateLogger("ORTDP").LogInformation(message);
        }

        public static void Debug(string message)
        {
            if (_loggerFactory is null) Console.WriteLine(message);
            else _loggerFactory.CreateLogger("ORTDP").LogDebug(message);
        }

        public static void Error(string message)
        {
            if (_loggerFactory is null) Console.WriteLine(message);
            else _loggerFactory.CreateLogger("ORTDP").LogError(message);
        }

        public static void Warn(string message)
        {
            if (_loggerFactory is null) Console.WriteLine(message);
            else _loggerFactory.CreateLogger("ORTDP").LogWarning(message);
        }
    }
}
