using DataModel;
using DataModel.Football;
using DataModel.Temp;
using NLog;
using System;
using System.IO;

namespace DataAccess.FileAccess
{
    public static class FileHandler
    {
        #region private declaration

        private static readonly Logger logger = LogManager.GetLogger("FileHandler");

        private const string temperatureFilePath = "Data\\temperature.dat";
        private const string sockerFilePath = "Data\\socker.dat";

        #endregion

        #region methods

        /// <summary>
        /// Read socker file
        /// </summary>
        /// <returns></returns>
        public static Statistics<Socker> ReadSocker()
        {
            logger.Log(LogLevel.Info, "ReadSocker");

            Statistics<Socker> statistics = null;
            string path = string.Concat(AppDomain.CurrentDomain.BaseDirectory, sockerFilePath);

            if (string.IsNullOrEmpty(path) || !File.Exists(path))
            {
                logger.Log(LogLevel.Error, $"ReadSocker file not found {path}");
                return null;
            }

            try
            {
                statistics = new Statistics<Socker>("Year 2024");

                var lines = File.ReadAllLines(path);
                for (var i = 0; i < lines.Length; i += 1)
                {
                    var line = lines[i];

                    //skip header
                    if (i == 0)
                    {
                        continue;
                    }
                    if (i > 0)
                    {
                        Socker socker = Socker.ParseSocker(line);
                        if (socker != null)
                        {
                            statistics.Rankings.Add(socker);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Log(LogLevel.Error, $"ReadSocker {ex.Message}");
                return null;
            }

            return statistics;
        }


        /// <summary>
        /// Read temperature file
        /// </summary>
        /// <returns></returns>
        public static Statistics<Temperature> ReadTemperature()
        {
            logger.Log(LogLevel.Info, "ReadTemperature");

            Statistics<Temperature> statistics = null;
            string path = string.Concat(AppDomain.CurrentDomain.BaseDirectory, temperatureFilePath);

            if (string.IsNullOrEmpty(path) || !File.Exists(path))
            {
                logger.Log(LogLevel.Error, $"ReadTemperature file not found {path}");
                return null;
            }

            try
            {
                var lines = File.ReadAllLines(path);
                for (var i = 0; i < lines.Length; i += 1)
                {
                    var line = lines[i];
                                       
                    //read header
                    if (i == 0)
                    {
                        statistics = new Statistics<Temperature>(line.Trim());
                        continue;
                    }
                    if(i > 3)
                    {
                        Temperature temperature = Temperature.ParseTemperature(line);
                        if(temperature != null)
                        {
                            statistics.Rankings.Add(temperature);
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                logger.Log(LogLevel.Error, $"ReadTemperature {ex.Message}");
                return null;
            }

            return statistics;
        }

        #endregion
    }
}
