using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.IO;

namespace API
{
   
    public partial class Form1 : Form
    {

        enum DriveType
        {
            DRIVE_UNKNOWN = 0,
            DRIVE_NO_ROOT_DIR = 1,
            DRIVE_REMOVABLE = 2,
            DRIVE_FIXED = 3,
            DRIVE_REMOTE = 4,
            DRIVE_CDROM = 5,
            DRIVE_RAMDISK = 6
        };

        [DllImport("kernel32.dll")]
        private static extern DriveType GetDriveType(String lpRootPathName);

        [DllImport("kernel32.dll")]
        static extern void GetDiskFreeSpace (string lpRootPathName, out uint lpSectorsPerCluster, out uint lpBytesPerSector, out uint lpNumberOfFreeClusters, out uint lpTotalNumberOfClusters);


        public Form1()
        {
            InitializeComponent();

            DriveInfo[] allDrives = DriveInfo.GetDrives();

            foreach(DriveInfo d in allDrives)
            {
                comboBox1.Items.Add(d);
            }

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem == null)
            {
                MessageBox.Show("Выберете диск","Предупреждение");
            }

            else
            {
                uint sectorsPerCluster, bytesPerSector, numberOfFreeClusters, totalNumberOfClusters;
                string disk = comboBox1.SelectedItem.ToString(); // @"C:\";
                GetDiskFreeSpace(disk, out sectorsPerCluster, out bytesPerSector, out numberOfFreeClusters, out totalNumberOfClusters);

                textBox1.Text = string.Format("Количество секторов в кластере на диске {0}: {1}", disk, sectorsPerCluster + " " + GetDriveType(disk));
            }

        }

     
    }
}
