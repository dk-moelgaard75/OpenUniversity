using OpenUniversity.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace OpenUniversity.Utility
{
    public static class BinaryFileUtil
    {
        public static void WriteToFile<T>(T objectToWrite) where T : class
        {
            string filePath = GetFilePath<T>();
            T newObj = null;
            if (typeof(T).Equals(typeof(StudentModel)))
            {
                StudentModel tmpObj = objectToWrite as StudentModel;
                tmpObj.Id = GetNextId<StudentModel>();
                newObj = tmpObj as T;
            }
            else if (typeof(T).Equals(typeof(CourseModel)))
            {
                CourseModel tmpObj = objectToWrite as CourseModel;
                tmpObj.Id = GetNextId<CourseModel>();
                newObj = tmpObj as T;
            }
            else if (typeof(T).Equals(typeof(EmployeeModel)))
            {
                EmployeeModel tmpObj = objectToWrite as EmployeeModel;
                tmpObj.Id = GetNextId<EmployeeModel>();
                newObj = tmpObj as T;
            }
            List<T> list = ReadFromFile<T>();
            list.Add(newObj);
            try
            {
                if (File.Exists(filePath))
                {
                    using (Stream stream = File.Open(filePath, FileMode.Truncate))
                    {
                        BinaryFormatter bin = new BinaryFormatter();
                        bin.Serialize(stream, list);
                    }
                }
                else
                {
                    using (Stream stream = File.Open(filePath, FileMode.OpenOrCreate))
                    {
                        BinaryFormatter bin = new BinaryFormatter();
                        bin.Serialize(stream, list);
                    }
                }
            }
            catch (Exception e)
            {
                //supress error
            }
        }
        public static List<T> ReadFromFile<T>() where T : class
        {
            string filePath = GetFilePath<T>();
            List<T> list = new List<T>();
            try
            {
                using (Stream stream = File.Open(filePath, FileMode.OpenOrCreate))
                {
                    BinaryFormatter bin = new BinaryFormatter();
                    list = (List<T>)bin.Deserialize(stream);
                }
            }
            catch (Exception e)
            {
                //supress error
            }
            return list;
        }
        public static void RemoveFromFile<T>(T objectToRemove) where T : class
        {
            string filePath = GetFilePath<T>();
            List<T> list = ReadFromFile<T>();
            list.Remove(objectToRemove);
            
            try
            {
                if (File.Exists(filePath))
                {
                    using (Stream stream = File.Open(filePath, FileMode.Truncate))
                    {
                        BinaryFormatter bin = new BinaryFormatter();
                        bin.Serialize(stream, list);
                    }
                }
                else
                {
                    using (Stream stream = File.Open(filePath, FileMode.OpenOrCreate))
                    {
                        BinaryFormatter bin = new BinaryFormatter();
                        bin.Serialize(stream, list);
                    }
                }
            }
            catch (Exception e)
            {
                //supress error
            }


        }
        private static string GetFilePath<T>() where T : class
        {
            string basepath = ConfigurationManager.AppSettings["filepath"];
            string filePath = basepath;
            if (typeof(T).Equals(typeof(StudentModel)))
            {
                filePath += "\\student.dat";
            }
            else if (typeof(T).Equals(typeof(CourseModel)))
            {
                filePath += "\\course.dat";
            }
            else if (typeof(T).Equals(typeof(EmployeeModel)))
            {
                filePath += "\\employee.dat";
            }
            return filePath;

        }
        private static int GetNextId<T>() where T : class
        {
            int retVal = 0;
            int currentMax = 0;
            string filePath = GetFilePath<T>();
            if (typeof(T).Equals(typeof(StudentModel)))
            {
                if (File.Exists(filePath))
                {
                    List<StudentModel> list = ReadFromFile<StudentModel>();
                    foreach (StudentModel obj in list)
                    {
                        if (currentMax == 0)
                        {
                            currentMax = obj.Id;
                        }
                        else if (currentMax < obj.Id)
                        {
                            currentMax = obj.Id;
                        }
                    }
                    currentMax++;
                }
                retVal = currentMax;

            }
            else if (typeof(T).Equals(typeof(CourseModel)))
            {
                if (File.Exists(filePath))
                {
                    List<CourseModel> list = ReadFromFile<CourseModel>();
                    foreach (CourseModel obj in list)
                    {
                        if (currentMax == 0)
                        {
                            currentMax = obj.Id;
                        }
                        else if (currentMax < obj.Id)
                        {
                            currentMax = obj.Id;
                        }
                    }
                    currentMax++;
                }
                retVal = currentMax;
            }
            else if (typeof(T).Equals(typeof(EmployeeModel)))
            {
                if (File.Exists(filePath))
                {
                    List<EmployeeModel> list = ReadFromFile<EmployeeModel>();
                    foreach (EmployeeModel obj in list)
                    {
                        if (currentMax == 0)
                        {
                            currentMax = obj.Id;
                        }
                        else if (currentMax < obj.Id)
                        {
                            currentMax = obj.Id;
                        }
                    }
                    currentMax++;
                }
                retVal = currentMax;
            }
            return retVal;
        }
    }
}

