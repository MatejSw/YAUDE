using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;
using YAUDE.Model;

namespace YAUDE.Services
{
    public static class FileSaver
    {
        public static bool Save(Form1 form)
        {
            try
            {
                if (form.filePath == null)
                {
                    SaveFileDialog saveFileDialog = new SaveFileDialog();
                    saveFileDialog.Filter = "JSON files (*.json)|*.json|All files (*.*)|*.*";
                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        form.filePath = saveFileDialog.FileName;
                    }
                    else
                    {
                        return false; // User cancelled the save operation
                    }
                }
                using (StreamWriter writer = new StreamWriter(form.filePath))
                {
                    string json = JsonConvert.SerializeObject(form.elements, Formatting.Indented);
                    writer.Write(json);
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool SaveAs(Form1 form)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "JSON files (*.json)|*.json|All files (*.*)|*.*";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                using (StreamWriter writer = new StreamWriter(saveFileDialog.FileName))
                {
                    string json = JsonConvert.SerializeObject(form.elements, Formatting.Indented);
                    writer.Write(json);
                }
                form.filePath = saveFileDialog.FileName;
                form.Text = $"YAUDE - {Path.GetFileName(form.filePath)}";
                return true;
            }
            return false;
        }

        public static List<DiagramElement> Open(Form1 form)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "JSON files (*.json)|*.json|All files (*.*)|*.*";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                List<DiagramElement> list = new();

                using (StreamReader reader = new StreamReader(openFileDialog.FileName))
                {
                    string json = reader.ReadToEnd();
                    List<DiagramElement> temp = new();

                    temp.AddRange(JsonConvert.DeserializeObject<List<DiagramClass>>(json));
                    list.AddRange(temp.Where(x => x.Type == "Class"));

                    temp.Clear();
                    temp.AddRange(JsonConvert.DeserializeObject<List<DiagramEnum>>(json));
                    list.AddRange(temp.Where(x => x.Type == "Enum"));

                    temp.Clear();
                    temp.AddRange(JsonConvert.DeserializeObject<List<DiagramNote>>(json));
                    list.AddRange(temp.Where(x => x.Type == "Note"));
                }
                form.filePath = openFileDialog.FileName;
                return list;
            }
            return new();
        }
    }
}
