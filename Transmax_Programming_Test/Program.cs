using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Transmax_Programming_Test
{
    static class Program
    {
        private const int CORRECT_VALUE_COUNT = 3;
        private const string OUTPUT_ERROR_FILE_NAME = "-error.txt";
        private const string OUTPUT_FILE_NAME = "-graded.txt";
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }

        public static void readFile(string filePath) {
            //MessageBox.Show(filePath, "Pass Upto This Point.", MessageBoxButtons.OK, MessageBoxIcon.None);
            string inputFileName = "";
            string outputFileDir = "";
            
            ArrayList recordList = new ArrayList();
            ArrayList errorRecordList = new ArrayList();

            if (IsFilePathExists(filePath))
            {
                //MessageBox.Show(filePath, "Pass Upto This Point. - IsFilePathExists(filePath)", MessageBoxButtons.OK, MessageBoxIcon.None);
                inputFileName = Path.GetFileNameWithoutExtension(filePath);
                //MessageBox.Show(inputFileName, "Pass Upto This Point. - inputFileName", MessageBoxButtons.OK, MessageBoxIcon.None);
                outputFileDir = Path.GetDirectoryName(filePath);
                //MessageBox.Show(outputFileDir, "Pass Upto This Point. - outputFileDir", MessageBoxButtons.OK, MessageBoxIcon.None);
                string[] lines = File.ReadAllLines(filePath);
                //MessageBox.Show(lines.Length.ToString(), "Pass Upto This Point. - lines", MessageBoxButtons.OK, MessageBoxIcon.None);
                foreach (string line in lines)
                {
                    string[] values = line.Split(',');

                    try
                    {
                        if (values.Length == CORRECT_VALUE_COUNT)
                        {
                            string lastname = values[0].Trim();
                            string firstname = values[1].Trim();
                            int grade = Convert.ToInt16(values[2].Trim());

                            if (grade > 0)
                            {
                                Record record = new Record(firstname, lastname, grade);
                                recordList.Add(record);
                            }else{
                                errorRecordList.Add(line);
                            }
                        }
                        else
                        {
                            errorRecordList.Add(line);
                        }
                    }
                    catch (Exception)
                    {
                        errorRecordList.Add(line);
                    }
                }
                SortRecord(recordList, inputFileName, outputFileDir);
                WriteErrorRecord(errorRecordList, inputFileName, outputFileDir);
            }
        }

        public static bool IsFilePathExists(string filePath)
        {
            return File.Exists(filePath);
        }

        public static void SortRecord(ArrayList recordList, string inputFileName, string outputFileDir)
        {
            IComparer comparer = new Record();
            recordList.Sort(comparer);
            WriteRecord(recordList, inputFileName, outputFileDir);
        }

        public static void WriteRecord(ArrayList recordList, string inputFileName, string outputFileDir)
        {
            //MessageBox.Show(recordList.Count.ToString(), "Pass Upto This Point. - outputErrorFilePath", MessageBoxButtons.OK, MessageBoxIcon.None);
            string outputFilePath = (outputFileDir + "\\" + inputFileName + OUTPUT_FILE_NAME);

            if (IsFilePathExists(outputFilePath))
            {
                File.Delete(outputFilePath);
            }

            foreach (Record record in recordList)
            {
                using (StreamWriter writer = new StreamWriter(outputFilePath, true))
                {
                    writer.WriteLine("{0}, {1}, {2}", record.lastname, record.firstname, record.grade);
                }
            }

        }

        public static void WriteErrorRecord(ArrayList ErrorRecordList, string inputFileName, string outputFileDir)
        {
            string outputErrorFilePath = (outputFileDir + "\\" + inputFileName + OUTPUT_ERROR_FILE_NAME);
            //MessageBox.Show(outputErrorFilePath, "Pass Upto This Point. - outputErrorFilePath", MessageBoxButtons.OK, MessageBoxIcon.None);
            if (IsFilePathExists(outputErrorFilePath))
            {
                File.Delete(outputErrorFilePath);
            }
            if (ErrorRecordList.Count > 0)
            {
                foreach (string errorLine in ErrorRecordList)
                {
                    using (StreamWriter writer = new StreamWriter(outputErrorFilePath, true))
                    {
                        writer.WriteLine("{0}", errorLine);
                    }
                }

                //Console.WriteLine("\nFinished with {0} error(s).", ErrorRecordList.Count);
                //Console.WriteLine("\nCreated:\t{0}", outputFilePath);
                //Console.WriteLine("Error log:\t{0}", outputErrorFilePath);
            }
            else
            {
                //Console.WriteLine("\nFinished.", ErrorRecordList.Count);
                //Console.WriteLine("\nCreated: {0}", outputFilePath);
            }
        } 
    }
}
