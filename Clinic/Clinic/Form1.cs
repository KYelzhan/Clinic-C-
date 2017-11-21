using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Clinic
{
    public partial class Form1 : MetroFramework.Forms.MetroForm
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "appData.Patients". При необходимости она может быть перемещена или удалена.
            this.patientsTableAdapter.Fill(this.appData.Patients);
        }

        private void TileAdd_Click(object sender, EventArgs e)
        {
            try
            {
                metroTabControl1.SelectedTab = metroTabPage2;
                panel.Enabled = true;
                appData.Patients.AddPatientsRow(appData.Patients.NewPatientsRow());
                patientsBindingSource.MoveLast();
                TextBoxFullName.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                appData.Patients.RejectChanges();
            }
        }

        private void TileEdit_Click(object sender, EventArgs e)
        {
            metroTabControl1.SelectedTab = metroTabPage2;
            panel.Enabled = true;
            TextBoxFullName.Focus();
        }

        private void TileDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure want to delete this record?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                patientsBindingSource.RemoveCurrent();
        }

        private void TileSave_Click(object sender, EventArgs e)
        {
            try
            {
                panel.Enabled = false;
                patientsBindingSource.EndEdit();
                patientsTableAdapter.Update(appData.Patients);
                Grid.Refresh();
                TextBoxFullName.Focus();
                MessageBox.Show("Your data has been sussefully saved.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                appData.Patients.RejectChanges();
            }
        }

        private void TileOK_Click(object sender, EventArgs e)
        {
            metroTabControl1.SelectedTab = metroTabPage1;
        }

        private void TileCancel_Click(object sender, EventArgs e)
        {
            panel.Enabled = false;
            patientsBindingSource.ResetBindings(false);
            metroTabControl1.SelectedTab = metroTabPage1;
        }

        private void ButtonApply_Click(object sender, EventArgs e)
        {
            //if (!string.IsNullOrEmpty(TextBoxSearch.Text))
            //patientsBindingSource.Filter = string.Format("Department = '{0}' OR Sex LIKE '*{1}*' OR Modality = '{2}' OR FullName LIKE '*{3}*' OR CardNumber = '{4}' OR ClinicalInformation LIKE '*{5}*' OR Diagnosis = '{6}' OR Site LIKE '*{7}*' OR Intagration = '{8}' OR Conclusion LIKE '*{9}*'", ComboBoxDepartment.Text, ComboBoxSex.Text, ComboBoxModality.Text, TextBoxFullName.Text, TextBoxCardNumber.Text, TextBoxClinicalInformation.Text, TextBoxDiagnosis.Text, TextBoxSite.Text, TextBoxIntagration.Text, TextBoxConclusion.Text);
            if (!string.IsNullOrEmpty(TextBoxSearch.Text))
                patientsBindingSource.Filter = string.Format("ClinicalInformation = '{0}' OR Diagnosis LIKE '*{1}*' OR Site = '{2}' OR Intagration LIKE '*{3}*' OR Conclusion = '{4}' OR Doctor LIKE '*{5}*' OR Speciality = '{6}'", TextBoxSearch.Text, TextBoxSearch.Text, TextBoxSearch.Text, TextBoxSearch.Text, TextBoxSearch.Text, TextBoxSearch.Text, TextBoxSearch.Text);
            else if (!string.IsNullOrEmpty(ComboBoxSSex.Text))
                patientsBindingSource.Filter = string.Format("Sex = '{0}'", ComboBoxSSex.Text);
            else if (!string.IsNullOrEmpty(ComboBoxSDepartment.Text))
                patientsBindingSource.Filter = string.Format("Department = '{0}'", ComboBoxSDepartment.Text);
            else if (!string.IsNullOrEmpty(ComboBoxSModality.Text))
                patientsBindingSource.Filter = string.Format("Modality = '{0}'", ComboBoxSModality.Text);
            else if (!string.IsNullOrEmpty(TextBoxSFullName.Text))
                patientsBindingSource.Filter = string.Format("FullName = '{0}'", TextBoxSFullName.Text);
            else if (!string.IsNullOrEmpty(TextBoxSCardNumber.Text))
                patientsBindingSource.Filter = string.Format("CardNumber = '{0}'", TextBoxSCardNumber.Text);
            else
                patientsBindingSource.Filter = string.Empty;
        }

        private void ButtonCancel_Click(object sender, EventArgs e)
        {
            patientsBindingSource.Filter = string.Empty;
            TextBoxSearch.Text = "";
            ComboBoxSSex.Text = "";
            ComboBoxSDepartment.Text = "";
            ComboBoxSModality.Text = "";
            TextBoxSFullName.Text = "";
            TextBoxSCardNumber.Text = "";
        }

        private void Grid_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            metroTabControl1.SelectedTab = metroTabPage2;
        }
    }
}
