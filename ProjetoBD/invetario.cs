﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace ProjetoBD
{
    public partial class invetario : UserControl
    {
        public invetario()
        {
            InitializeComponent();
        }

        private void invetario_Load(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection("Data Source=DESKTOP-JLR4AMV;Initial Catalog=Livraria;Integrated Security=True"))
            {
                con.Open();
                    SqlDataAdapter sqlDa = new SqlDataAdapter("select * from inventario",con);
                DataTable dt = new DataTable();

                sqlDa.Fill(dt);
                dataGridView1.DataSource = dt;

            }
                
        }

        private String CheckIfEmpty(TextBox textBox)
        {
            if (!String.IsNullOrEmpty(textBox.Text))
            { 
                if(textBox.Name == "Editora" || textBox.Name=="Autor") 
                    return textBox.Name +".Nome = '"+ textBox.Text +"'";
                if(textBox.Name == "Titulo")
                    return "Livro."+textBox.Name+" = '"+textBox.Text +"'";
                if(textBox.Name == "ISBN")
                    return "Livro.Ref = '" + textBox.Text +"'";

            }
            return "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection("Data Source=DESKTOP-JLR4AMV;Initial Catalog=Livraria;Integrated Security=True"))
            {

                con.Open();
                String total = "";

                List<TextBox> lst = new List<TextBox> { Editora, Autor, Titulo, ISBN };
                foreach (TextBox tb in lst)
                {
                    if(total.Length == 0)
                    {
                        if(CheckIfEmpty(tb) != "")
                            total += "WHERE " + CheckIfEmpty(tb);
                    }
                    else
                    {
                        if (CheckIfEmpty(tb) != "") 
                            total += " AND " + CheckIfEmpty(tb);
                    }
                }
                
                SqlDataAdapter sqlDa = new SqlDataAdapter("SELECT Ref, Titulo, Price, Editora.Nome AS Editora, Autor.Nome AS Autor, Stock FROM " +
                "Livro JOIN Editora ON ID_Editora = Editora.Identificador JOIN Autor ON ID_Autor = Autor.Identificador " +
                total, con);
                DataTable dt = new DataTable();
                sqlDa.Fill(dt);
                dataGridView1.DataSource = dt;
                

            }
            

            

        }
    }
}
