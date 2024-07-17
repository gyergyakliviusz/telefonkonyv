using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace TelefonkonyvWinforms
{
    public partial class Form1 : Form
    {
        private List<Contact> contacts;

        public Form1()
        {
            InitializeComponent();
            contacts = new List<Contact>();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var contact = new Contact
            {
                Name = txtName.Text,
                Address = txtAddress.Text,
                FatherName = txtFatherName.Text,
                MotherName = txtMotherName.Text,
                MobileNo = long.Parse(txtMobileNo.Text),
                Sex = txtSex.Text,
                Mail = txtMail.Text,
                CitizenNo = txtCitizenNo.Text
            };

            contacts.Add(contact);
            MessageBox.Show("Contact added successfully!");

            ClearTextFields();
        }

        private void btnList_Click(object sender, EventArgs e)
        {
            lstContacts.Items.Clear();
            foreach (var contact in contacts)
            {
                lstContacts.Items.Add(contact);
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnModify_Click(object sender, EventArgs e)
        {
            var name = txtName.Text;
            var contact = contacts.FirstOrDefault(c => c.Name == name);
            if (contact != null)
            {
                contact.Address = txtAddress.Text;
                contact.FatherName = txtFatherName.Text;
                contact.MotherName = txtMotherName.Text;
                contact.MobileNo = long.Parse(txtMobileNo.Text);
                contact.Sex = txtSex.Text;
                contact.Mail = txtMail.Text;
                contact.CitizenNo = txtCitizenNo.Text;
                MessageBox.Show("Contact modified successfully!");
            }
            else
            {
                MessageBox.Show("Contact not found!");
            }

            ClearTextFields();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            var name = txtName.Text;
            var contact = contacts.FirstOrDefault(c => c.Name == name);
            if (contact != null)
            {
                txtAddress.Text = contact.Address;
                txtFatherName.Text = contact.FatherName;
                txtMotherName.Text = contact.MotherName;
                txtMobileNo.Text = contact.MobileNo.ToString();
                txtSex.Text = contact.Sex;
                txtMail.Text = contact.Mail;
                txtCitizenNo.Text = contact.CitizenNo;
                MessageBox.Show("Contact found!");
            }
            else
            {
                MessageBox.Show("Contact not found!");
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            var name = txtName.Text;
            var contact = contacts.FirstOrDefault(c => c.Name == name);
            if (contact != null)
            {
                contacts.Remove(contact);
                MessageBox.Show("Contact deleted successfully!");
            }
            else
            {
                MessageBox.Show("Contact not found!");
            }

            ClearTextFields();
        }

        private void ClearTextFields()
        {
            txtName.Clear();
            txtAddress.Clear();
            txtFatherName.Clear();
            txtMotherName.Clear();
            txtMobileNo.Clear();
            txtSex.Clear();
            txtMail.Clear();
            txtCitizenNo.Clear();
        }
    }

    public class Contact
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string FatherName { get; set; }
        public string MotherName { get; set; }
        public long MobileNo { get; set; }
        public string Sex { get; set; }
        public string Mail { get; set; }
        public string CitizenNo { get; set; }

        public override string ToString()
        {
            return $"{Name} - {MobileNo}";
        }
    }

}