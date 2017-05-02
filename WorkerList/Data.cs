using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Win32;

namespace WorkerList
{
    static class Data
    {
        public static string GetFile()
        {
            string filePath = "";
            OpenFileDialog filedialog = new OpenFileDialog()
            {
                Title="Открыть",
                DefaultExt = ".txt",
                CheckFileExists=true,
                Multiselect=false,
                Filter = "TXT Files (*.txt)|*.txt"
            };

            if (filedialog.ShowDialog() == true)
            {
                if (filedialog.FileName == null)
                    throw new DataExeption("ошибка открытия файла");

                filePath = filedialog.FileName;
            }
            else throw new DataExeption("файл не выбран");

            return filePath;
        }

        public static string GetSaveFile()
        {
            string filePath = "";
            SaveFileDialog filedialog = new SaveFileDialog()
            {
                Title = "Сохранение",
                DefaultExt = ".txt",
                AddExtension =true,
                CreatePrompt=true,
                OverwritePrompt=true,
                
                Filter = "TXT Files (*.txt)|*.txt"
            };

            if (filedialog.ShowDialog() == true)
            {
                if (filedialog.FileName == null)
                    throw new DataExeption("ошибка открытия файла");

                filePath = filedialog.FileName;
            }
            else throw new DataExeption("файл не выбран");

            return filePath;
        }
    }
}
