using Aspose.Pdf;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;

namespace ConvertImageToPDF
{
    public partial class MainForm : Form
    {
        List<string> imagesPath = new List<string>();
        public MainForm()
        {
            InitializeComponent();
        }

        private void BrowserButton_Click(object sender, EventArgs e)
        {
            imagesPath.Clear();
            ImagesPathDataGridView.DataSource = null;
            StatusLabel.ForeColor = System.Drawing.Color.Red;
            StatusLabel.Text = "No File Choosing(Max : 4 images)";
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Title = "Choose Files";
            openFile.Filter = "Only Image Format|*.jpeg;*.png;*.jpg";
            openFile.Multiselect = true;
            var result = openFile.ShowDialog();
            if (result == DialogResult.OK)
            {
                if (openFile.FileNames.Count() > 4)
                    return;
                foreach (var path in openFile.FileNames)
                {
                    imagesPath.Add(path);
                }
                ImagesPathDataGridView.DataSource = imagesPath.Select(x => new { ImagePath = x }).ToList();
                StatusLabel.ForeColor = System.Drawing.Color.Green;
                StatusLabel.Text = "Files Selected";
            }
        }

        private void ConvertButton_Click(object sender, EventArgs e)
        {
            if (imagesPath.Count != 0)
            {
                Document document = new Document();

                imagesPath.ForEach(i =>
                {
                    Page page = document.Pages.Add();
                    page.PageInfo.Height = 750;
                    page.PageInfo.Width = 750;
                    Aspose.Pdf.Image image = new Aspose.Pdf.Image();
                    image.File = i;
                    page.Paragraphs.Add(image);
                });

                SaveFileDialog saveFile = new SaveFileDialog();
                saveFile.Title = "Choose a place";
                saveFile.Filter = "Only PDF Format|*.pdf";
                var result = saveFile.ShowDialog();
                if (result == DialogResult.OK)
                {
                    document.Save(saveFile.FileName);
                    MessageBox.Show("Done");
                }
            }
        }

        private void ImagesPathDataGridViwe_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var path = ImagesPathDataGridView.CurrentCell.Value.ToString();
            Process.Start(path);
        }
    }
}
